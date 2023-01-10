using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Net;
using System.IO;

namespace BucketListAdventures.Models
{
    public class ClimateNormals
    {
        public class MonthlyData
        {
            public int DATE { get; set; }
            public double MLY_TAVG_NORMAL { get; set;}
            public double MLY_TMAX_NORMAL { get; set;}
            public double MLY_TMIN_NORMAL { get; set;}

        }

        public static IEnumerable<MonthlyData> GetClimateNormals(string stationId)
        {
            IEnumerable<MonthlyData> records;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.Replace('_','-'),
            };
            
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(5.0);
                //"@" symbol is for raw string literal
                var requestUrl = $@"https://noaanormals.blob.core.windows.net/climate-normals/normals-monthly/1991-2020/access/{stationId}.csv";
                var stream = httpClient.GetStreamAsync(requestUrl).Result;

                using (var reader = new StreamReader(stream))
                {
                    using (var csv = new CsvReader(reader, config))
                    {
                        records = csv.GetRecords<MonthlyData>().ToList();
                    }
                }
            }
            return records;
        }
    }
}
