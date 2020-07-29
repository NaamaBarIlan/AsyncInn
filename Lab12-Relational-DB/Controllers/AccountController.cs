using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Lab12_Relational_DB.Model;
using Lab12_Relational_DB.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab12_Relational_DB.Controllers
{
    /// <summary>
    /// This API controller handles User Identity, 
    /// including user registration and user login
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// This custom API route handles user registration to the application
        /// while implementing a RegisterDTO
        /// </summary>
        /// <param name="register">A unique RegisterDTO object</param>
        /// <returns>Task result: either an Ok or a BadRequest message</returns>
        // api/account/register
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {   
            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                UserName = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName
            };

            // Create the user
            var result = await _userManager.CreateAsync(user, register.Password);

            if(result.Succeeded)
            {
                //sign the user in if it was successful.
                await _signInManager.SignInAsync(user, false);

                return Ok();
            }

            return BadRequest("Invalid Registration");

        }

        /// <summary>
        /// This custom API route handles user login to the application
        /// while implementing a LoginDTO
        /// </summary>
        /// <param name="login">A unique LoginDTO object</param>
        /// <returns>Task result: either an Ok or a BadRequest message</returns>
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
             if(result.Succeeded)
            {
                return Ok("logged in");
            }

            return BadRequest("Invalid attempt");

        }
    }
}
