using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //GET: ACCOUNT/INDEX
        public ActionResult Index()
        {
            return View();
        }

        //Application db contains all personal information about the user. e.g name, address, id-unique identifier of the user. 
        //match the id (userid) obtained from logging in to the Id in the application db
        public ActionResult Details()
        {
            using (var context = new ApplicationDbContext())
            {
                //current user
                var userId = User.Identity.GetUserId();

                //match the Id in db with current user
                var user = context.Users.Single(p => p.Id == userId);

                return View(user);
            }
        }

        //check if the billing address is null. If null redirect to new address. If not null display address; AddressViewModel is found in the accountviewmodel
        public ActionResult Address(int? AccountId = null)
        {
            var model = new AddressViewModel()
            {
                CountriesList = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    .Select(p =>
                    {
                        try
                        {
                            return new RegionInfo(p.LCID).DisplayName;
                        }
                        catch
                        {
                            return null;
                        }
                    })
                    .Where(p => p != null)
                    .Distinct()
                    .OrderBy(p => p)
                    .ToList(),
                BillingAddress = new BillingAddress()
            };

            if (AccountId == null)
            {
                return View(model);
            }

            using (var context = new OnlineStoreContext())
            {
                var userId = User.Identity.GetUserId();
                var address = context.BillingAddress.Where(p => p.CustomerId == userId).Single();

                model.BillingAddress = address;

                return View(model);
            }
        }

        public ActionResult Addresses()
        {
            using (var context = new OnlineStoreContext())
            {
                var userId = User.Identity.GetUserId();

                //return a list of billing addresses where the customerid == userid
                var billingAddresses = context.BillingAddress.Where(p => p.CustomerId == userId).ToList();

                return View(billingAddresses);
            }

        }
        //purpose obtain orders that belong to 1 user; therefore create several orders/customer, therefore create ToList; several orderids are linked to one Customer
        //there is no order form as this is used to display items that have been ordered
        //purchaseddetails table-this contains details of items ordered/customer
        //user logins ensure to view their purchased details. userid is id given to user-user identifier. match the userId (loggin id ) to custoemrid
        //GET method as we are not modifiying anything
        public ActionResult Orders(int? AccountId = null)
        {
            if (AccountId != null)
            {
                using (var context = new OnlineStoreContext())
                {

                    var userId = User.Identity.GetUserId();
                    var orders = context.PurchasedDetails.Where(p => p.CustomerId == userId).ToList();
                    var models = new OrderViewModel()
                    {
                        //contain list of purchases

                    };

                    return View(models);


                }


            }
            return View();
        }

        //create Credit Card form
        //1 user can have several credit cards stored
        //GET: Account/PaymentCard
        public ActionResult PaymentCard()
        {
          
            return View();
        }

        public ActionResult SubmitCards(PaymentOptions)
        {

        }

        //list of credit cards and choose from them
        //HTTP:POST Account/Cards
        [Authorize]
        public ActionResult Cards()
        {
                using (var context = new OnlineStoreContext())
                {
                    var userId = User.Identity.GetUserId();
                    var cardsOp = context.PaymentOptions.Where(p => p.CustomerId == userId).ToList();

                    return View(cardsOp);

                }
                
        }

        //create a cart. 
        //1.create the cart;2. add items to cart. create a new item if cart item does not exist
        //GET:/Account/Cart
        public ActionResult AddCart(int cart, int cartItem)
        {
            using (var context = new OnlineStoreContext())
            {
                var chocolateItem=context.Carts.SingleOrDefault(p => p.ChocolateId == cart.ChocolateId);
                var wineItem = context.Carts.SingleOrDefault(p => p.WineId == cart.WineId);
               //var cartItem = context.Carts.Where(p => p.WineId == cart.WineId || p.ChocolateId == cart.ChocolateId).SingleOrDefault();

                if(chocolateItem==null || wineItem==null)
                {
                    chocolateItem = new Cart
                    {
                        CartId = +1,
                        ChocolateId = cart.ChocolateId,
                        WineId=cart.WineId,
                        DateCreated = DateTime.Now,
                        Count=1

                    };
                    context.Carts.Add(chocolateItem);
                }
                else
                {
                    var addItem = chocolateItem.Count +1;
                }
                
            }
                return View();
        }

        //POST: /Account/Cart
        //Add items in a cart
        //create a new cart if cart is null
        /*[HttpPost]
        public ActionResult AddCart(Cart cart)
        {
            using (var context = new OnlineStoreContext())

            {

                context.Carts.Add(cart);
                context.SaveChanges();

                return View(cart);
            }

        }
        */
       
        //GET: Account/RemoveCartItem
        public ActionResult RemoveCartItem(int id, int cartId)
        {
            using (var context = new OnlineStoreContext())
            {
                var cart = context.Carts.Where(P => P.CartId == id);

                //delete the cart item
               
                if (cart != null)
                {



                }

                return View();

            }

        }

        //GET: Account/EmptyCart
        public ActionResult EmptyCart()
        {
            using (var context = new OnlineStoreContext())
            {


                return View();
            }

        }

        //Diplay list of items in a cart
        //GET: Account/CartList
        //Display Paypal and Credit Card hpyerlinks from cartList page
        [Authorize]
        public ActionResult CartList()
        {
            
                using (var context = new OnlineStoreContext())
                {
                    var userId = User.Identity.GetUserId();
                    var cartList = context.Carts.Where(p => p.CustomerId == userId).ToList();

                    return View(cartList);

                }
            
            
        }

       
       


        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            try
            {
                // Sign in the user with this external login provider if the user already has a login
                var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                    case SignInStatus.Failure:
                    default:
                        // If the user does not have an account, then prompt the user to create an account
                        try
                        {
                            var user = new ApplicationUser
                            {
                                UserName = loginInfo.ExternalIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value + " " + loginInfo.ExternalIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value,
                                Email = loginInfo.Email,
                                FirstName = loginInfo.ExternalIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value,
                                Surname = loginInfo.ExternalIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value
                            };
                            var newUserResult = await UserManager.CreateAsync(user);
                            if (newUserResult.Succeeded)
                            {
                                newUserResult = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                                if (newUserResult.Succeeded)
                                {
                                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                                    return RedirectToLocal("/");
                                }
                            }

                            AddErrors(newUserResult);
                        }
                        catch
                        {

                        }
                        return View("ExternalLoginFailure");
                }
            }
            catch
            {
                return null;
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }


            //create personal details


        }
        #endregion





        //
        // POST: /Account/Login
        /* [HttpPost]
         [AllowAnonymous]
         [ValidateAntiForgeryToken]
         public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
         {
             if (!ModelState.IsValid)
             {
                 return View(model);
             }

             // This doesn't count login failures towards account lockout
             // To enable password failures to trigger account lockout, change to shouldLockout: true
             var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
             switch (result)
             {
                 case SignInStatus.Success:
                     return RedirectToLocal(returnUrl);
                 case SignInStatus.LockedOut:
                     return View("Lockout");
                 case SignInStatus.RequiresVerification:
                     return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                 case SignInStatus.Failure:
                 default:
                     ModelState.AddModelError("", "Invalid login attempt.");
                     return View(model);
             }
         }
         */
        //
        // GET: /Account/VerifyCode
        /*  [AllowAnonymous]
          public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
          {
              // Require that the user has already logged in via username/password or external login
              if (!await SignInManager.HasBeenVerifiedAsync())
              {
                  return View("Error");
              }
              return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
          }

          //
          // POST: /Account/VerifyCode
          [HttpPost]
          [AllowAnonymous]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
          {
              if (!ModelState.IsValid)
              {
                  return View(model);
              }

              // The following code protects for brute force attacks against the two factor codes. 
              // If a user enters incorrect codes for a specified amount of time then the user account 
              // will be locked out for a specified amount of time. 
              // You can configure the account lockout settings in IdentityConfig
              var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
              switch (result)
              {
                  case SignInStatus.Success:
                      return RedirectToLocal(model.ReturnUrl);
                  case SignInStatus.LockedOut:
                      return View("Lockout");
                  case SignInStatus.Failure:
                  default:
                      ModelState.AddModelError("", "Invalid code.");
                      return View(model);
              }
          }

          //
          // GET: /Account/Register
          [AllowAnonymous]
          public ActionResult Register()
          {
              return View();
          }

          //
          // POST: /Account/Register
          [HttpPost]
          [AllowAnonymous]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Register(RegisterViewModel model)
          {
              if (ModelState.IsValid)
              {
                  var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                  var result = await UserManager.CreateAsync(user, model.Password);
                  if (result.Succeeded)
                  {
                      await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                      // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                      // Send an email with this link
                      // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                      // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                      // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                      return RedirectToAction("Index", "Home");
                  }
                  AddErrors(result);
              }

              // If we got this far, something failed, redisplay form
              return View(model);
          }

          //
          // GET: /Account/ConfirmEmail
          [AllowAnonymous]
          public async Task<ActionResult> ConfirmEmail(string userId, string code)
          {
              if (userId == null || code == null)
              {
                  return View("Error");
              }
              var result = await UserManager.ConfirmEmailAsync(userId, code);
              return View(result.Succeeded ? "ConfirmEmail" : "Error");
          }

          //
          // GET: /Account/ForgotPassword
          [AllowAnonymous]
          public ActionResult ForgotPassword()
          {
              return View();
          }

          //
          // POST: /Account/ForgotPassword
          [HttpPost]
          [AllowAnonymous]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
          {
              if (ModelState.IsValid)
              {
                  var user = await UserManager.FindByNameAsync(model.Email);
                  if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                  {
                      // Don't reveal that the user does not exist or is not confirmed
                      return View("ForgotPasswordConfirmation");
                  }

                  // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                  // Send an email with this link
                  // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                  // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                  // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                  // return RedirectToAction("ForgotPasswordConfirmation", "Account");
              }

              // If we got this far, something failed, redisplay form
              return View(model);
          }

          //
          // GET: /Account/ForgotPasswordConfirmation
          [AllowAnonymous]
          public ActionResult ForgotPasswordConfirmation()
          {
              return View();
          }

          //
          // GET: /Account/ResetPassword
          [AllowAnonymous]
          public ActionResult ResetPassword(string code)
          {
              return code == null ? View("Error") : View();
          }

          //
          // POST: /Account/ResetPassword
          [HttpPost]
          [AllowAnonymous]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
          {
              if (!ModelState.IsValid)
              {
                  return View(model);
              }
              var user = await UserManager.FindByNameAsync(model.Email);
              if (user == null)
              {
                  // Don't reveal that the user does not exist
                  return RedirectToAction("ResetPasswordConfirmation", "Account");
              }
              var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
              if (result.Succeeded)
              {
                  return RedirectToAction("ResetPasswordConfirmation", "Account");
              }
              AddErrors(result);
              return View();
          }

          //
          // GET: /Account/ResetPasswordConfirmation
          [AllowAnonymous]
          public ActionResult ResetPasswordConfirmation()
          {
              return View();
          }

          //


          //
          // GET: /Account/SendCode
          [AllowAnonymous]
          public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
          {
              var userId = await SignInManager.GetVerifiedUserIdAsync();
              if (userId == null)
              {
                  return View("Error");
              }
              var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
              var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
              return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
          }

          //
          // POST: /Account/SendCode
          [HttpPost]
          [AllowAnonymous]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> SendCode(SendCodeViewModel model)
          {
              if (!ModelState.IsValid)
              {
                  return View();
              }

              // Generate the token and send it
              if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
              {
                  return View("Error");
              }
              return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
          }

*/
    }
}