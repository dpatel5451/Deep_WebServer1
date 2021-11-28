/*
* Filename		:	ServerResponseBytes.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Patel & Sean Mccarthy
* First Version	:	24/11/2021
* Description	:	It will store full path and check IP. It will also 
*                   It will also reads all the contents of the file and 
*                   it will also get the file information length and also 
*                   logs Response into the log file.
*/




using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{

    /* Name      : ServerResponseBytes
    * Purpose    : The purpose of this class is to set up an server. It gives user an awesome experience of real server.
    */
    class ServerResponseBytes
    {

        //Gets and sets string FilePath.
        public string FilePath { get; private set; }

        //Gets and sets string Response.
        public string Response { get; private set; }

        //Gets and sets bytes array of FileInformation.
        public byte[] FileInformation { get; private set; }

        //Gets and sets string ContentLength.
        public string ContentLength { get; private set; }

        //Gets and sets string Ip.
        public string Ip { get; private set; }



        /*  -- Method Header Comment
	    * Name	    :	ServerResponseBytes -- CONSTRUCTOR
	    * Purpose   :	It will initializes all the members of ServerResponseBytes class.
	    *               It will also store the whole file path of a resource.
	    * Inputs	:	root            -   string
	    *               fileREsource    -   string
	    *               ip              -   string
	    * Outputs	:	NONE
	    * Returns	:	Nothing
        */
        public ServerResponseBytes(string root, string fileResource, string ip)
        {

            //Stores the whole file path into filePath.
            FilePath = root + fileResource;

            //Stores ip into local varaible.
            Ip = ip;

            //Reads the whole file and will store it into 'fileInformation' string.
            FileInformation = File.ReadAllBytes(FilePath);

            //Stores fileInformation length into 'contentLength' string.
            ContentLength = FileInformation.Length.ToString();

        }




        /*  -- Method Header Comment
            Name	:	GenerateServerResponseJpg 
            Purpose :	The purpose of this method is to return header data for jpg files.
            Inputs	:	NONE
            Returns	:	string          -       Header data for jpg files which will have
                                                Content type, Content Length, Server and Date.
        */
        public string GenerateServerResponseJpg()
        {
            //Current date and time.
            DateTime time = DateTime.Now;

            //Logs server response into log file.
            Logger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: image/jpeg Content-Length: " + ContentLength + " Server: " + Ip + " Date: " + time.ToString());

            return "HTTP/1.1\r\nContent-Type: image/jpeg\r\nContent-Length: " + ContentLength + "\r\nServer: " + Ip + "\r\nDate: " + time.ToString() + "\r\n\r\n";
        }





        /*  -- Method Header Comment
            Name	:	GenerateServerResponseGif 
            Purpose :	The purpose of this method is to return header data for gif files.
            Inputs	:	NONE
            Returns	:	string          -       Header data for gif files which will have
                                                Content type, Content Length, Server and Date.
        */
        public string GenerateServerResponseGif()
        {
            //Current date and time.
            DateTime time = DateTime.Now;

            //Logs server response into log file.
            Logger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: image/gif Content-Length: " + ContentLength + " Server: " + Ip + " Date: " + time.ToString());

            return "HTTP/1.1\r\nContent-Type: image/gif\r\nContent-Length: " + ContentLength + "\r\nServer: " + Ip + "\r\nDate: " + time.ToString() + "\r\n\r\n";
        }
    }
}
