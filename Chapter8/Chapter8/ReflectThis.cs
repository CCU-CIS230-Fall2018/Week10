using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;

namespace Chapter8
{
    public class ReflectThis
    {
        public static bool LoadClassFromSQLDataReader (object myClass, SqlDataReader dr)
        {
            if (dr.HasRows)
            {
                dr.Read();
                Type typeOfClass = myClass.GetType();

                object[] dataMappingAttributes = typeOfClass.GetCustomAttributes(typeof(DataMappingAttribute), false);

                for (int columnIndex = 0; columnIndex < dr.FieldCount; columnIndex++)
                {
                    string columnName = dr.GetName(columnIndex);
                    PropertyInfo propInfo = typeOfClass.GetProperty(columnName);

                    //check if an attribute exists that maps this column to a prop.
                    foreach(DataMappingAttribute dataMappingAttribute in dataMappingAttributes)
                    {
                        if (dataMappingAttribute.ColumnName == columnName)
                        {
                            propInfo = typeOfClass.GetProperty(dataMappingAttribute.PropertyName);
                            break;
                        }
                    }

                    if (propInfo == null)
                    {
                        propInfo = typeOfClass.GetProperty(columnName);
                    }

                    if (propInfo != null)
                    {
                        propInfo.SetValue(myClass, dr.GetValue(columnIndex));
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
