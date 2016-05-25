namespace MyGraduationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UsersAdress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsersAdress()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int ADRESS_ID { get; set; }

        [StringLength(50)]
        public string STREET_NAME { get; set; }

        [StringLength(50)]
        public string STREET_NUMBER { get; set; }

        public short? POSSESION_NUMBER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
