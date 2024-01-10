using WarehouseManagementSystem.Domain.Extentions;

namespace WarehouseManagementSystem.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Validate_IEnumerableExtensions()
        {
            var elements = new[] { "Filip", "Sofie", "Mila", "Elise" };
            Assert.AreEqual("Sofie", elements.Find(name => name == "Sofie").First());

            Assert.Null(elements.Find(name => name == "NoName").First());

            //Assert.AreEqual("Filip", elements.Find(name => name == "Filip"));
        }
    }
}