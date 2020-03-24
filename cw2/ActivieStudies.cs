using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    public class ActivieStudies
    {
        [XmlAttribute(attributeName: "name")]
        [JsonPropertyName("name")]
        public string Nazwa { get; set; }

        [XmlAttribute(attributeName: "numberOfStudents")]
        [JsonPropertyName("numberOfStudents")]
        public int LiczbaStudentow { get; set; }
    }
}
