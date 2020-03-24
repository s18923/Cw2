using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    public class Studia
    {
        [XmlElement(elementName: "name")]
        [JsonPropertyName("name")]
        public string Kierunek { get; set; }

        [XmlElement(elementName: "mode")]
        [JsonPropertyName("mode")]
        public string Tryb { get; set; }       
    }
}
