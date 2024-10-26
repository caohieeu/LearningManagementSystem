using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearningManagementSystem.Repositories
{
    public class AccountRepository : Repository<ApplicationUser>, IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly LMSContext _context;
        public AccountRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            LMSContext context) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _context = context;
        }

        string GenerateToken(SignInDto model)
        {
            var authClaims = new List<Claim>
            {
                new Claim("UserName", model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> GenerateNewId()
        {
            var entity = await _context.Users
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync();

            string newId;

            if (entity == null)
            {
                newId = "#DKL1";
            }
            else
            {
                var currentId = entity.Id;
                var idNumber = int.Parse(currentId.Substring(4));

                newId = $"#KDL{idNumber + 1}";
            }
            return newId;
        }
        public async Task<AuthResponseDto> SignInAsync(SignInDto model)
        {
            AuthResponseDto authResponseDto = new AuthResponseDto
            {
                result = false,
                token = null
            };

            var result = await _signInManager.PasswordSignInAsync
                (model.UserName, model.Password, false, false);

            if(!result.Succeeded)
            {
                return authResponseDto;
            }

            authResponseDto.result = true;
            authResponseDto.token = GenerateToken(model);

            return authResponseDto;
        }

        public Task<IdentityResult> SignUpAsync(SignUpDto model)
        {
            var user = new ApplicationUser
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Gender = model.Gender,
            };

            return _userManager.CreateAsync(user, model.Password);
        }
    }
}
