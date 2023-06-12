using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;
using System;
using System.IO;

namespace WpfApp1.Services
{
    public class XmlLoaderService<T> : IXmlLoaderService<T>
    {
        private readonly IExtendedXmlSerializer serializer;

        public XmlLoaderService()
        {
            serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableImplicitTyping(typeof(T))
                .Type<DateTime?>()
                .Register()
                .Converter()
                .Using(new NullableDateTimeConverter())
                .Create();
        }

        public T LoadXml(string path)
        {
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    return serializer.Deserialize<T>(reader);
                }
            }
            else
            {
                return default;
            }
        }
    }
}