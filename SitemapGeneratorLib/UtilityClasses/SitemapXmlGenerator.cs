using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO.Compression;

namespace SitemapGenerator
{
    class SitemapXmlGenerator
    {
        public static byte[] GetSiteMap(IEnumerable<SiteMap> items)
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = false,
                CloseOutput = false,
                Encoding = Encoding.UTF8,
                ConformanceLevel = ConformanceLevel.Document
            };

            byte[] bytesToCompress;

            try
            {
                using (MemoryStream sourceStream = new MemoryStream())
                {
                    using (XmlWriter writer = XmlWriter.Create(sourceStream, settings))
                    {
                        writer.WriteStartElement("urlset", Settings.Instance.SitemapsNamespace);
                        foreach (var item in items)
                        {
                            writer.WriteStartElement("url");
                            writer.WriteStartElement("loc");
                            writer.WriteString(String.Format("{0}/{1}", Settings.Instance.DomainName, item.Identifier));
                            writer.WriteEndElement();
                            writer.WriteStartElement("lastmod");
                            writer.WriteString(item.LastMod.ToString("yyyy-MM-dd"));
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.Flush();
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

        public static byte[] GetSitemapGeneral(IEnumerable<SiteMapFileInformation> items)
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = false,
                CloseOutput = false,
                Encoding = Encoding.UTF8,
                ConformanceLevel = ConformanceLevel.Document
            };

            byte[] bytesToCompress;

            try
            {
                using (MemoryStream sourceStream = new MemoryStream())
                {
                    using (XmlWriter writer = XmlWriter.Create(sourceStream, settings))
                    {
                        writer.WriteStartElement("sitemapindex", Settings.Instance.SitemapsNamespace);

                        foreach (var item in items)
                        {
                            // создаем новый элемент ("sitemap");
                            writer.WriteStartElement("sitemap", Settings.Instance.SitemapsNamespace);
                           // создаем элементы 
                            writer.WriteStartElement("loc", Settings.Instance.SitemapsNamespace);
 writer.WriteString(String.Format("{0}/sitemap/{1}", Settings.Instance.DomainName, item.NameFile));
                            writer.WriteEndElement();

                            writer.WriteStartElement("lastmod", Settings.Instance.SitemapsNamespace);
                            // создаем текстовые значения для элементов и атрибута          
                            writer.WriteString(item.LastDate.ToString("yyyy-MM-dd"));
                            writer.WriteEndElement();

                            writer.WriteEndElement();

                            
                        }

                        writer.WriteEndElement();
                        writer.Flush();
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
