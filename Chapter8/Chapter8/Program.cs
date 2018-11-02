using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Chapter8
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExploreAnAssembly();
            //ExploreAType();
            Person p = new Person();
            p.GetPerson(1);
            Console.WriteLine("FirstName: {0}", p.FName);
        }

        static void ExploreAnAssembly()
        {
            Assembly anAssembly = Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            Console.WriteLine("Codebase: {0}", anAssembly.CodeBase);
            Console.WriteLine("Location: {0}", anAssembly.Location);

            Module[] anAssemblyModules = anAssembly.GetModules();

            foreach (Module mod in anAssemblyModules)
            {
                Console.WriteLine(mod.Name);
            }
        }

        static void ExploreAType()
        {
            string myName = "Keaton";
            System.Type myType = myName.GetType();
            Console.WriteLine(myType.AssemblyQualifiedName);
        }

    }
}
