using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestAPIAutomation.Model.XmlModel
{
        [XmlRoot(ElementName = "laptopDetails")]
        public class LaptopDetails
        {
            [XmlElement(ElementName ="Laptop")]
            public Laptop Laptop { get; set; }
        }

    }
