using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using testingUK.Model.Dto;
using testingUK.Repositories;

namespace testingUK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController( UserManager<IdentityUser> userManager , ITokenRepository  tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Username

            };
            Console.WriteLine(identityUser);
            Console.WriteLine(registerRequest.Password);
            var identityResult = await userManager.CreateAsync(identityUser, registerRequest.Password);

            Console.WriteLine("----------------------------------------------------------------------");
            if (identityResult.Succeeded)
            {
                Console.WriteLine(1);
                if(registerRequest.Roles != null && registerRequest.Roles.Length != 0)
                {
                    Console.WriteLine(2);
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequest.Roles);

                    if (identityResult.Succeeded)
                    {
                        Console.WriteLine("***********************************************************");
                        return Ok(new { message = "Signup ... from backend" });

                    }

                }

            }
            return BadRequest(new { message = "Something went wrong !!" });

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if(user!=null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for this user 
                    var roles = await userManager.GetRolesAsync(user);
                    
                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(new { message = jwtToken });
                    }

                } 
            }


            return BadRequest(new { message = "Something went wrong !!" });
        }

    }
}
