using ThuvienMvc.Dtos.UserDtos;
using ThuvienMvc.Models;

namespace ThuvienMvc.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> SignUp(CreateUserDto input);
        public Task<string> SignIn(SignInUserDto input );
        void Update(UserDto input);
        User GetById(int id);
        void delete(int id);
    }
}
