using System.Collections.Generic;
using System.Xml.Serialization;

namespace WpfApp1.Models
{
    [XmlRoot("Cards")]
    public class Cards : List<Card>
    {
    }
}