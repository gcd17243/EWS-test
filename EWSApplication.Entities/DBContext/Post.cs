namespace EWSApplication.Entities.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public int postid { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        public bool? anonymous { get; set; }

        [StringLength(10)]
        public string tag { get; set; }
        public int userid { get; set; }
        [StringLength(250)]
        public string content { get; set; }

        public int view { get; set; }

        public int  like { get; set; }
        public int dislike { get; set; }
        public DateTime datetimepost { get; set; }
        [StringLength(250)]
        public string filePath { get; set; }
        public bool isActive { get; set; }
    }
}
