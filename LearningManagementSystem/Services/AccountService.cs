using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ArgumentException = LearningManagementSystem.Exceptions.ArgumentException;

namespace LearningManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly LMSContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(
            IAccountRepository accountRepository,
            IMapper mapper,
            IConfiguration config,
            LMSContext context,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _config = config;
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> SignInAsync(SignInDto model)
        {
            return await _accountRepository.SignInAsync(model);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpDto model, IFormFile? avatar)
        {
            return await _accountRepository.SignUpAsync(model, avatar);
        }

        public async Task<List<UserResponseDto>> GetAllUser()
        {
            List<UserResponseDto> listUserResponse = new List<UserResponseDto>();

            var listUser = await _accountRepository.GetAll();
            foreach(var item in listUser)
            {
                listUserResponse.Add(_mapper.Map<UserResponseDto>(item));
            }

            return listUserResponse;
        }

        public async Task<IntrospectResponseDto> GetInfoUser(string token)
        {
            var tokenInfo = _accountRepository.GetTokenInfo(token);
            string expiredDate, username;

            IntrospectResponseDto response = new IntrospectResponseDto
            {
                Valid = false,
            };

            if (tokenInfo == null || !tokenInfo.TryGetValue("exp", out expiredDate))
            {
                return response;
            }

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiredDate));
            DateTime dateTime = dateTimeOffset.UtcDateTime;
            if (dateTime > DateTime.UtcNow)
            {

                if (tokenInfo.TryGetValue("UserName", out username))
                {
                    response.Valid = true;
                    response.User = await _accountRepository.GetByUsername(username);
                    response.User.Role = (from ur in _context.UserRoles
                                          join r in _context.Roles on ur.RoleId equals r.Id
                                          where ur.UserId == response.User.Id
                                          select r.Name).FirstOrDefault() ?? "";
                }

                return response;
            }

            return response;
        }
        public async Task<UserResponseDto> GetUserById(string id)
        {
            var user = await _context.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                throw new NotFoundException("Không tìm thấy người dùng");
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> SendMailForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if(user is null)
            {
                throw new NotFoundException("Không tìm thấy người dùng");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine($"Generated Token: {token}");
            var param = new Dictionary<string, string>
            {
                { "email", forgotPasswordDto.Email },
                { "token", token }
            };

            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientUri!, param);

            try
            {
                await _emailSender.SendEmailAsync(
                user.Email,
                "Đặt lại mật khẩu",
                $"Vui lòng nhấp vào link để đặt lại mật khẩu: {callback}");

                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ConfirmChangePassword(ConfirmPasswordDto confirmPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmPasswordDto.Email);

            if(user is null)
            {
                throw new NotFoundException("Không tìm thấy tài khoản người dùng");
            }

            var result = await _userManager.ResetPasswordAsync(user, confirmPasswordDto.Token!, confirmPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                Console.WriteLine($"Errors: {string.Join(", ", errors)}");
            }

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                throw new Exception(string.Join(", ", errors));
            }

            return true;
        }
    }
}
