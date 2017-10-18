using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapGenerator
{
    class SiteMap
    {
        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        public string Identifier { get; set; }
        public DateTime LastMod { get; set; }
    }
}
