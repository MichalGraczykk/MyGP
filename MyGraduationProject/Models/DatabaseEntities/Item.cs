namespace MyGraduationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Reservations = new HashSet<Reservation>();
        }

        [Key]
        public int ITEM_ID { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        public string DESCRPTION { get; set; }

        public string PHOTO { get; set; }

        public decimal? PRICE_PER_DAY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
