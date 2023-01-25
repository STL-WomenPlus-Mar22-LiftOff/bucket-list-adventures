using BucketListAdventures.Data;
using static BucketListAdventures.Models.ClimateNormals;

namespace ClimateNormalsTests
{
    [TestClass]
    public class ClimateNormalsTests
    {
        public ClimateNormals climateNormals = new ClimateNormals();


        [TestMethod]
        public void ReadCsvDataReturnsSomething()
        {
            IEnumerable<MonthlyData> sampleData = ReadCsvData("AQW00061705");
            Assert.IsTrue(sampleData.Count() > 0, "Expected a non-empty collection.");
        }

        [TestMethod]
        public void ReadCsvDataReturnsTwelveObjects()
        {
            IEnumerable<MonthlyData> sampleData = ReadCsvData("AQW00061705");
            Assert.AreEqual(sampleData.Count(), 12);
        }
    }
}