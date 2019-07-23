using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Dapper;
using ILibrary;

namespace DynamicDLLDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte[] data = File.ReadAllBytes("LibraryImplementation.dll");

            //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ToString()))
            //{
            //    conn.Execute("truncate table T_Assembly");

            //    conn.Execute("insert into T_Assembly ([Assembly]) values (@assembly)", new { assembly = data });
            //}

            byte[] readData = null;
            using (SqlConnection readConn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ToString()))
            {
                readData = readConn.ExecuteScalar<byte[]>("select top 1 [Assembly] from T_Assembly");
            }

            Assembly assembly = Assembly.Load(readData);
            Type[] types = assembly.GetTypes();
            Type type = types.FirstOrDefault(t => typeof(ILibrary.IPersonControl).IsAssignableFrom(t));
            if(type == null)
            {
                Console.WriteLine("No assembly available.");
                Console.ReadKey();
                return;
            }

            IPersonControl p = Activator.CreateInstance(type) as IPersonControl;
            p.Print();
            Console.ReadKey();
            return;
        }
    }
}
