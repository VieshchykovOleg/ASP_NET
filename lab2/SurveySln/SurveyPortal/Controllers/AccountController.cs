using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models.ViewModels;

namespace SurveyPortal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager; // Для роботи з ролями

        public AccountController(UserManager<IdentityUser> userMgr,
                                 SignInManager<IdentityUser> signInMgr,
                                 RoleManager<IdentityRole> roleMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMgr;
        }

        // --- LOGIN ---
        [AllowAnonymous] // Доступно всім
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl ?? "/"
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? user =
                    await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("", "Неправильне ім'я або пароль");
            }
            return View(loginModel);
        }

        // --- REGISTER ---
        [AllowAnonymous]
        public ViewResult Register() => View(new RegisterModel());

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // АВТОМАТИЧНО ДОДАЄМО НОВОГО КОРИСТУВАЧА ДО РОЛІ "User"
                    await userManager.AddToRoleAsync(user, "User");

                    // Одразу логінимо
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        // --- LOGOUT ---
        [Authorize] // Доступно тільки залогіненим
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        // --- EDIT / PROFILE (Особистий кабінет) ---
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User); // Отримуємо поточного юзера
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new EditModel
            {
                Id = user.Id,
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Користувача не знайдено");
                return View(model);
            }

            // Оновлюємо Email
            user.Email = model.Email;
            var emailResult = await userManager.UpdateAsync(user);

            if (!emailResult.Succeeded)
            {
                ModelState.AddModelError("", "Неможливо змінити Email.");
                return View(model);
            }

            // Оновлюємо пароль (тільки якщо його ввели)
            if (!string.IsNullOrEmpty(model.Password))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var passResult = await userManager.ResetPasswordAsync(user, token, model.Password);

                if (!passResult.Succeeded)
                {
                    ModelState.AddModelError("", "Неможливо змінити пароль.");
                    foreach (var err in passResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(model);
                }
            }

            TempData["message"] = "Профіль оновлено";
            return RedirectToAction("Index", "Home");
        }
    }
}