using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace EladInon.Models
{
    public class User
    {
        private static object authDetails;
        private string _password;

        [Required(ErrorMessage = "שדה זה הוא חובה")]
        public int Id { get; set; }

        [Required(ErrorMessage = "שדה זה הוא חובה")]
        [MinLength(4, ErrorMessage = "שם משתמש חייב להיות 4 תווים ומעלה")]
        [MaxLength(20, ErrorMessage = "שם משתמש חייב להיות פחות מ20 תווים")]
        [RegularExpression(@"\w+", ErrorMessage = "שם משתמש חייב להיות מורכב רק צאותיות ומספרים")]
        [DisplayName("שם משתמש")]
        public string UserName { get; set; }

        [MaxLength(20, ErrorMessage = "שם פרטי חייב להיות פחות מ20 תווים")]
        [DisplayName("שם פרטי")]
        public string FirstName { get; set; }
        [MaxLength(20, ErrorMessage = "שם משפחה חייב להיות פחות מ20 תווים")]
        [DisplayName("שם משפחה")]
        public string LastName { get; set; }

        [DisplayName("שם")]
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "שדה זה הוא חובה")]
        [MinLength(8, ErrorMessage = "סיסמה חייבת להיות באורך של 8 תווים לפחות")]
        [MaxLength(200, ErrorMessage = "סיסמה חייבת להיות פחות מ200 תווים")]
        [DataType(DataType.Password)]
        [DisplayName("סיסמה")]
        public string Password {
            get => _password;
            set => _password = EncryptedPassword(value);
        }

        [Required(ErrorMessage = "שדה זה הוא חובה")]
        [EmailAddress (ErrorMessage ="אנא הכנס כתובת מייל לגיטימית")]
        [DisplayName("E-Mail")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "שדה זה הוא חובה")]
        public bool IsAdmin { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }



        public static string EncryptedPassword(string password)
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var sha = new SHA512Managed();
            return Encoding.ASCII.GetString(sha.ComputeHash(passwordBytes));
        }
    }
}
