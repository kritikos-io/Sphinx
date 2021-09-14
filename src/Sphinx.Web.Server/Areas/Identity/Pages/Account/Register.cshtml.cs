 namespace Kritikos.Sphinx.Web.Server.Areas.Identity.Pages.Account
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Text;
  using System.Text.Encodings.Web;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Web.Server.Helpers;

  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.DataProtection;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.UI.Services;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;
  using Microsoft.AspNetCore.WebUtilities;
  using Microsoft.Extensions.Logging;

  [AllowAnonymous]
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<SphinxUser> signInManager;
    private readonly UserManager<SphinxUser> userManager;
    private readonly IPersistedDataProtector protector;

    private readonly ILogger<RegisterModel> logger;
    private readonly IEmailSender emailSender;

    public RegisterModel(
      UserManager<SphinxUser> userManager,
      SignInManager<SphinxUser> signInManager,
      ILogger<RegisterModel> logger,
      IDataProtectionProvider dataProtector,
      IEmailSender emailSender)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.logger = logger;
      this.emailSender = emailSender;
      protector = dataProtector.CreateProtector(DataProtectionPurposes.UserManagement) as IPersistedDataProtector
                  ?? throw new InvalidOperationException("Could not create a data protector for user management");
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; } = string.Empty;

    public IList<AuthenticationScheme> ExternalLogins { get; set; }
      = Array.Empty<AuthenticationScheme>();

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
      returnUrl ??= Url.Content("~/");
      ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      if (ModelState.IsValid)
      {
        var user = new SphinxUser
        {
          UserName = Input.Email,
          Email = Input.Email,
          FirstName = protector.Protect(Input.FirstName),
          LastName = protector.Protect(Input.LastName),
          PhoneNumber = protector.Protect(Input.PhoneNumber),
        };

        var result = await userManager.CreateAsync(user, Input.Password);
        if (result.Succeeded)
        {
          logger.LogInformation("User created a new account with password.");

          var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
          code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
          var callbackUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
            protocol: Request.Scheme);

          await emailSender.SendEmailAsync(
            Input.Email,
            "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

          if (userManager.Options.SignIn.RequireConfirmedAccount)
          {
            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
          }
          else
          {
            await signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
          }
        }

        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }

    public async Task OnGetAsync(string? returnUrl = null)
    {
      ReturnUrl = returnUrl ?? string.Empty;
      ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public class InputModel
    {
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; } = string.Empty;

      [Display(Name = "First Name")]
      public string FirstName { get; set; } = string.Empty;

      [Display(Name = "Last Name")]
      public string LastName { get; set; } = string.Empty;

      [Display(Name = "Phone Number")]
      public string PhoneNumber { get; set; } = string.Empty;

      [Required]
      [StringLength(
        100,
        ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; } = string.Empty;

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; } = string.Empty;
    }
  }
}
