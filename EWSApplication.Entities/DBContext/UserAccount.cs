namespace EWSApplication.Entities.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        [Key]
        [Required]
        public int userid { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        public int roleid { get; set; }

        [StringLength(100)]
        public string username { get; set; }
        
        public int Departmentid { get; set; }
        
        public DateTime? opentime { get; set; }
    }
}
