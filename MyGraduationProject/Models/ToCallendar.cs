using System;
using System.ComponentModel.DataAnnotations;

namespace MyGraduationProject.Models
{
    public class ToCallendar
    {
        public int id { get; set; }

        public string title { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }
    }
}