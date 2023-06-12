using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.Services;

namespace WpfApp1Tests.Services
{
    [TestClass]
    public class DBAccessTests
    {
        [TestMethod]
        public void OnConfiguring_ShouldSetSqlServer()
        {
            // Arrange
            // Act
            var dBAccess = new DBAccess();

            // Assert
            Assert.IsTrue(dBAccess.Database.IsSqlServer());
        }
    }
}