using EWSApplication.Entities.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWSApplication.DataLayers.Common
{
    public class StructureCommentToRender : Comment
    {
        public string username { get; set; }
        public int roleid { get; set; }
    }
}
