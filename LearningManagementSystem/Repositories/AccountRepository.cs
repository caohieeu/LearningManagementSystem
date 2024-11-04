using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Internal;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        public AccountRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            LMSContext context,
            IMapper mapper) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _mapper = mapper;
        }

        string GenerateToken(SignInDto model, List<Claim> listClaim)
        {
            var authClaims = new List<Claim>
            {
                new Claim("UserName", model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach(var claim in listClaim)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, claim.Value));
            }

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
            var user = await _userManager.FindByNameAsync(model.UserName);
            var passwordValid = await _userManager.CheckPasswordAsync
                (user, model.Password);

            if(user == null || !passwordValid)
            {
                return authResponseDto;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var listClaim = new List<Claim>();
            foreach (var role in userRoles)
            {
                listClaim.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            authResponseDto.result = true;
            authResponseDto.token = GenerateToken(model, listClaim);

            return authResponseDto;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpDto model)
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
                DepartmentId = model.DepartmentId
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                if(!await _roleManager.RoleExistsAsync(Utils.Roles.Teacher))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Utils.Roles.Teacher));
                }

                await _userManager.AddToRoleAsync(user, Utils.Roles.Teacher);
            }

            return result;
        }
        public Dictionary<string, string> GetTokenInfo(string token)
        {
            try
            {
                var tokenInfo = new Dictionary<string, string>();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var claims = jwtSecurityToken.Claims.ToList();

                foreach (var claim in claims)
                {
                    tokenInfo.Add(claim.Type, claim.Value);
                }

                return tokenInfo;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserResponseDto> GetByUsername(string username)
        {
            return _mapper.Map<UserResponseDto>(await _context.Users
                .Where(x => x.UserName == username)
                .FirstOrDefaultAsync());
        }
    }
}
