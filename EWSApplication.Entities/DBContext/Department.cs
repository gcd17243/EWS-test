namespace EWSApplication.Entities.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        
        public int Departmentid { get; set; }

        [Required]
        [StringLength(100)]
        public string Departmentname { get; set; }

        public DateTime? Departmentyear { get; set; }
    }
}
