using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.Dtos
 {
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [StringLength(8,MinimumLength= 4,ErrorMessage="يجب انا لا تزيد كلمة السر عن8 ولا تقل عن 4")]
        public string Password { get; set; }
    }
}