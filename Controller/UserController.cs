using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DataAccess;
using WindowsFormsApp1.Dtos;
using WindowsFormsApp1.Repositories;

namespace WindowsFormsApp1.Controller
{
    internal class UserController
    {
        private UserRepository repo;

        public UserController()
        {
            repo = new UserRepository();  
        }
        public List<string> GetITUsers()
        {
            return repo.GetITUsers();
        }
        public UserDto Login(string username, string password)
        {
            return repo.Login(username, password);
        }

        public List<UserDtoAdmin> GetAllUser()
        {
            return repo.GetAllUser();   
        }
 
    }
}
