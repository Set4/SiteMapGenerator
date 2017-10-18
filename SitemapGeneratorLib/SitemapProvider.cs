using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SitemapGenerator
{

    public class SitemapProvider : ISitemapProvider
    {
        public byte[] GeGZipedSitemapFile(int number)
        {
            try
            {
                if (number < 1)
                    throw new Exception("The number can not be less than one");
                List<SiteMap> items = DBHelper.Instance.GetIntervalSiteMaps(number).ToList();
                byte[] compressedXMLFile = CompressHelper.GetGZipFile( SitemapXmlGenerator.GetSiteMap(items));
                return compressedXMLFile;
            }
            catch
            {
                throw;
            }
        }

        public byte[] GetGeneralSitemapFile()
        {
            try
            {
                List<SiteMapFileInformation> generateFilesInformation = GetSiteMapFilesInformation();
                byte[] xmlFile = SitemapXmlGenerator.GetSitemapGeneral(generateFilesInformation);
                return xmlFile;
            }
            catch
            {
                throw;
            }
        }

        private List<SiteMapFileInformation> GetSiteMapFilesInformation()
        {
            List<SiteMapFileInformation> items = new List<SiteMapFileInformation>();
           
            var countGenerateFiles = Math.Ceiling((double)DBHelper.Instance.GetCountSiteMapItems() / (double)Settings.Instance.CountItemsPerPage);

            for (int i = 1; i < countGenerateFiles; i++)
                items.Add(new SiteMapFileInformation
                {
                    LastDate = DBHelper.Instance.GetLastDateFromIntervalSiteMaps(i),
                    NameFile = Settings.Instance.SitemapIdentefiter + i + ".xml.gz"
                });
                
            return items;
        }
    }
}
