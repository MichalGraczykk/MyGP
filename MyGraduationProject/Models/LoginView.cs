using DatabaseAccess;
using System.ComponentModel.DataAnnotations;

namespace MyGraduationProject.Models
{
    public class LoginView
    {
            [Required]
            [Display(Name = "LOGIN")]
            public string LOGIN { get; set; }

            [Required]
            [DataType(DataType.Password)]//TODO sprawdz co to 
            [Display(Name = "PASSWORD")]
            public string PASSWORD { get; set; }
    }
}