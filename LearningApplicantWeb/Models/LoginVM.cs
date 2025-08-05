namespace LearningApplicantWeb.Models
{
    public class LoginVM
    {
        public class Index
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public Index()
            {
                
            }
        }

        public class Method
        {
            public static void Authenticate(Index input)
            {
                if (string.IsNullOrEmpty(input.Username) || string.IsNullOrEmpty(input.Password))
                {
                    throw new Exception("Username dan Password tidak boleh kosong");
                }

                var context = DBClass.GetContext();

                var data = context.Users
                    .Where(x => x.Username.ToLower().Trim() == input.Username.ToLower().Trim())
                    .FirstOrDefault();

                if (data == null)
                {
                    throw new Exception("Username tidak ditemukan");
                }

                var passwordHash = Helpers.GeneratePasswordHash(input.Username, input.Password);

                if (data.Password != passwordHash)
                {
                    throw new Exception("Password salah");
                }
            }
        }
    }
}
