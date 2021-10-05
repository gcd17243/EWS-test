namespace EWSApplication.Entities.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int commentid { get; set; }

        public bool anonymous { get; set; }

        public DateTime Date { get; set; }

        [StringLength(250)]
        public string Content { get; set; }
    
        public int postid { get; set; }            
        public int userid { get; set; }

    }
}
