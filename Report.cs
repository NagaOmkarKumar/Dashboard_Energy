using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JBM_OGIHARA
{
    class Report
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String MeterName { get; set; }
        public String DateTimeNow { get; set; }
        public String? VAReadings { get; set; }

        public static DateTime StringToDate(string theDateInStringFormat)
        {
            DateTime result;
            if (DateTime.TryParse(theDateInStringFormat, out result))
            {
                return result;
            }

            return new DateTime();
        }
    }
}
