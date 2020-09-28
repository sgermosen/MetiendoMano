using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Model.Domain;

namespace Auth.Services
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Lastname),
                new Claim(ClaimTypes.Uri, user.SeoUrl),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.SeoUrl),
                new Claim("ImageProfile", user.Image ?? "")
            };

            var roles = _userManager.GetRolesAsync(user).Result;

            // Seteamos el primer rol encontrado
            if (roles != null && roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.First()));
            }

            context.IssuedClaims.AddRange(claims);

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;

            // Su lógica para validar si el usuario tiene acceso al sistema o no
            /*
             context.IsActive = !user.IsBanned;
             */

            context.IsActive = true;

            return Task.FromResult(0);
        }
    }
}
