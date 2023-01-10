using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace BucketListAdventures.Models
{
    public class ClimateNormals
    {
        public class MonthlyData
        {
            public int DATE;
            public double MLY_TAVG_NORMAL;
            public double MLY_TMAX_NORMAL;
            public double MLY_TMIN_NORMAL;

        }

        public static IEnumerable<MonthlyData> GetClimateNormals()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.Replace('-','_'),
            };
            //"@" symbol is for raw string literal
            using (var reader = new StreamReader(@"https://noaanormals.blob.core.windows.net/climate-normals/normals-monthly/1991-2020/access/AQW00061705.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<MonthlyData>();
                return records;
            }
        }
    }
}
