using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1Tests.Services
{
    [TestClass]
    public class XmlLoaderServiceTests
    {
        [TestMethod]
        public void LoadXml_FileExists_ShouldReturnData()
        {
            // Arrange
            var service = new XmlLoaderService<Cards>();

            // Act
            var result = service.LoadXml("Cards_20211005080948.xml");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(67, result.Count);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Card));
            CollectionAssert.AllItemsAreNotNull(result);
            CollectionAssert.AllItemsAreUnique(result);
        }

        [TestMethod]
        public void LoadXml_FileNotExists_ShouldReturnNull()
        {
            // Arrange
            var service = new XmlLoaderService<Cards>();

            // Act
            var result = service.LoadXml("xml");

            // Assert
            Assert.IsNull(result);
        }
    }
}