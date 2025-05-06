using DimiTours.Enum;
using System.ComponentModel.DataAnnotations;

namespace DimiTours.Models
{
    public class User
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        [RegularExpression(@"^\S(.*\S)?$", ErrorMessage = "Username cannot be blank or whitespace.")]
        public string UserName { get; set; }


        [Required]
        [MaxLength(512)]
        public string Password { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }


        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }


        [Required]
        [MaxLength(50)]
        public string Email { get; set; }


    }
}
