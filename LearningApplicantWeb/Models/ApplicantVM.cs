using LearningApplicantWeb.Models.EF;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection;

namespace LearningApplicantWeb.Models
{
    public class ApplicantVM
    {
        public class Index
        {
            public List<Applicant> Applicants { get; set; } = new List<Applicant>();
            public Index()
            {
                Applicants = Method.GetDataAll();
            }
        }

        public class Create
        { 
            public int PositionId { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Nik { get; set; } = null!;
            public string BirthPlace { get; set; } = null!;
            public DateOnly BirthDate { get; set; }
            public int Gender { get; set; }
            public string Address { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Education { get; set; } = null!;
            public Create()
            {

            }
        }

        public class Edit
        {
            public int Id { get; set; }
            public int PositionId { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Nik { get; set; } = null!;
            public string BirthPlace { get; set; } = null!;
            public DateOnly BirthDate { get; set; }
            public int Gender { get; set; }
            public string Address { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Education { get; set; } = null!;
            public Edit()
            {

            }
            public Edit(int id)
            {
                var data = Method.GetDataById(id);
                if (data == null)
                {
                    throw new Exception("Applicant not found or already deleted.");
                }

                Id = data.ApplicantId;
                PositionId = data.PositionId;
                FirstName = data.FirstName;
                LastName = data.LastName;
                Nik = data.Nik;
                BirthPlace = data.BirthPlace;
                BirthDate = data.BirthDate;
                Gender = data.Gender;
                Address = data.Address;
                Phone = data.Phone;
                Email = data.Email;
                Education = data.Education;
            }
        }

        public class Delete
        {
            public int Id { get; set; }
            public string ApplicantName { get; set; } = null!;
            public string RegisterCode { get; set; } = null!;
            public Delete()
            {

            }
            public Delete(int id)
            {
                var data = Method.GetDataById(id);
                if (data == null)
                {
                    throw new Exception("Applicant not found or already deleted.");
                }

                Id = data.PositionId;
                ApplicantName = $"{data.FirstName} {data.LastName}";
                RegisterCode = data.RegisterCode;
            }
        }

        public class SetStatus
        {
            public int Id { get; set; }
            public string ApplicantName { get; set; } = null!;
            public string RegisterCode { get; set; } = null!;
            public bool? IsApproved { get; set; }
            public string? Description { get; set; }
            public SetStatus()
            {

            }
            public SetStatus(int id)
            {
                var data = Method.GetDataById(id);
                if (data == null)
                {
                    throw new Exception("Applicant not found or already deleted.");
                }

                Id = data.PositionId;
                ApplicantName = $"{data.FirstName} {data.LastName}";
                RegisterCode = data.RegisterCode;
            }
        }

        public class Method
        {
            public static void Insert(Create input, string sess)
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
                    CreatedBy = sess,
                    CreatedAt = DateTime.Now
                };

                context.Applicants.Add(newRow);
                context.SaveChanges();
            }
            public static void Update(Edit input, string sess)
            {
                var context = DBClass.GetContext();

                // Validasi field wajib
                if (string.IsNullOrWhiteSpace(input.FirstName)) throw new Exception("Nama depan wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.LastName)) throw new Exception("Nama belakang wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Nik)) throw new Exception("NIK wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.BirthPlace)) throw new Exception("Tempat lahir wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Address)) throw new Exception("Alamat wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Phone)) throw new Exception("No. HP wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Email)) throw new Exception("Email wajib diisi.");
                if (string.IsNullOrWhiteSpace(input.Education)) throw new Exception("Pendidikan wajib diisi.");

                // Validasi posisi
                var position = context.JobPositions.FirstOrDefault(p => p.PositionId == input.PositionId);
                if (position == null)
                {
                    throw new Exception("Posisi jabatan tidak ditemukan.");
                }

                // Cari data pelamar
                var existing = context.Applicants.FirstOrDefault(a => a.ApplicantId == input.Id);
                if (existing == null)
                {
                    throw new Exception("Data pelamar tidak ditemukan.");
                }

                // Validasi duplikat NIK
                if (context.Applicants.Any(a => a.Nik == input.Nik && a.ApplicantId != input.Id))
                {
                    throw new Exception("NIK sudah terdaftar oleh pelamar lain.");
                }

                // Validasi duplikat Email
                if (context.Applicants.Any(a => a.Email.ToLower() == input.Email.ToLower() && a.ApplicantId != input.Id))
                {
                    throw new Exception("Email sudah terdaftar oleh pelamar lain.");
                }

                // Lakukan update data
                existing.PositionId = input.PositionId;
                existing.FirstName = input.FirstName;
                existing.LastName = input.LastName;
                existing.Nik = input.Nik;
                existing.BirthPlace = input.BirthPlace;
                existing.BirthDate = input.BirthDate;
                existing.Gender = input.Gender;
                existing.Address = input.Address;
                existing.Phone = input.Phone;
                existing.Email = input.Email;
                existing.Education = input.Education;
                existing.UpdatedBy = sess;
                existing.UpdatedAt = DateTime.Now;

                context.SaveChanges();
            }
            public static void Remove(Delete input, string sess)
            {
                var context = DBClass.GetContext();

                var data = context.Applicants.FirstOrDefault(j => j.PositionId == input.Id);
                if (data != null)
                {
                    context.Applicants.Remove(data);
                    context.SaveChanges();
                }
                else
                {
                    // Opsional: throw atau log jika tidak ditemukan
                    throw new Exception("Applicant not found or already deleted.");
                }
            }
            public static void UpdateStatusApprove(SetStatus input, string sess)
            {
                var context = DBClass.GetContext();
                var data = context.ApplicantStatuses.FirstOrDefault(j => j.ApplicantId == input.Id);
                if (data != null)
                {
                    if(input.IsApproved == null)
                    {
                        throw new Exception("Status approval tidak boleh kosong.");
                    }
                    if (string.IsNullOrWhiteSpace(input.Description))
                    {
                        throw new Exception("Deskripsi tidak boleh kosong.");
                    }

                    data.IsApproved = input.IsApproved ?? false;
                    data.Description = input.Description;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Applicant not found or already deleted.");
                }
            }
            public static List<Applicant> GetDataAll()
            {
                var context = DBClass.GetContext();
                return context.Applicants.Include(x => x.ApplicantStatus).ToList();
            }
            public static Applicant? GetDataById(int id)
            {
                var context = DBClass.GetContext();
                return context.Applicants.FirstOrDefault(j => j.ApplicantId == id);
            }
        }
    }
}
