using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBM_OGIHARA
{
    class units
    {

        public string Date { get; set; }
        public string Time { get; set; }

        public string Day { get; set; }

        public double Value { get; set; }

        public static IEnumerable<Report> QueryValuations(SQLiteConnection db, Report report)
        {
            return db.Query<Report>("select * from Report where Id = ?", report.Id);
        }
    }
}
