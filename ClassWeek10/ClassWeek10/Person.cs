using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace ClassWeek10
{
    [DataMapping("FirstName","FName")]
    [DataMapping("LastName","LName")]
    class Person
    {
        public int PersonId { get; set; }
        public string FName { get; set; }//works with GetPerson if same feild names
        public string LName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public bool GetPerson(int personID)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Cora\Source\Repos\Week10\ClassWeek10\ClassWeek10\ReflectionDemo.mdf;Integrated Security=True");
            cn.Open();
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM People WHERE PersonID = {0}", personID), cn);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return ReflectThis.LoadClassFromSQLDataReader(this, dr);
        }
    }
}
