using ThuvienMvc.Dtos.UserDtos;
using ThuvienMvc.Models;
using X.PagedList;

namespace ThuvienMvc.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> SignUp(CreateUserDto input);
        public Task<string> SignIn(SignInUserDto input );
        IPagedList<User> GetPagedUser(string name, int page, int pageSize);
        void Update(UserDto input);
        void UpdateAdmin(Admin inpput);
        User GetById(int id);
        Admin GetAdmin(int id );
        void delete(int id);
    }
}
