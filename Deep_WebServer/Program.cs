/*
* Filename		:	Program.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Kalpeshkumar Patel
* First Version	:	24/11/2021
* Description	:	
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep_WebServer
{

	/* Name     : Program
    * Purpose   : The purpose of this class is to intialize instance of server and will start the server.
    */
	internal class Program
	{
		static void Main(string[] args)
		{

			//Initializes instance of local variable server.  
			Server serve = new Server();

			//Starts the server.
			serve.StartServer(@"C:/localWebSite", "127.0.0.1", "15000");

		}
	}
}
