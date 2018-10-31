using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;

namespace ClassWeek10
{
    class ReflectThis
    {
        public static bool LoadClassFromSQLDataReader(object myClass, SqlDataReader dr)
        {
            if (dr.HasRows)
            {
                dr.Read();
                Type typeOfClass = myClass.GetType();
                object[] dataMappingAttributes = typeOfClass.GetCustomAttributes(typeof(DataMappingAttribute), false);//for non matching type

                for (int columIndex = 0; columIndex < dr.FieldCount; columIndex++)
                {
                    string columnName = dr.GetName(columIndex);

                    PropertyInfo propertyInfo = null;//typeOfClass.GetProperty(columnName);
                    //check if an attribute exists theat maps this column to a property
                    foreach(DataMappingAttribute dataMappingAttribute in dataMappingAttributes)
                    {
                        if(dataMappingAttribute.ColumnName == columnName)
                        {
                            propertyInfo = typeOfClass.GetProperty(dataMappingAttribute.PropertyName);
                            break;
                        }
                    }

                    if(propertyInfo == null)
                    {
                        propertyInfo = typeOfClass.GetProperty(columnName);
                    }

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(myClass, dr.GetValue(columIndex));
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
