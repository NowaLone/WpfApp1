using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1Tests.ViewModels
{
    [TestClass]
    public class MainViewModelTests
    {
        //[TestMethod]
        //public void ExitCommand_Execute_ShouldSaveAndExit()
        //{
        //    // Arrange
        //    var saved = false;
        //    var dBAccess = new DBAccess();
        //    var eventHandler = new EventHandler<Microsoft.EntityFrameworkCore.SavedChangesEventArgs>((s, a) => { saved = true; });
        //    dBAccess.SavedChanges += eventHandler;
        //    var viewModel = new MainViewModel(null, null, dBAccess);

        //    // Act
        //    viewModel.ExitCommand.Execute(null);
        //    dBAccess.SavedChanges -= eventHandler;

        //    // Assert
        //    Assert.IsTrue(saved);
        //}

        [TestMethod]
        public void SaveCommand_Execute_ShouldSave()
        {
            // Arrange
            var saved = false;
            var dBAccess = new DBAccess();
            var eventHandler = new EventHandler<Microsoft.EntityFrameworkCore.SavedChangesEventArgs>((s, a) => { saved = true; });
            dBAccess.SavedChanges += eventHandler;
            var viewModel = new MainViewModel(null, null, dBAccess);

            // Act
            viewModel.SaveCommand.Execute(null);
            dBAccess.SavedChanges -= eventHandler;

            // Assert
            Assert.IsTrue(saved);
        }

        [TestMethod]
        public void AddConnectionCommand_CanConnect_ShouldAddConnection()
        {
            // Arrange
            var changed = string.Empty;
            var dBAccess = new DBAccess();
            var viewModel = new MainViewModel(null, null, dBAccess);
            var propertyChangedEventHandler = new System.ComponentModel.PropertyChangedEventHandler((s, o) => { changed = o.PropertyName; });
            viewModel.PropertyChanged += propertyChangedEventHandler;

            // Act
            viewModel.AddConnectionCommand.Execute("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            viewModel.PropertyChanged -= propertyChangedEventHandler;

            // Assert
            Assert.IsTrue(viewModel.IsConnected);
            Assert.IsNotNull(viewModel.Cards);
            Assert.AreEqual(nameof(viewModel.Cards), changed);
        }

        [TestMethod]
        public void AddConnectionCommand_CanNotConnect_ShouldNotAddConnection()
        {
            // Arrange
            var changed = string.Empty;
            var dBAccess = new DBAccess();
            var viewModel = new MainViewModel(null, null, dBAccess);
            var propertyChangedEventHandler = new System.ComponentModel.PropertyChangedEventHandler((s, o) => { changed = o.PropertyName; });
            viewModel.PropertyChanged += propertyChangedEventHandler;

            // Act
            viewModel.AddConnectionCommand.Execute("");
            viewModel.PropertyChanged -= propertyChangedEventHandler;

            // Assert
            Assert.IsFalse(viewModel.IsConnected);
            Assert.IsNull(viewModel.Cards);
            Assert.AreEqual(string.Empty, changed);
        }

        [TestMethod]
        public void LoadCommand_WithData_ShouldLoad()
        {
            // Arrange
            var saved = false;
            var dBAccess = new DBAccess();
            var eventHandler = new EventHandler<Microsoft.EntityFrameworkCore.SavedChangesEventArgs>((s, a) => { saved = true; });
            dBAccess.SavedChanges += eventHandler;
            var viewModel = new MainViewModel(new XmlLoaderService<Cards>(), new OpenFileDialogServiceMock("Cards_20211005080948.xml"), dBAccess);

            // Act
            viewModel.AddConnectionCommand.Execute("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            viewModel.Cards.First().LastName = "LoadCommand_WithData_ShouldLoad" + Random.Shared.Next();
            viewModel.Cards.Remove(viewModel.Cards.Last());
            viewModel.SaveCommand.Execute(null);
            viewModel.LoadCommand.Execute("");
            dBAccess.SavedChanges -= eventHandler;

            // Assert
            Assert.IsTrue(saved);
        }

        [TestMethod]
        public void LoadCommand_WithoutData_ShouldNotLoad()
        {
            // Arrange
            var saved = false;
            var dBAccess = new DBAccess();
            var eventHandler = new EventHandler<Microsoft.EntityFrameworkCore.SavedChangesEventArgs>((s, a) => { saved = true; });
            dBAccess.SavedChanges += eventHandler;
            var viewModel = new MainViewModel(new XmlLoaderService<Cards>(), new OpenFileDialogServiceMock(""), dBAccess);

            // Act
            viewModel.AddConnectionCommand.Execute("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=test;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            viewModel.Cards.First().LastName = "LoadCommand_WithoutData_ShouldNotLoad" + Random.Shared.Next();
            viewModel.Cards.Remove(viewModel.Cards.Last());
            viewModel.LoadCommand.Execute("");
            dBAccess.SavedChanges -= eventHandler;

            // Assert
            Assert.IsFalse(saved);
        }

        private class OpenFileDialogServiceMock : IOpenFileDialogService
        {
            private readonly string path;

            public OpenFileDialogServiceMock(string path)
            {
                this.path = path;
            }

            public string Show()
            {
                return path;
            }
        }
    }
}