using ThuvienMvc.Dtos.UserDtos;
using ThuvienMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ThuvienMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ThuvienMvc.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly Data _service;
        public UserService(Data service)
        {
            _service = service;
        }

        public void delete(int id)
        {
            var users = _service.users.SingleOrDefault(a => a.IdUser == id);
            if (users != null)
            {
                _service.users.Remove(users);
                _service.SaveChanges();
            }
        }

        public User GetById(int id)
        {
            var users = _service.users.SingleOrDefault(a=>a.IdUser == id);
            if (users == null)
            {
                return null; 
            }
            return users; 
        }

        public async Task<string> SignIn(SignInUserDto input)
        {

            var user = await _service.users
            .Where(e => e.UserName == input.UserName && e.Password == input.Password)
            .Select(e => new { e.IdUser })
            .FirstOrDefaultAsync();


            var admin = await _service.admins
            .Where(e => e.AdminName == input.UserName && e.Password == input.Password)
            .Select(e => new { e.IdAmin })
            .FirstOrDefaultAsync();

            if (user != null)
            {
                input.IdSign = user.IdUser;
                return "User";
            }
            else if (admin != null)
            {
                 input.IdSign = admin.IdAmin ;
                return "Admin";
            }
            else
            {
                
                return "Invalid username or password.";
            }

        }

        public Task<string> SignUp(CreateUserDto input)
        {
            var user = new User
            {
                UserName = input.UserName,
                Password = input.Password,
                NameUser = input.NameUser,
                Createdat=DateTime.Now,
                Updatedat=DateTime.Now,
                deleteflag=false,

            };
            var check = _service.users.FirstOrDefault(e => e.UserName == input.UserName );
            var admin = _service.admins.FirstOrDefault(e => e.AdminName == input.UserName );
            if (check == null && admin == null) 
            {
                _service.users.Add(user);
                _service.SaveChanges();
                return Task.FromResult("User signed up successfully.");
            }
            
            return Task.FromResult("User registration failed.");

        }

        public void Update(UserDto input)
        {
            var users = _service.users.SingleOrDefault(a => a.IdUser == input.IdUser);
            if (users == null)
            {
                throw new Exception("");
            }

            users.UserName = input.UserName;
            users.Password = input.Password;
            users.NameUser = input.NameUser;

          
            _service.users.Update(users);
        }
    }
}
