namespace WpfApp1.Services
{
    public interface IXmlLoaderService<T>
    {
        T LoadXml(string path);
    }
}