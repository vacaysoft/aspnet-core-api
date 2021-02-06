using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacaySoft.Application.DataTransferObjects.UserProfile;
using VacaySoft.Application.Services;
using VacaySoft.Domain.Entities;

namespace VacaySoft.Infrastructure.Services
{
    internal class UserProfileService : UserManager<UserProfile>, IUserProfileService
    {
        public UserProfileService(
            IUserStore<UserProfile> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserProfile> passwordHasher,
            IEnumerable<IUserValidator<UserProfile>> userValidators,
            IEnumerable<IPasswordValidator<UserProfile>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<UserProfile>> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<UserProfileResponse> CreateUserProfile(CreateUserProfileRequest request)
        {
            var profile = new UserProfile
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await CreateAsync(profile, request.Password);

            return new UserProfileResponse();
        }
    }
}
