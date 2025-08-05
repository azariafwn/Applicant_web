using LearningApplicantWeb.Models.EF;

namespace LearningApplicantWeb.Models
{
    public class RoleVM
    {
        public class Index
        {
            public List<Role> Roles { get; set; } = new List<Role>();
            public Index()
            {
                Roles = Method.GetDataAll();
            }
        }

        public class Create
        {
            public string RoleName { get; set; } = null!;
            public Create()
            {

            }
        }

        public class Edit
        {
            public int Id { get; set; }
            public string RoleName { get; set; } = null!;
            public Edit()
            {

            }
            public Edit(int id)
            {
                var data = Method.GetDataById(id);
                if(data == null)
                {
                    throw new Exception("Role not found or already deleted.");
                }

                Id = data.RoleId;
                RoleName = data.RoleName;
            }
        }

        public class Delete
        {
            public int Id { get; set; }
            public string RoleName { get; set; } = null!;
            public Delete()
            {

            }
            public Delete(int id)
            {
                var data = Method.GetDataById(id);
                if(data == null)
                {
                    throw new Exception("Role not found or already deleted.");
                }

                Id = data.RoleId;
                RoleName = data.RoleName;
            }
        }

        public class Method
        {
            public static void Insert(Create input)
            {
                var context = DBClass.GetContext();

                if (string.IsNullOrEmpty(input.RoleName))
                {
                    throw new Exception("Nama Jabatan Harap Diisi");
                }

                if (context.Roles.Any(j => j.RoleName.Equals(input.RoleName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("Nama Jabatan Sudah Ada");
                }

                var newRow = new Role
                {
                    RoleId = GenerateNewRoleId(),
                    RoleName = input.RoleName
                };

                context.Roles.Add(newRow);
                context.SaveChanges();
            }

            public static void Update(Edit input)
            {
                var context = DBClass.GetContext();

                if (string.IsNullOrEmpty(input.RoleName))
                {
                    throw new Exception("Nama Jabatan Harap Diisi");
                }

                if (context.Roles.Any(j => j.RoleName.Equals(input.RoleName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("Nama Jabatan Sudah Ada");
                }

                var existing = context.Roles.FirstOrDefault(j => j.RoleId == input.Id);
                if (existing != null)
                {
                    existing.RoleName = input.RoleName;

                    context.SaveChanges();
                }
                else
                {
                    // Opsional: throw atau log jika tidak ditemukan
                    throw new Exception("Role not found or already deleted.");
                }
            }

            public static void Remove(Delete input)
            {
                var context = DBClass.GetContext();

                var data = context.Roles.FirstOrDefault(j => j.RoleId == input.Id);
                if (data != null)
                {
                    context.Roles.Remove(data);
                    context.SaveChanges();
                }
                else
                {
                    // Opsional: throw atau log jika tidak ditemukan
                    throw new Exception("Role not found or already deleted.");
                }
            }

            public static List<Role> GetDataAll()
            {
                var context = DBClass.GetContext();
                return context.Roles.ToList();
            }

            public static Role? GetDataById(int id)
            {
                var context = DBClass.GetContext();
                return context.Roles.FirstOrDefault(j => j.RoleId == id);
            }

            private static int GenerateNewRoleId()
            {
                var context = DBClass.GetContext();
                int maxId = context.Roles.Select(p => (int?)p.RoleId).Max() ?? 0;
                return maxId + 1;
            }

        }
    }
}
