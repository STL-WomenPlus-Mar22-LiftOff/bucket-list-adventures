using static BucketListAdventures.Models.ClimateNormals;

namespace ClimateNormalsTests
{
    [TestClass]
    public class ClimateNormals
    {
        [TestMethod]
        public void GetClimateNormalsReturnsTwelveObjects()
        {
            IEnumerable<MonthlyData> data = GetClimateNormals();
            Assert.AreEqual(data.Count(), 12);
            foreach (MonthlyData line in data)
            {
                Console.WriteLine(line);
            }
        }
    }
}