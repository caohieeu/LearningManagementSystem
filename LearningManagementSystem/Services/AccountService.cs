using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LearningManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly LMSContext _context;
        public AccountService(
            IAccountRepository accountRepository,
            IMapper mapper,
            IConfiguration config,
            LMSContext context)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _config = config;
            _context = context;
        }

        public async Task<AuthResponseDto> SignInAsync(SignInDto model)
        {
            return await _accountRepository.SignInAsync(model);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpDto model)
        {
            return await _accountRepository.SignUpAsync(model);
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
                response.Valid = true;

                if (tokenInfo.TryGetValue("UserName", out username))
                {
                    response.User = await _accountRepository.GetByUsername(username);
                }

                return response;
            }

            return response;
        }

        public bool CheckToken(string token)
        {
            return true;
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
    }
}
