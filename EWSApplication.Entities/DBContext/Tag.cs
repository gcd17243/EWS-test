namespace EWSApplication.Entities.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tag")]
    public partial class Tag
    {
        [Key]
        [Required]
        public int tagid { get; set; }

        [StringLength(50)]
        public string tagname { get; set; }

        [StringLength(50)]
        public string description { get; set; }
    }
}
