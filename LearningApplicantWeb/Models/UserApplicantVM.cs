using LearningApplicantWeb.Models.EF;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection;

namespace LearningApplicantWeb.Models
{
    public class UserApplicantVM
    {
        public class Index
        { 
            public int PositionId { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Nik { get; set; } = null!;
            public string BirthPlace { get; set; } = null!;
            public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));
            public int Gender { get; set; }
            public string Address { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Education { get; set; } = null!;
            public List<EF.JobPosition> JobPositions { get; set; } = new List<EF.JobPosition>();
            public Index()
            {
                JobPositions = JobPositionVM.Method.GetDataAll();
            }
        }

        
        public class Method
        {
            public static string Insert(Index input)
            {
                var context = DBClass.GetContext();

                if (string.IsNullOrWhiteSpace(input.FirstName)) throw new Exception("Nama depan wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.LastName)) throw new Exception("Nama belakang wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Nik)) throw new Exception("NIK wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.BirthPlace)) throw new Exception("Tempat lahir wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Address)) throw new Exception("Alamat wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Phone)) throw new Exception("No. HP wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Email)) throw new Exception("Email wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Education)) throw new Exception("Pendidikan wajib diisi.");

                var position = context.JobPositions.FirstOrDefault(p => p.PositionId == input.PositionId && p.DeletedAt == null);
                if (position == null)
                {
                    throw new Exception("Posisi jabatan tidak ditemukan.");
                }

                if (context.Applicants.Any(a => a.Nik == input.Nik))
                {
                    throw new Exception("NIK sudah terdaftar.");
                }

                if (context.Applicants.Any(a => a.Email.ToLower() == input.Email.ToLower()))
                {
                    throw new Exception("Email sudah terdaftar.");
                }

                // method untuk melakukan validasi jika register code sudah ada maka lakukan generate lagi hingga tidak ada yang sama
                string registerCode = "";
                do
                {
                    registerCode = Helpers.GenerateAlphanumericCode();
                }
                while (context.Applicants.Any(x => x.RegisterCode == registerCode));

                var newRow = new Applicant
                {
                    PositionId = input.PositionId,
                    RegisterCode = registerCode,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Nik = input.Nik,
                    BirthPlace = input.BirthPlace,
                    BirthDate = input.BirthDate,
                    Gender = input.Gender,
                    Address = input.Address,
                    Phone = input.Phone,
                    Email = input.Email,
                    Education = input.Education,
                    CreatedBy = $"{input.Nik}",
                    CreatedAt = DateTime.Now
                };

                context.Applicants.Add(newRow);
                context.SaveChanges();

                return registerCode; // Mengembalikan kode pendaftaran yang baru dibuat
            }

            public static ApplicantStatus CheckStatus(string registerCode)
            {
                var context = DBClass.GetContext();
                var data = context.Applicants
                    .Include(x => x.ApplicantStatus)
                    .Where(x => x.RegisterCode == registerCode)
                    .FirstOrDefault();

                if(data == null)
                {
                    throw new Exception("Pastikan Register Code Sudah Terdaftar");
                }

                if(data.ApplicantStatus == null)
                {
                    throw new Exception("Pelamar Masih Dalam tahap Proses");
                }

                return data.ApplicantStatus;
            }
        }
    }
}
