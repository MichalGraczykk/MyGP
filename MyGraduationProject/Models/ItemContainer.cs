using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGraduationProject.Models
{
    public class ItemContainer
    {
        public IEnumerable<Connector> connectors { get; set; }
        public Item item { get; set; }
        public IEnumerable<PropValue> pValues { get; set; }
        public IEnumerable<Property> properties { get; set; }
    }
}