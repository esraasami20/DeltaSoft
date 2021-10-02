using DeltaSoft.DTO;
using DeltaSoft.Helper;
using DeltaSoft.Models;
using DeltaSoft.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeltaSoft.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ManageRoles _manageRoles;
        private JwtHelper _jwt;

        public AuthenticationService(UserManager<ApplicationUser> userManager, ManageRoles manageRoles, IOptions<JwtHelper> jwt)
        {
            _manageRoles = manageRoles;
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<ResponseAuth> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.password))
            {
                return new ResponseAuth { Message = "email or password not valid" };
            }
            if (user.AdminLocked)
            {
                return new ResponseAuth { Message = "email or password not valid" };
            }

            var token = await CreateJwtToken(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            return new ResponseAuth
            {
                Email = user.Email,
                UserName = user.Email,
                Role = userRoles[0],
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = token.ValidTo,
                IsAuthenticated = true

            };

        }


        public async Task<ResponseAuth> RegisterAdminAsync(RegisterModel model)
        {
            string username = model.Email.Split('@')[0];
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ResponseAuth { Message = "Email is already Exist" };

            if (await _userManager.FindByNameAsync(username) != null)
                return new ResponseAuth { Message = "Username is already Exist" };

            ApplicationUser user = new ApplicationUser()
            {
                Fname = model.Fname,
                Lname = model.Lname,
                Email = model.Email,
                UserName = username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new ResponseAuth { Message = errors };

            }

            await _manageRoles.AddToSAdminRole(user);
            var token = await CreateJwtToken(user);
            return new ResponseAuth
            {
                Email = user.Email,
                UserName = username,
                Role = "Admin",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = token.ValidTo,
                IsAuthenticated = true

            };
        }



        public async Task<ResponseAuth> RegisterEmployeeAsync(RegisterModel model)
        {
            string username = model.Email.Split('@')[0];

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ResponseAuth { Message = "Email is already Exist" };

            if (await _userManager.FindByNameAsync(username) != null)
                return new ResponseAuth { Message = "Username is already Exist" };

            ApplicationUser user = new ApplicationUser()
            {
                Fname = model.Fname,
                Lname = model.Lname,
                Email = model.Email,
                UserName = username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new ResponseAuth { Message = errors };

            }


            var token = await CreateJwtToken(user);
            return new ResponseAuth
            {
                Email = user.Email,
                UserName = username,
                Role = "Employee",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = token.ValidTo,
                IsAuthenticated = true

            };



        }





        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }



    }
}
