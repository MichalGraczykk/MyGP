namespace MyGraduationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Reservations = new HashSet<Reservation>();
        }

        [Key]
        public int USER_ID { get; set; }

        [StringLength(12)]
        public string LOGIN { get; set; }

        [StringLength(20)]
        public string PASSWORD { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        [StringLength(20)]
        public string SURNAME { get; set; }

        public short? AGE { get; set; }

        public int? ADRESS_ID { get; set; }

        public int? ROLE_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual Role Role { get; set; }

        public virtual UsersAdress UsersAdress { get; set; }
    }
}
