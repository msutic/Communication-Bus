using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationBus
{
    public interface IRepository
    {
        string status { get; set; }

        int statusCode { get; set; }

        string payload { get; set; }

        string connectToDataBaseGet(string query, string konekcija, string server, string port, string username, string pass);

        void setResponseParams(string response);

        string createTable(string imeTabele, string imeBaze, string server, string port, string username, string pass);

        string insertInDataBase(string query, string dataBaseName, string server, string port, string username, string pass);

        string createDataBase(string dataBaseName,  string server, string port, string username, string pass);
    }
}
