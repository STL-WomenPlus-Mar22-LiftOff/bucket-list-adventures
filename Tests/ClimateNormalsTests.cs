using static BucketListAdventures.Models.ClimateNormals;

namespace ClimateNormalsTests
{
    [TestClass]
    public class ClimateNormals
    {
        public IEnumerable<MonthlyData> sampleData;

        [TestInitialize]
        public void GetSampleDataset()
        {
            sampleData = GetClimateNormals("AQW00061705");
        }

        [TestMethod]
        public void GetClimateNormalsReturnsSomething()
        {
            Assert.IsTrue(sampleData.Count() > 0, "Expected a non-empty collection.");
        }

        [TestMethod]
        public void GetClimateNormalsReturnsTwelveObjects()
        {
            Assert.AreEqual(sampleData.Count(), 12);
        }
    }
}