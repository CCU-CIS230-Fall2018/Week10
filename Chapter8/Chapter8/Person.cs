using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Chapter8
{
    [DataMapping("FirstName","FName")]
    [DataMapping("LastName","LName")]
    class Person
    {
        public int PersonId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public bool GetPerson(int personID)
        {
            SqlConnection cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Keaton Helm\Source\Repos\CCU-CIS230-Fall2018\Week10\Chapter8\Chapter8\ReflectionDemo.mdf'; Integrated Security = True");
            cn.Open();

            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM People WHERE PersonID = {0}", personID), cn);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return ReflectThis.LoadClassFromSQLDataReader(this, dr);
        }

    }
}
