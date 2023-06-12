using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfApp1;

namespace WpfApp1Tests
{
    [TestClass]
    public class RelayCommandTests
    {
        [TestMethod]
        public void Execute_WithoutCondition_ShouldExecute()
        {
            // Arrange
            var executed = false;
            var execute = new Action<object>((parameter) => executed = true);
            var command = new RelayCommand((parameter) => true, execute);
            var eventHandler = new EventHandler((a, b) => { });
            command.CanExecuteChanged += eventHandler;

            // Act
            command.CanExecute(null);
            command.Execute(null);
            command.CanExecuteChanged -= eventHandler;

            // Assert
            Assert.IsTrue(executed);
        }

        [TestMethod]
        public void Execute_WithConditionAndParameter_ShouldExecute()
        {
            // Arrange
            var executed = string.Empty;
            var execute = new Action<object>((parameter) => executed = parameter.ToString());
            var command = new RelayCommand((parameter) => parameter != null, execute);
            var eventHandler = new EventHandler((a, b) => { });
            command.CanExecuteChanged += eventHandler;

            // Act
            command.CanExecute(nameof(Execute_WithConditionAndParameter_ShouldExecute));
            command.Execute(nameof(Execute_WithConditionAndParameter_ShouldExecute));
            command.CanExecuteChanged -= eventHandler;

            // Assert
            Assert.AreEqual(nameof(Execute_WithConditionAndParameter_ShouldExecute), executed);
        }
    }
}