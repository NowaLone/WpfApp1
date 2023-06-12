using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace WpfApp1.Models
{
    [XmlRoot("Card")]
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [XmlAttribute("CARDCODE")]
        public long CardCode { get; set; }

        [XmlAttribute("STARTDATE")]
        public DateTime? StartDate { get; set; }

        [XmlAttribute("FINISHDATE")]
        public DateTime? FinishDate { get; set; }

        [XmlAttribute("LASTNAME")]
        public string LastName { get; set; }

        [XmlAttribute("FIRSTNAME")]
        public string FirstName { get; set; }

        [XmlAttribute("SURNAME")]
        public string SurName { get; set; }

        [XmlAttribute("FULLNAME")]
        public string FullName { get; set; }

        [XmlAttribute("GENDERID")]
        public int GenderId { get; set; }

        [XmlAttribute("BIRTHDAY")]
        public DateTime? Birthday { get; set; }

        [XmlAttribute("PHONEHOME")]
        public string PhoneHome { get; set; }

        [XmlAttribute("PHONEMOBIL")]
        public string PhoneMobil { get; set; }

        [XmlAttribute("EMAIL")]
        public string Email { get; set; }

        [XmlAttribute("CITY")]
        public string City { get; set; }

        [XmlAttribute("STREET")]
        public string Street { get; set; }

        [XmlAttribute("HOUSE")]
        public string House { get; set; }

        [XmlAttribute("APARTMENT")]
        public string Apartment { get; set; }

        [XmlAttribute("ALTADDRESS")]
        public string AltAddress { get; set; }

        [XmlAttribute("CARDTYPE")]
        public string CardType { get; set; }

        [XmlAttribute("OWNERGUID")]
        public Guid OwnerGuid { get; set; }

        [XmlAttribute("CARDPER")]
        public int Cardper { get; set; }

        [XmlAttribute("TURNOVER")]
        public double Turnover { get; set; }
    }
}