using LearningApplicantWeb.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace LearningApplicantWeb.Models
{
    public class UserVM
    {
        public class Index
        {
            public List<User> Users { get; set; } = new List<User>();
            public Index()
            {
                Users = Method.GetDataAll();
            }
        }

        public class Create
        {
            public int RoleId { get; set; }
            public string UserName { get; set; } = null!;
            public string Password { get; set; } = null!;
            public List<Role> RoleList { get; set; } = new List<Role>();
            public Create()
            {
                RoleList = RoleVM.Method.GetDataAll();
            }
        }

        public class Edit
        {
            public int RoleId { get; set; }
            public string UserName { get; set; } = null!;
            public string OldPassword { get; set; } = null!;
            public string NewPassword { get; set; } = null!;
            public List<Role> RoleList { get; set; } = new List<Role>();
            public Edit()
            {

            }
            public Edit(string username)
            {
                RoleList = RoleVM.Method.GetDataAll();
                var data = Method.GetDataByUsername(username);
                if(data == null)
                {
                    throw new Exception("User not found or already deleted.");
                }

                RoleId = data.RoleId;
                UserName = data.Username;
            }
        }

        public class Delete
        {
            public string UserName { get; set; } = null!;
            public Delete()
            {

            }
            public Delete(string username)
            {
                var data = Method.GetDataByUsername(username);
                if(data == null)
                {
                    throw new Exception("User not found or already deleted.");
                }

                UserName = data.Username;
            }
        }

        public class Method
        {
            public static void Insert(Create input)
            {
                var context = DBClass.GetContext();

                if (string.IsNullOrWhiteSpace(input.UserName) || string.IsNullOrWhiteSpace(input.Password))
                {
                    throw new ArgumentException("Username and Password cannot be empty.");
                }

                var newRow = new User
                {
                    Username = input.UserName,
                    Password = Helpers.GeneratePasswordHash(input.UserName, input.Password)
                };

                context.Users.Add(newRow);
                context.SaveChanges();
            }

            public static void Update(Edit input, string sess)
            {
                var context = DBClass.GetContext();

                if (string.IsNullOrWhiteSpace(input.UserName) || string.IsNullOrWhiteSpace(input.OldPassword) || string.IsNullOrWhiteSpace(input.NewPassword))
                {
                    throw new ArgumentException("Username, OldPassword, and New Password cannot be empty.");
                }


                var data = context.Users.FirstOrDefault(j => j.Username == input.UserName);
                if (data != null)
                {
                    var oldPasswordHash = Helpers.GeneratePasswordHash(input.UserName, input.OldPassword);
                    if (data.Password != oldPasswordHash)
                    {
                        throw new Exception("Old password is incorrect.");
                    }

                    data.Password = Helpers.GeneratePasswordHash(data.Username, input.NewPassword);
                    context.SaveChanges();
                }
                else
                {
                    // Opsional: throw atau log jika tidak ditemukan
                    throw new Exception("User not found or already deleted.");
                }
            }

            public static void Remove(Delete input)
            {
                var context = DBClass.GetContext();

                var data = context.Users.FirstOrDefault(j => j.Username == input.UserName);
                if (data != null)
                {
                    context.Users.Remove(data);
                    context.SaveChanges();
                }
                else
                {
                    // Opsional: throw atau log jika tidak ditemukan
                    throw new Exception("User not found or already deleted.");
                }
            }

            public static List<User> GetDataAll()
            {
                var context = DBClass.GetContext();
                return context.Users.Include(x => x.Role).ToList();
            }

            public static User? GetDataByUsername(string username)
            {
                var context = DBClass.GetContext();
                return context.Users.FirstOrDefault(j => j.Username == username);
            }


        }
    }
}
