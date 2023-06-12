using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfApp1;

namespace WpfApp1Tests
{
    [TestClass]
    public class NullableDateTimeConverterTests
    {
        [TestMethod]
        public void Format_WithNull_ShouldReturnStringEmpty()
        {
            // Arrange
            var converter = new NullableDateTimeConverter();

            // Act
            var result = converter.Format(null);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Format_WithDate_ShouldReturnStringDate()
        {
            // Arrange
            var converter = new NullableDateTimeConverter();
            var now = DateTime.Now;

            // Act
            var result = converter.Format(now);

            // Assert
            Assert.AreEqual(now.ToString(), result);
        }

        [TestMethod]
        public void Parse_WithDate_ShouldReturnStringDate()
        {
            // Arrange
            var converter = new NullableDateTimeConverter();
            var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            // Act
            var result = converter.Parse(now.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(now, result.Value);
        }

        [TestMethod]
        [DataRow(default)]
        [DataRow("")]
        [DataRow("  ")]
        public void Parse_WithWrongString_ShouldReturnNull(string data)
        {
            // Arrange
            var converter = new NullableDateTimeConverter();

            // Act
            var result = converter.Parse(data);

            // Assert
            Assert.IsNull(result);
        }
    }
}