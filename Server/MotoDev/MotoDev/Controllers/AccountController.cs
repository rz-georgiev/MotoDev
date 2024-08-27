using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Core.Dtos;
using MotoDev.Services.Interaces;

namespace MotoDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService userService)
        {
            _accountService = userService;
        }
        
        [HttpPost("Login")]
        public async Task<BaseResponse> Login(LoginRequest request)
        {
            return await
                _accountService.LoginAsync(request);
        }

        [HttpPost("Register")]
        public async Task<BaseResponse> Register(RegisterAccountRequest request)
        {
            return await
                _accountService.RegisterAsync(request);
        }

        [HttpPost("ConfirmAccount")]
        public async Task<BaseResponse> ConfirmAccount(ConfirmAccountRequest request)
        {
            return await
                _accountService.ConfirmAccountAsync(request);
        }

        [HttpPost("ForgottenPassword")]
        public async Task<BaseResponse> ForgottenPassword(ForgottenEmailRequest request)
        {
            return await
                _accountService.ForgottenPasswordAsync(request);
        }

        [HttpPost("ResetPassword")]
        public async Task<BaseResponse> ResetPassword(ResetPasswordRequest request)
        {
            return await
                _accountService.ResetPasswordAsync(request);
        }
    }
}