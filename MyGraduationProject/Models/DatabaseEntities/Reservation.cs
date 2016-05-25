namespace MyGraduationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RESERVATION_ID { get; set; }

        public DateTime? DATE_FROM { get; set; }

        public DateTime? DATE_TO { get; set; }

        public DateTime? ORDER_DATE { get; set; }

        public decimal? OVERALL_PRICE { get; set; }

        public int? USER_ID { get; set; }

        public int? ITEM_ID { get; set; }

        public int? STATUS_ID { get; set; }

        public virtual Item Item { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }

        public virtual User User { get; set; }
    }
}
