using ExtendedXmlSerializer.ContentModel.Conversion;
using System;

namespace WpfApp1
{
    public class NullableDateTimeConverter : ConverterBase<DateTime?>
    {
        public override string Format(DateTime? instance)
        {
            return instance.HasValue ? instance.ToString() : string.Empty;
        }

        public override DateTime? Parse(string data)
        {
            return string.IsNullOrWhiteSpace(data) ? null : DateTime.Parse(data);
        }
    }
}