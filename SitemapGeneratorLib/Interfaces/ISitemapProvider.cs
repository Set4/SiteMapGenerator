using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Xml;

namespace SitemapGenerator
{
    public interface ISitemapProvider
    {
        byte[] GetGeneralSitemapFile();
        byte[] GeGZipedSitemapFile(int page);
    }
}
