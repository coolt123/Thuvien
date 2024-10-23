using ThuvienMvc.Dtos.UserDtos;
using ThuvienMvc.Models;
using ThuvienMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

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

          


        public Admin GetAdmin(int id)
        {
            var admin = _service.admins.SingleOrDefault(a=>a.IdAmin==id);
            if (admin == null)
            {
                return null;
            }
            return admin;
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

        public IPagedList<User> GetPagedUser(string name, int page, int pageSize)
        {
            var users = _service.users.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                string normalizedSearchName = $"%{name}%";

                users = users.Where(e => EF.Functions.Like(EF.Functions.Collate(e.NameUser,"SQL_latin1_Genneral_CP1_CI_AI"),normalizedSearchName));
            }

            return users.ToPagedList(page, pageSize);
        }

        public async Task<string> SignIn(SignInUserDto input)
        {

            var user = await _service.users
            .Where(e => e.UserName == input.UserName )
            .Select(e => new { e.IdUser , e.Password })
            .FirstOrDefaultAsync();


            var admin = await _service.admins
            .Where(e => e.AdminName == input.UserName )
            .Select(e => new { e.IdAmin , e.Password })
            .FirstOrDefaultAsync();

            if (user != null)
            {
                if (user.Password == input.Password)
                {
                    input.IdSign = user.IdUser;
                    return "User";
                }
                else
                {
                    return "IncorrectPasswordUser"; 
                }
            }
            else if (admin != null)
            {
                if (admin.Password == input.Password)
                {
                    input.IdSign = admin.IdAmin;
                    return "Admin";
                }
                else
                {
                    return "IncorrectPasswordAdmin";
                }
            }
            else
            {
                
                return "InvalidUsername";
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


            users.Password = input.Password;
            users.NameUser = input.NameUser;

          
            _service.users.Update(users);
        }
        public void UpdateAdmin(Admin input)
        {
            var admin = _service.admins.SingleOrDefault(a => a.IdAmin == input.IdAmin);

            if (admin == null)
            {
                throw new Exception("");
            }


            admin.Password = input.Password;
            admin.NameAdmin = input.NameAdmin;


            _service.admins.Update(admin);
        }


    }
}
