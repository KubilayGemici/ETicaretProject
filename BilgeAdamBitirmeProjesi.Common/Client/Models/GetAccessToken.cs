using System;
using System.Collections.Generic;
using System.Text;

namespace BilgeAdamBitirmeProjesi.Common.Client.Models
{
    public class GetAccessToken
    {
        //Kullanıcı login olduktan sonra user nesnesi olarak döndürücem.
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        //Token Bitiş Süresi
        public long Expires { get; set; }
        //Kullanıcı Şifre olmadan yeniden token alıyorum.
        public string RefreshToken { get; set; }
    }
}
