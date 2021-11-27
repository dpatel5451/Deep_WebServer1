/*
* Filename		:	Program.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Patel & Sean Mccarthy
* First Version	:	24/11/2021
* Description	:	It will check if suer entered 3 fields in command line arguments
*					or not. If user didn't entered any 1 of the fields, it will show
*					error. It will start the server using the user entered inputs.
*/



using myOwnWebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WDD_A6.InputVerification;

namespace myOwnWebServer
{

	/* Name     : Program
    * Purpose   : The purpose of this class is to initialize instance of server
    *			  and will start the server using webRoot, webIP and webPort.
    */
	internal class Program
	{

		static void Main(string[] args)
		{

			//Initializes webRoot, webIP and webPort.
			string webRoot = "";
			string webIp = "";
			string webPort = "";

			//Initializes isValidInput as an false bool.
			bool isValidInput = false;

			//If args.Length is not 3, it will print error.
			if (args.Length != 3)
			{
				Console.WriteLine("Please enter arguments for all three command line arguments:");
				Console.WriteLine(@"myOwnWebServer –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300");
			}
			else
			{
				//If args.Length is 3, it will set isValidInput to true.
				isValidInput = true;

				//If ValidateEntireWebRoot is not true, it will set isValidInput to false and prompts error to user.
				if (!ValidateEntireWebRoot(args[0], out webRoot))
				{
					Console.WriteLine("Please enter the switch for the webRoot correctly, e.g.: \"–webRoot=C:\\localWebSite\"");
					isValidInput = false;
				}

				//If ValidateEntireWebIp is not true, it will set isValidInput to false and prompts error to user.
				if (!ValidateEntireWebIp(args[1], out webIp))
				{
					Console.WriteLine("Please enter the switch for the webIp correctly, e.g.: \"–webIP=192.168.0.100.23\"");
					isValidInput = false;
				}

				//If ValidateEntireServerPort is not true, it will set isValidInput to false and prompts error to user.
				if (!ValidateEntireServerPort(args[2], out webPort))
				{
					Console.WriteLine("Please enter the switch for the serverPort correctly, e.g.: \"–webIP=5300\"");
					isValidInput = false;
				}
			}

			//If isValidInput is false, and shuts the program.
			if (!isValidInput)
			{
				Console.ReadKey();
				return;
			}

            //Initializes instance of local variable server.  
            Server serve = new Server();

			//Starts the server using webRoot, webIP and webPort as an Paramater.
			serve.StartServer(webRoot, webIp, webPort);
		}
	}
}
