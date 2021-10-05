using EWSApplication.Entities.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWSApplication.DataLayers.Common
{
    public class StructurePostToRender : Post
    {
        public string username { get; set; }
    }
}
