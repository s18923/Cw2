using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    public class Student
    {
        [XmlElement(elementName: "fname")]
        [JsonPropertyName("fname")]
        public string Imie { get; set; }

        [XmlElement(elementName: "lname")]
        [JsonPropertyName("lname")]
        public string Nazwisko { get; set; }

        [XmlAttribute(attributeName: "Index")]
        public string Index { get; set; }

        [XmlElement(elementName: "bitrhdate")]
        [JsonPropertyName("bitrhdate")]
        public string Date { get; set; }

        [XmlElement(elementName: "email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [XmlElement(elementName: "motherName")]
        [JsonPropertyName("motherName")]
        public string ImieMatki { get; set; }

        [XmlElement(elementName: "fatherName")]
        [JsonPropertyName("fatherName")]
        public string ImieOjca { get; set; }

        [XmlElement(elementName: "studies")]
        [JsonPropertyName("studies")]
        public Studia studia { get; set; }
    }
}
