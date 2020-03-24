using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    public class Wynik
    {
        [XmlAttribute(attributeName: "createdAt")]
        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }

        [XmlAttribute(attributeName: "autor")]
        [JsonPropertyName("autor")]
        public string Author { get; set; }

        public List<Student> studenci { get; set; }
       
        public List<ActivieStudies> activieStudies { get; set; }
    }
}
