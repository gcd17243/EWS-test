namespace EWSApplication.Entities.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [StringLength(10)]
        public string Staffid { get; set; }

        [Required]
        [StringLength(100)]
        public string Staffname { get; set; }

        [Required]
        [StringLength(10)]
        public string Staffgender { get; set; }

        public DateTime Staffdob { get; set; }

        [Required]
        [StringLength(10)]
        public string Departmentid { get; set; }

        [Required]
        [StringLength(10)]
        public string userid { get; set; }

        [Required]
        [StringLength(10)]
        public string QAmid { get; set; }
    }
}
