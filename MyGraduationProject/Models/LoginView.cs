using System.ComponentModel.DataAnnotations;

namespace MyGraduationProject.Models
{
    public class LoginView
    {
            [Required]
            [Display(Name = "LOGIN")]
            public string LOGIN { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "PASSWORD")]
            public string PASSWORD { get; set; }
    }
}