using AutoMapper;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountService(
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
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
    }
}
