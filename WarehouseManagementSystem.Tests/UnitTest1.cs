using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;
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

        [Test]
        public void Process_HasNullChecks()
        {
            var processor = new OrderProcessorRecord();

            var result = processor.GenerateReportFor(null);
            Assert.True(result == "My Provider" || result == "No Provider");

            result = processor.GenerateReportFor(new OrderRecord(null, null, 0m));
            Assert.True(result == "My Provider" || result == "No Provider");

            result = processor.GenerateReportFor(new OrderRecord(new ShippingProvider(), null, 0m));
            Assert.True(result == "My Provider" || result == "No Provider");

            result = processor.GenerateReportFor(new OrderRecord(new ShippingProvider { Name = "filips shipping provider" }, null, 0m));
            Assert.AreEqual("filips shipping provider", result);


        }
    }
}