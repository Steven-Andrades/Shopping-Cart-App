using ShoppingCartApp.Models;
using ShoppingCartApp.Models.DTOs;

namespace ShoppingCartApp.Models.Repositories
{
    public interface IAuthenticationRepository
    {
        AppUserId Authenticate(LoginDTO loginDTO);
        AppUserId CreateUser(CreateUserDTO userDTO);
    }
}
