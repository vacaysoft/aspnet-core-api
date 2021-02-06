using System.Threading.Tasks;
using VacaySoft.Application.DataTransferObjects.UserProfile;

namespace VacaySoft.Application.Services
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse> CreateUserProfile(CreateUserProfileRequest request);
    }
}
