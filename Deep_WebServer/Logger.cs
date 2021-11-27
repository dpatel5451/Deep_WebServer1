/*
* Filename		:	Logger.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Patel & Sean Mccarthy
* First Version	:	24/11/2021
* Description	:	It will create a Log file and every time program
*                   starts, it will delete last log file and will 
*                   entering new logs into created log.
*/




using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace myOwnWebServer
{

    /* Name     : Program
    * Purpose   : The purpose of this class is to create a log file that will  
    *             report every log into log file.
    */
    class Logger
    {

        // Gets and sets MyFileStream.
        public FileStream MyFileStream { get; private set; }



        /*  -- Method Header Comment
	    * Name	    :	Logger -- CONSTRUCTOR
	    * Purpose   :	It will verify that if file exists, it will delete
                        log file and will create a new log file.
	    * Inputs	:	filePath    -   string
	    * Outputs	:	NONE
	    * Returns	:	Nothing
        */
        public Logger (string filePath)
        {

            //If file exists, it will delete Log file.
            if (File.Exists(filePath))
            {
                DeleteLog(filePath);
            }

            //Initializes a new instance of the FileStream class.
            this.MyFileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);

        }




        /*  -- Method Header Comment
            Name	:	Log 
            Purpose :	The purpose of this method is to write messages to log files.
            Inputs	:	logMessage     -   string
            Returns	:	Nothing
        */
        public void Log(string logMessage)
        {

            //Initializes a new instance of StreamWritee class.
            StreamWriter myStreamWriter = new StreamWriter(MyFileStream);

            //Writes messsage to log file.
            myStreamWriter.WriteLine(DateTime.Now.ToString() + " " + logMessage);

            //Clears all the buffers.
            myStreamWriter.Flush();
        }




        /*  -- Method Header Comment
            Name	:	DeleteLog 
            Purpose :	It will delete the whole log file.
            Inputs	:	filePath     -   string
            Returns	:	NOTHING
        */
        public static void DeleteLog(string filePath)
        {

            //Deletes the specified file.
            File.Delete(filePath);

        }
    }
}
