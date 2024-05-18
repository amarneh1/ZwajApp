using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZwajApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [StringLength(8,MinimumLength=4,ErrorMessage ="لا يجب أن تزيد كلمة المرور عن اربعة أحرف ولا تقل عن ثمانية")]
        public string Password { get; set; }
    }
}