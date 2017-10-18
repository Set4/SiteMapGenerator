using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapGenerator
{
    class CompressHelper
    {
        public static byte[] GetGZipFile(byte[] file)
        {
            try
            {
                byte[] bytesToCompress;
                using (MemoryStream sourceStream = new MemoryStream())
                {
                    using (GZipStream compressionStream = new GZipStream(sourceStream, CompressionMode.Compress))
                    { 
                        compressionStream.Write(file, 0, file.Length);
                    }

                    bytesToCompress = sourceStream.ToArray();
                }
                return bytesToCompress;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
