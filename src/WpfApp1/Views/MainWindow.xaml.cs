using System.Windows;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel(new XmlLoaderService<Cards>(), new OpenFileDialogService(), new DBAccess());

            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new InputWindow()
            {
                DataContext = DataContext,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            }.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as MainViewModel).SaveCommand.Execute(null);
        }
    }
}