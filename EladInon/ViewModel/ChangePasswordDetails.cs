using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.ViewModels
{
    public class ChangePasswordDetails
    {
        [Required(ErrorMessage = "שדה זה הוא חובה")]
        [MinLength(8, ErrorMessage = "סיסמה חייבת להיות באורך של 8 תווים לפחות")]
        [MaxLength(200, ErrorMessage = "סיסמה חייבת להיות פחות מ200 תווים")]
        [DataType(DataType.Password)]
        [DisplayName("סיסמה נוכחית")]
        public string Password { get; set; }

        [Required(ErrorMessage = "שדה זה הוא חובה")]
        [MinLength(8, ErrorMessage = "סיסמה חייבת להיות באורך של 8 תווים לפחות")]
        [MaxLength(200, ErrorMessage = "סיסמה חייבת להיות פחות מ200 תווים")]
        [DataType(DataType.Password)]
        [DisplayName("סיסמה חדשה")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "שדה זה הוא חובה")]
        [MinLength(8, ErrorMessage = "סיסמה חייבת להיות באורך של 8 תווים לפחות")]
        [MaxLength(200, ErrorMessage = "סיסמה חייבת להיות פחות מ200 תווים")]
        [DataType(DataType.Password)]
        [DisplayName("חזור על הסיסמה")]
        public string ReNewPassword { get; set; }

    }
}
