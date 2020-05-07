using Basics.Syslog.Net;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Basics.Syslog.ConsoleApp.Server
{
    class Program
    {
        private static int _port = 514;
        static void Main(string[] args)
        {
            PrintInfo();

            UdpListener server = new UdpListener(_port);
            server.MessageReceived += Server_MessageReceived;
            Task.Run(server.Start);
            PrintMenu();
        }
        private static void Server_MessageReceived(object sender, MessageReceivedArgs e)
        {
            var receivedData = Encoding.ASCII.GetString(e.Data, 0, e.Data.Length);
            var msg = SyslogMessage.Parse(e.IPEndPoint, receivedData);

            Console.WriteLine(msg.ToString());
        }
        static void PrintMenu()
        {

            while (true)
            {
                Console.Write("[Syslog Server :: ? for help] >> ");
                var userInput = Console.ReadLine().Trim();

                switch (userInput)
                {
                    case "?":
                        Console.WriteLine("---");
                        Console.WriteLine("  q      Quit the application");
                        Console.WriteLine("  cls    Clear the screen");
                        break;

                    case "q":
                        Console.WriteLine("Exiting.");
                        Environment.Exit(0);
                        break;

                    case "c":
                    case "cls":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Unknown command.  Type '?' for help.");
                        continue;
                }
            }
        }

        static void PrintInfo()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("           Syslog Server              ");
            Console.WriteLine("======================================");
        }
    }
}
