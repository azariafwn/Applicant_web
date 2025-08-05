using LearningApplicantWeb.Models.EF;

namespace LearningApplicantWeb.Models
{
    public class JobPositionVM
    {
        public class Index
        {
            public List<JobPosition> JobPositions { get; set; } = new List<JobPosition>();
            public Index()
            {
                JobPositions = Method.GetDataAll();
            }
        }

        public class Create
        {
            public string PositionName { get; set; } = null!;
            public Create()
            {

            }
        }

        public class Edit
        {
            public int Id { get; set; }
            public string PositionName { get; set; } = null!;
            public Edit()
            {

            }
            public Edit(int id)
            {
                var data = Method.GetDataById(id);
                if(data == null)
                {
                    throw new Exception("Job position not found or already deleted.");
                }

                Id = data.PositionId;
                PositionName = data.PositionName;
            }
        }

        public class Delete
        {
            public int Id { get; set; }
            public string PositionName { get; set; } = null!;
            public Delete()
            {

            }
            public Delete(int id)
            {
                var data = Method.GetDataById(id);
                if(data == null)
                {
                    throw new Exception("Job position not found or already deleted.");
                }

                Id = data.PositionId;
                PositionName = data.PositionName;
            }
        }

        public class Method
        {
            public static void Insert(Create input, string sess)
            {
                var context = DBClass.GetContext();

                if (string.IsNullOrEmpty(input.PositionName))
                {
                    throw new Exception("Nama Jabatan Harap Diisi");
                }

                if (context.JobPositions.Any(j => j.PositionName.Equals(input.PositionName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("Nama Jabatan Sudah Ada");
                }

                var newRow = new JobPosition
                {
                    PositionId = GenerateNewPositionId(),
                    PositionName = input.PositionName,
                    CreatedBy = sess,
                    CreatedAt = DateTime.Now
                };

                context.JobPositions.Add(newRow);
                context.SaveChanges();
            }

            public static void Update(Edit input, string sess)
            {
                var context = DBClass.GetContext();

                if (string.IsNullOrEmpty(input.PositionName))
                {
                    throw new Exception("Nama Jabatan Harap Diisi");
                }

                if (context.JobPositions.Any(j => j.PositionName.Equals(input.PositionName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("Nama Jabatan Sudah Ada");
                }

                var existing = context.JobPositions.FirstOrDefault(j => j.PositionId == input.Id);
                if (existing != null)
                {
                    existing.PositionName = input.PositionName;
                    existing.UpdatedBy = sess;
                    existing.UpdatedAt = DateTime.Now;

                    context.SaveChanges();
                }
                else
                {
                    // Opsional: throw atau log jika tidak ditemukan
                    throw new Exception("Job position not found or already deleted.");
                }
            }

            public static void Remove(Delete input, string sess)
            {
                var context = DBClass.GetContext();

                var data = context.JobPositions.FirstOrDefault(j => j.PositionId == input.Id);
                if (data != null)
                {
                    context.JobPositions.Remove(data);
                    context.SaveChanges();
                }
                else
                {
                    // Opsional: throw atau log jika tidak ditemukan
                    throw new Exception("Job position not found or already deleted.");
                }
            }

            public static List<JobPosition> GetDataAll()
            {
                var context = DBClass.GetContext();
                return context.JobPositions.ToList();
            }

            public static JobPosition? GetDataById(int id)
            {
                var context = DBClass.GetContext();
                return context.JobPositions.FirstOrDefault(j => j.PositionId == id);
            }

            private static int GenerateNewPositionId()
            {
                var context = DBClass.GetContext();
                int maxId = context.JobPositions.Select(p => (int?)p.PositionId).Max() ?? 0;
                return maxId + 1;
            }

        }

    }
}
