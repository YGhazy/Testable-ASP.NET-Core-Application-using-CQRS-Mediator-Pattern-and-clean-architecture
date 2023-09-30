using AutoMapper;
using Blazor.Application.Helper;
using Blazor.Application.DTOs;
using Blazor.Application.IRepository;
using Blazor.Application.Services.IServices;
using Blazor.Domain.Common;
using Blazor.Domain.Entities;
using blazor_Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Blazor.Application.Common;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Blazor.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly APISettings _aPISettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(IMapper mapper, IUnitOfWork unitOfWork,
                        UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _aPISettings= options.Value;


        }

        public async Task<ApiResponse<bool>> SignUp( SignUpRequestDTO signUpRequestDTO)
        {
            ApiResponse<bool> result_ = new ApiResponse<bool>();

            var user = new ApplicationUser
            {
                UserName = signUpRequestDTO.Email,
                Email = signUpRequestDTO.Email,
                Name = signUpRequestDTO.Name,
                PhoneNumber = signUpRequestDTO.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, signUpRequestDTO.Password);

            if (!result.Succeeded)
            {
                result_.Succeeded = false;
                result_.Errors.AddRange(result.Errors.Select(u => u.Description));
                return result_;
            }

            var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_Customer);
            if (!roleResult.Succeeded)
            {
                result_.Succeeded = false;
                result_.Errors.AddRange(result.Errors.Select(u => u.Description));
                return result_;
            }
            result_.Succeeded = true;
            result_.Data = true;
            return result_;
        }

        public async Task<ApiResponse<SignInResponseDTO>> SignIn( SignInRequestDTO signInRequestDTO)
        {
            ApiResponse<SignInResponseDTO> result_ = new ApiResponse<SignInResponseDTO>();

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.UserName, signInRequestDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.UserName);
                if (user==null)
                {
                    result_.Succeeded = false;
                    result_.Errors.Add("Invalid Authentication");
                    return result_;
                }

                //everything is valid and we need to login 
                var signinCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _aPISettings.ValidIssuer,
                    audience: _aPISettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signinCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                var signInResponseDTO = new SignInResponseDTO
                {

                    IsAuthSuccessful=true,
                    Token = token,
                    UserDTO = new UserDTO()
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    }
                };
                result_.Succeeded = true;
                result_.Data = signInResponseDTO;
                return result_;

            }
            else
            {
                result_.Succeeded = false;
                result_.Errors.Add("Invalid Authentication");
                return result_;
            }
        }


        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("Id",user.Id)
                };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
