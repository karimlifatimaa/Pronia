using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pronia.DTOs.AccountDto;
using Pronia.Helpers;
using Pronia.Models;

namespace Pronia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(!ModelState.IsValid) return View();
            User user = new User()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,  
                Name = registerDto.Name,
                Surname = registerDto.Surname,  
                
            };
            var result= await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            

            return RedirectToAction(nameof(Login));
        }                              
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto,string? ReturnUrl=null)
        {
            if(!ModelState.IsValid) return View();
            var user=await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);
                if(user == null)
                {
                    ModelState.AddModelError("", "Username/Email ve ya Password yanlisdir");
                    return View();
                }
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "Daha sonra yenidn sina");
                return View();
            }
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username/Email ve ya Password yanlisdir");
                return View();
            }
            await _signInManager.SignInAsync(user, loginDto.IsRemember);
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> CreateRole()
        {
           foreach(var item in Enum.GetValues(typeof(UserRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name=item.ToString()
                });
            }
            return Ok();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
        