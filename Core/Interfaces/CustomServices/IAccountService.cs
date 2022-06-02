using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomServices
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterUserDTO data);
        Task<AuthorizationDTO> LoginAsync(string email, string password);
        Task<AuthorizationDTO> CreateRefreshTokenAsync(string userId);
        Task<AuthorizationDTO> RefreshTokenAsync(AuthorizationDTO authorizationDTO);
        Task LogoutAsync(AuthorizationDTO userTokensDTO);
        Task SentResetPasswordTokenAsync(string userEmail);
        //Task ResetPasswordAsync(AuthorChangePasswordDTO userChangePasswordDTO);
    }
}
