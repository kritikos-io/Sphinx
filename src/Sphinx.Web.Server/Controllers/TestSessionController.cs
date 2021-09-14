namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Server.Helpers.Extensions;
  using Kritikos.Sphinx.Web.Server.Models.CreateDto;

  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [Route("api/testSession")]
  [AllowAnonymous]
  public class TestSessionController : BaseController<TestSessionController>
  {
    public TestSessionController(
      SphinxDbContext dbContext,
      IPureMapper mapper,
      ILogger<TestSessionController> logger,
      UserManager<SphinxUser> userManager)
      : base(dbContext, mapper, logger, userManager)
    {
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateSession(TestSessionCreateDto dto, CancellationToken token = default!)
    {
      List<long> groupIds = new(5) { dto.GroupA, dto.GroupB, dto.GroupC };
      if (dto.GroupD != null)
      {
        groupIds.Add(dto.GroupD.Value);
      }

      if (dto.GroupE != null)
      {
        groupIds.Add(dto.GroupE.Value);
      }

      var groups = await DbContext.StimulusGroups
        .Include(e => e.Stimuli)
        .Include(e => e.Dataset)
        .Where(x => groupIds.Contains(x.Id))
        .ToListAsync(token);

      if (groups.Count != groupIds.Count)
      {
        return NotFound("Some stimuli group do not exist or you do not have sufficient permissions!");
      }

      var matches = await DbContext.SignificantMatches
        .Include(x => x.Secondary)
        .ThenInclude(x => x.Group)
        .Include(x => x.Secondary)
        .ThenInclude(x => x.Group)
        .ToListAsync(token);

      var session = new TestSession
      {
        Title = dto.Title,
        A = groups.Single(x => x.Id == dto.GroupA),
        B = groups.Single(x => x.Id == dto.GroupB),
        C = groups.Single(x => x.Id == dto.GroupC),
        D = groups.SingleOrDefault(x => x.Id == dto.GroupD),
        E = groups.SingleOrDefault(x => x.Id == dto.GroupE),
      };

      var questions_AB =
        Foo(session.A.Stimuli, session.B.Stimuli, matches, session);

      var questions_BA =
        Foo(session.B.Stimuli, session.A.Stimuli, matches, session);

      var questions_AC =
        Foo(session.A.Stimuli, session.C.Stimuli, matches, session);

      var questions_CA =
        Foo(session.C.Stimuli, session.A.Stimuli, matches, session);

      var questions_BC =
        Foo(session.B.Stimuli, session.C.Stimuli, matches, session);

      var questions_CB =
        Foo(session.C.Stimuli, session.B.Stimuli, matches, session);

      DbContext.TestSessions.Add(session);
      DbContext.SessionQuestions.AddRange(questions_AB);
      DbContext.SessionQuestions.AddRange(questions_BA);
      DbContext.SessionQuestions.AddRange(questions_AC);
      DbContext.SessionQuestions.AddRange(questions_CA);
      DbContext.SessionQuestions.AddRange(questions_BC);
      DbContext.SessionQuestions.AddRange(questions_CB);

      await DbContext.SaveChangesAsync(token);

      return Ok();
    }

    List<TestSessionQuestion> Foo(
      IReadOnlyCollection<Stimulus> first,
      IReadOnlyCollection<Stimulus> second,
      List<SignificantStimuliMatch> matches,
      TestSession session)
    {
      var questions = new List<TestSessionQuestion>(first.Count);
      var thisGroup = first.First().Group;
      var otherGroup = second.First().Group;
      var arePrimary = thisGroup.IsPrimary || otherGroup.IsPrimary;

      var relevantMatches = matches
        .WhereIf(thisGroup.IsPrimary, x => x.Secondary.Group.Id == otherGroup.Id)
        .WhereIf(otherGroup.IsPrimary, x => x.Secondary.Group.Id == thisGroup.Id)
        .ToList();
      List<(Stimulus Question, Stimulus Answer)> properMatches;

      if (arePrimary)
      {
        properMatches = relevantMatches.Select(x => thisGroup.IsPrimary
            ? (x.Primary, x.Secondary)
            : (x.Secondary, x.Primary))
          .ToList();
      }
      else
      {
        properMatches = relevantMatches.GroupBy(x => x.Primary)
          .Select(x => (x.Single(x => x.Secondary.Group.Id == thisGroup.Id).Secondary, x.Single(x => x.Secondary.Group.Id == otherGroup.Id).Secondary))
          .ToList();
      }

      foreach (var stimulus in first)
      {
        var question = new TestSessionQuestion
        {
          Session = session,
          UnderTest = stimulus,
          CorrectAnswer = properMatches.Single(x => x.Question.Id == stimulus.Id).Answer,
        };

        var wrong = properMatches.Where(x => x.Answer.Id != question.CorrectAnswer.Id)
          .Randomize()
          .Take(3)
          .ToList();

        question.False1 = wrong.Skip(0).Take(1).Single().Answer;
        question.False2 = wrong.Skip(1).Take(1).Single().Answer;
        question.False3 = wrong.Skip(2).Take(1).Single().Answer;

        questions.Add(question);
      }

      return questions;
    }
  }
}
