using MVC.Dto;
using MVC.Repository;

namespace MVC.Service
{
    public class AuthService
    {
        public readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(LoginDTO loginDto)
        {
            if (loginDto.Email != null)
            {
                var user = _userRepository.GetUserByEmail(loginDto.Email);

                if (user.Result.Password == loginDto.Password)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
