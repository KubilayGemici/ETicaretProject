namespace BilgeAdamBitirmeProjesi.Common.Client.Models
{
    public class LoginRequest
    {
        //Kullanıcı adı şifre doğruysa access token doldurup geri dönecek.
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
