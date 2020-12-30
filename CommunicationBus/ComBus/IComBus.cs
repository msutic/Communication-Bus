using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    public interface IComBus
    {
        XmlAdapter xml { get; set; }
        XmlToDB db { get; set; }
        string dbFormat { get; set; }
        string xmlFormat { get; set; }
        string DetermineMethod(string json, string attr);
    }
}
