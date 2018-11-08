using System;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

namespace SqliteWithExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SQLiteConnection db = new SQLiteConnection("Data Source=E:\\work\\myproject\\netcore\\aspnetcoreDemo\\ef\\SqliteWithExample\\Dict2Anki.db;FailIfMissing=True;");
            try
            {
                //string commandText = "select id,terms,time  deck dictionary from history";
                string commandText = "select id,terms,time from history";
                db.Open();

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(commandText, db);

                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    //Console.WriteLine($"id:{row["id"]}");
                    //Console.WriteLine($"terms:{row["terms"]}");
                    //Console.WriteLine($"time:{row["time"]}");
                    //Console.WriteLine("------------------------------------");

                    System.Diagnostics.Debug.WriteLine($"id:{row["id"]}");
                    System.Diagnostics.Debug.WriteLine($"terms:{row["terms"]}");
                    System.Diagnostics.Debug.WriteLine($"time:{row["time"]}");
                    System.Diagnostics.Debug.WriteLine("------------------------------------");
                }

                Console.WriteLine(ds.Tables[0].Rows.Count);
                Console.ReadKey();
            }
            finally
            {
                db.Close();
            }
            
        }
    }
}
