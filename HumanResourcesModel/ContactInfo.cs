using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesModel
{
    [DataContract]
    public class ContactInfo
    {
        [DataMember]
        public string Kind { get; set; }
        [DataMember]
        public string Info { get; set; }
    }
}
