using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }


        // POST: /api/Auth/Register

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            // we are creating a new identity, so that when the user registers it takes in these parameters, which we just declared.

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                   identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles); //we are registering the user here and assigning them their roles, and then those roles go to the user through the userManager.
                    
                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! You can now login");
                    }
                }
            }
            return BadRequest("Something went wrong"); 

        }
    }
}
