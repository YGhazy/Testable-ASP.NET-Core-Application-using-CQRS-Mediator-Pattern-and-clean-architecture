using Blazor.Application.Common;
using Blazor.Application.DTOs;

namespace Blazor.Application.Services.IServices
{
    public interface IAccountService
    {
       Task<ApiResponse<bool>> SignUp(SignUpRequestDTO signUpRequestDTO);
        Task<ApiResponse<SignInResponseDTO>> SignIn(SignInRequestDTO signInRequestDTO);
    }
}
