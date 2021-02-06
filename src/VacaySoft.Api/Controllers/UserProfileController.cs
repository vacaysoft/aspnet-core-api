using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VacaySoft.Application.DataTransferObjects.UserProfile;
using VacaySoft.Application.Services;

namespace VacaySoft.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _userProfileService.CreateUserProfile(new CreateUserProfileRequest
            {
                Email = "taipan@prasithpongchai.com",
                Password = "g3#^H8ufj0@X"
            });
            return Ok();
        }
    }
}
