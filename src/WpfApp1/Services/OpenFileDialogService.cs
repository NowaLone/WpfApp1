using Microsoft.Win32;

namespace WpfApp1.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        public string Show()
        {
            var openFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".xml",
                Filter = "XML (.xml)|*.xml"
            };

            var result = openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}