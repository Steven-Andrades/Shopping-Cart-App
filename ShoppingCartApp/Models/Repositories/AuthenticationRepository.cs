using ShoppingCartApp.Models;
using ShoppingCartApp.Models.Data;
using ShoppingCartApp.Models.DTOs;

namespace ShoppingCartApp.Models.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ShoppingCartDBContext _context;
        public AuthenticationRepository(ShoppingCartDBContext context)
        {
            _context = context;
        }

        public AppUserId Authenticate(LoginDTO loginDTO)
        {
            var userDetails = _context.AppUsers.Where(u => u.Email == loginDTO.Email)
                                               .FirstOrDefault();
            if (userDetails == null) 
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.EnhancedVerify(loginDTO.Password, userDetails.PasswordHash))
            {
                return userDetails;
            }

            return null;
        }

        public AppUserId CreateUser(CreateUserDTO userDTO)
        {
            var userDetails = _context.AppUsers.Where(u => u.Email == userDTO.Email)
                                               .FirstOrDefault();
            if (userDetails != null)
            {
                return null;
            }

            var user = new AppUserId 
            {
                Email = userDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(userDTO.Password)
            };

            _context.AppUsers.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
