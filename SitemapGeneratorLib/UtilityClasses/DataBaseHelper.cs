using Reputation.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SitemapGenerator
{
    class DBHelper
    {
        private static readonly Object s_lock = new Object();
        private static DBHelper instance = null;

        public static DBHelper Instance
        {
            get
            {
                if (instance != null) return instance;
                Monitor.Enter(s_lock);
                DBHelper temp = new DBHelper();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);
                return instance;
            }
        }

        public DBHelper()
        {
            DataAccess.Config.DBSource = Settings.Instance.DBSource;
            DataAccess.Config.DBUserName = Settings.Instance.DBUserName;
            DataAccess.Config.DBPassword = Settings.Instance.DBPassword;
            DataAccess.Config.DataIsolationLevel = System.Data.IsolationLevel.ReadUncommitted;
        }

        public int GetCountSiteMapItems()
        {
            using (var databaseHelper = new DatabaseHelper(false))
            {
                return databaseHelper.Query<int>("SELECT count(*) FROM [SiteData].[dbo].[SiteMapItems]", null).First();
            }
        }

        public IEnumerable<SiteMap> GetIntervalSiteMaps(int numberInterval)
        {
            using (var databaseHelper = new DatabaseHelper(false))
            {
                return databaseHelper.Query<SiteMap>("SELECT * FROM [SiteData].[dbo].[SiteMapItems] ORDER BY Id OFFSET @skipCount ROWS FETCH NEXT @fetchCount ROWS ONLY ", new { skipCount = --numberInterval * Settings.Instance.CountItemsPerPage, fetchCount = Settings.Instance.CountItemsPerPage }).ToList();
            }
        }

        public DateTime GetLastDateFromIntervalSiteMaps(int numberInterval)
        {
            using (var databaseHelper = new DatabaseHelper(false))
            {
                return databaseHelper.Query<DateTime>("SELECT MAX(LastMod) FROM (SELECT * FROM [SiteData].[dbo].[SiteMapItems] ORDER BY Id OFFSET @skipCount ROWS FETCH NEXT @fetchCount ROWS ONLY) as lastMode", new { skipCount = --numberInterval * Settings.Instance.CountItemsPerPage, fetchCount = Settings.Instance.CountItemsPerPage }).First();
            }
        }
    }
}
