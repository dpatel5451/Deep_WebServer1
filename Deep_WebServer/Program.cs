/*
* Filename		:	Program.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Patel & Sean Mccarthy
* First Version	:	24/11/2021
* Description	:	It will check if suer entered 3 fields in command line arguments
*					or not. If user didn't entered any 1 of the fields, it will show
*					error. It will start the server using the user entered inputs.
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WDD_A6.InputVerification;

namespace Deep_WebServer
{

	/* Name     : Program
    * Purpose   : The purpose of this class is to initialize instance of server and will start the server.
    */
	internal class Program
	{
		static void Main(string[] args)
		{
			string webRoot = "";
			string webIp = "";
			string webPort = "";

			bool isValidInput = false;
			if (args.Length != 3)
			{
				Console.WriteLine("Please enter arguments for all three command line arguments:");
				Console.WriteLine(@"myOwnWebServer –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300");
			}
			else
			{
				isValidInput = true;
				if (!ValidateEntireWebRoot(args[0], out webRoot))
				{
					Console.WriteLine("Please enter the switch for the webRoot correctly, e.g.: \"–webRoot=C:\\localWebSite\"");
					isValidInput = false;
				}
				if (!ValidateEntireWebIp(args[1], out webIp))
				{
					Console.WriteLine("Please enter the switch for the webIp correctly, e.g.: \"–webIP=192.168.0.100.23\"");
					isValidInput = false;
				}
				if (!ValidateEntireServerPort(args[2], out webPort))
				{
					Console.WriteLine("Please enter the switch for the serverPort correctly, e.g.: \"–webIP=5300\"");
					isValidInput = false;
				}
			}

			if (!isValidInput)
			{
				Console.ReadKey();
				return;
			}
			else
            {
				Console.WriteLine("You entered the webRoot as: {0}, the webIp as: {1}, and the webPort as: {2}", webRoot, webIp, webPort);
				Console.WriteLine("Starting the server now...");
            }

            //Initializes instance of local variable server.  
            Server serve = new Server();

			//Starts the server.
			serve.StartServer(webRoot, webIp, webPort);
		}
	}
}
