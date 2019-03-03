using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon.ViewModels
{
    public class AuthenticationDetails
    {
        [Required (ErrorMessage = "שדה זה הוא חובה")]
        [MinLength(4,ErrorMessage ="שם משתמש חייב להיות 4 תווים ומעלה")]
        [MaxLength(20,ErrorMessage = "שם משתמש חייב להיות פחות מ20 תווים")]
        [RegularExpression(@"\w+",ErrorMessage ="שם משתמש חייב להיות מורכב רק צאותיות ומספרים")]
        [DisplayName("שם משתמש")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "שדה זה הוא חובה")]
        [MinLength(8,ErrorMessage ="סיסמה חייבת להיות באורך של 8 תווים לפחות")]
        [MaxLength(200,ErrorMessage ="סיסמה חייבת להיות פחות מ200 תווים")]
        [DataType(DataType.Password)]
        [DisplayName("סיסמה")]
        public string Password { get; set; }


        [DisplayName("זכור אותי")]
        public bool IsPersistent { get; set; }
    }
}
