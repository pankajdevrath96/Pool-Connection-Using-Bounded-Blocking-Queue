
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Pool_Connection_Using_Bounded_Blocking_Queue
{
    public class Program
    {
        public static async Task Main(string[] str)
        {


            var connectionString = "";  // Define you connection string.
            int maxPoolSize = 2;


            DB dB = new DB(connectionString, maxPoolSize);

            var tasks = new Task[10];


            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    SqlConnection conn = null;
                    try
                    {
                        conn = dB.AquireConnection();
                        Console.WriteLine($"Thread {Task.CurrentId} acquired connection.");
                        Thread.Sleep(2000);
                    }
                    finally
                    {
                        dB.ReleaseConnection(conn);
                        Console.WriteLine($"Thread {Task.CurrentId} released connection.");
                    }
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("All tasks completed.");

            Console.ReadLine();


        }

    }
}