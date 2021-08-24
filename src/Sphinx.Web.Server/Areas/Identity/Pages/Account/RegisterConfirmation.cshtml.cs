namespace Kritikos.Sphinx.Web.Server.Areas.Identity.Pages.Account
{
  using System.Text;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence.Identity;

  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.UI.Services;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;
  using Microsoft.AspNetCore.WebUtilities;

  [AllowAnonymous]
  public class RegisterConfirmationModel : PageModel
  {
    private readonly UserManager<SphinxUser> userManager;
    private readonly IEmailSender sender;

    public RegisterConfirmationModel(UserManager<SphinxUser> userManager, IEmailSender sender)
    {
      this.userManager = userManager;
      this.sender = sender;
    }

    public string Email { get; set; } = string.Empty;

    public bool DisplayConfirmAccountLink { get; set; }

    public string EmailConfirmationUrl { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string? email, string? returnUrl = null)
    {
      if (email == null)
      {
        return RedirectToPage("/Index");
      }

      var user = await userManager.FindByEmailAsync(email);
      if (user == null)
      {
        return NotFound($"Unable to load user with email '{email}'.");
      }

      Email = email;

      DisplayConfirmAccountLink = false;
      if (DisplayConfirmAccountLink)
      {
        var userId = await userManager.GetUserIdAsync(user);
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        EmailConfirmationUrl = Url.Page(
          "/Account/ConfirmEmail",
          pageHandler: null,
          values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
          protocol: Request.Scheme);
      }

      return Page();
    }
  }
}
