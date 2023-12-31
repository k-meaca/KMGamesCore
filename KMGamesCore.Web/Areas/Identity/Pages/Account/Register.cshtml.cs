﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using KMGamesCore.Data.Repository.Interfaces;
using KMGamesCore.Models.Models;
using KMGamesCore.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Nodes;

namespace KMGamesCore.Web.Areas.Identity.Pages.Account
{

    [IgnoreAntiforgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [StringLength(300,ErrorMessage = "Street Address must be provided.",MinimumLength = 6)]
            public string StreetAddress { get; set; }


            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Country must be selected.")]
            [DisplayName("Country")]
            [BindProperty(SupportsGet = true)]
            public int CountryId { get; set; }

            [ForeignKey("CountryId")]
            public Country Country { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "City must be selected.")]
            [DisplayName("City")]
            [BindProperty]
            public int CityId { get; set; }

            [ForeignKey("CityId")]
            public City City { get; set; }

            public string? ZipCode { get; set; }

            public string Role { get; set; }

            [ValidateNever]
            public List<SelectListItem> Roles { get; set; }

            [ValidateNever]
            public List<SelectListItem> Countries { get; set; }

            [ValidateNever]
            public List<SelectListItem> Cities { get; set; }

            [StringLength(14,ErrorMessage = "Phone must be provided.",MinimumLength =1)]
            public string Phone { get; set; }

        }

        //public IActionResult OnGetMessage()
        //{
        //    return new JsonResult("message from code.");
        //}

        public IActionResult OnGetCities(int countryId)
        {

            return new JsonResult(_unitOfWork.Cities.GetAll().Where(c => c.CountryId == countryId));

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!(await _roleManager.RoleExistsAsync(WC.Role_Admin)))
            {
                await _roleManager.CreateAsync(new IdentityRole(WC.Role_Admin));
                await _roleManager.CreateAsync(new IdentityRole(WC.Role_Customer));
                await _roleManager.CreateAsync(new IdentityRole(WC.Role_User_individual));
                await _roleManager.CreateAsync(new IdentityRole(WC.Role_User_Company));
            }
            ReturnUrl = returnUrl;

            Input = new InputModel()
            {
                Countries = _unitOfWork.Countries.GetAll()
                                                 .Select(c =>
                                                 {
                                                     return new SelectListItem()
                                                     {
                                                         Value = c.CountryId.ToString(),
                                                         Text = c.Name
                                                     };
                                                 }).ToList(),
                Cities = new List<SelectListItem>(),
                Roles = _roleManager.Roles.Select(r => r.Name)
                                          .Select(r => new SelectListItem
                                          {
                                              Value = r,
                                              Text = r
                                          }).ToList()

            };

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            //HARDCORDE
            //Input.CityId = 1;

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.CountryId = Input.CountryId;
                user.StreetAddress = Input.StreetAddress;
                user.ZipCode = Input.ZipCode;
                user.PhoneNumber = Input.Phone;
                user.PhoneNumberConfirmed = true;

                //HARDCODE
                //user.CityId = _unitOfWork.Cities.GetAll().FirstOrDefault().CityId;

                user.CityId = Input.CityId;


                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if(Input.Role == null)
                    {
                        await _userManager.AddToRoleAsync(user, WC.Role_User_individual);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form

            Input.Countries = _unitOfWork.Countries.GetAll()
                                 .Select(c =>
                                 {
                                     return new SelectListItem()
                                     {
                                         Value = c.CountryId.ToString(),
                                         Text = c.Name
                                     };
                                 }).ToList();

            Input.Cities = _unitOfWork.Cities.GetAll()
                     .Select(c =>
                     {
                         return new SelectListItem()
                         {
                             Value = c.CityId.ToString(),
                             Text = c.Name
                         };
                     }).ToList();

            Input.Roles = _roleManager.Roles.Select(r => r.Name)
                          .Select(r => new SelectListItem
                          {
                              Value = r,
                              Text = r
                          }).ToList();

            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
