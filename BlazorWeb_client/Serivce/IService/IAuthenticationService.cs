using Blazor.Application.DTOs;

namespace BlazorWeb_Client.Serivce.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequestDTO);
        Task<SignInResponseDTO> Login(SignInRequestDTO signInRequestDTO);
        Task Logout();
    }
}
