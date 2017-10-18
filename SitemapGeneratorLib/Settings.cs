using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SitemapGenerator
{
    public class Settings
    {
        private static readonly Object s_lock = new Object();
        private static Settings instance = null;

        private Settings()
        {
            //inicialize
            try
            {
                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SitemapsNamespace"]))
                    SitemapsNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
                else
                    SitemapsNamespace = ConfigurationManager.AppSettings["SitemapsNamespace"];

                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DomainName"]))
                    DomainName = "https://";
                else
                    DomainName = ConfigurationManager.AppSettings["DomainName"];

                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SitemapIdentefiter"]))
                    SitemapIdentefiter = "sitemap-";
                else
                    SitemapIdentefiter = ConfigurationManager.AppSettings["SitemapIdentefiter"];

                int countItemsPerPage;
                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["CountItemsPerPage"]) || int.TryParse(ConfigurationManager.AppSettings["CountItemsPerPage"], out countItemsPerPage))
                    CountItemsPerPage = 50000;
                else
                    CountItemsPerPage = countItemsPerPage;


                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DBSource"]))
                    DBSource = "185..";
                else
                    DBSource = ConfigurationManager.AppSettings["DBSource"];

                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DBUserName"]))
                    DBUserName = "..";
                else
                    DBUserName = ConfigurationManager.AppSettings["DBUserName"];

                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DBPassword"]))
                    DBPassword = "...";
                else
                    DBPassword = ConfigurationManager.AppSettings["DBPassword"];
            } 
            catch (Exception ex)
            {
                throw new SettingsException("Initialize Settings error", ex);
            }
        }
        public static Settings Instance
        {
            get
            {
                if (instance != null) return instance;
                Monitor.Enter(s_lock);
                Settings temp = new Settings();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);
                return instance;
            }
        }


        public string SitemapsNamespace { get; private set; }
        public string DomainName { get; private set; }
        public string SitemapIdentefiter { get; private set; }
        public int CountItemsPerPage { get; private set; }

        public string DBSource { get; private set; }
        public string DBUserName { get; private set; }
        public string DBPassword { get; private set; }
    }
    public class SettingsException : Exception
    {
        public SettingsException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public SettingsException(string message) : base(message)
        {
        }
    }
}
