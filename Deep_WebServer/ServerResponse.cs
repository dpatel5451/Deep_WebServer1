using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    class ServerResponse
    {
        public string FilePath { get; private set; }
        public string Response { get; private set; }
        public string FileInformation { get; private set; }
        public string ContentLength { get; private set; }
        public string Ip { get; private set; }




        /*  -- Method Header Comment
	    * Name	    :	ServerResponse -- CONSTRUCTOR
	    * Purpose   :	It will initializes all the members of ServerResponse class.
	    *               It will also store the whole file path of a resource.
	    * Inputs	:	root            -   string
	    *               fileREsource    -   string
	    *               ip              -   string
	    * Outputs	:	NONE
	    * Returns	:	Nothing
        */
        public ServerResponse(string root, string fileResource, string ip)
        {
            FilePath = root + fileResource;

            Ip = ip;

            //Reads the whole file and will store it into 'fileInformation' string.
            FileInformation = File.ReadAllText(FilePath);

            //Stores fileInformation length into 'contentLength' string.
            ContentLength = FileInformation.Length.ToString();            
        }



        /*  -- Method Header Comment
            Name	:	GenerateServerResponseHtml 
            Purpose :	The purpose of this method is to return header data for html files.
            Inputs	:	NONE
            Returns	:	string          -       Header data for html files which will have
                                                Content type, Content Length, Server and Date.
        */
        public string GenerateServerResponseHtml()
        {
            //Current date and time.
            DateTime time = DateTime.Now;

            //Logs server response into log file.
            Logger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: text/html Content-Length: " + ContentLength + " Server: " + Ip + " Date: " + time.ToString());

            return "HTTP/1.1\r\nContent-Type: text/html\r\nContent-Length: " + ContentLength + "\r\nServer: " + Ip + "\r\nDate: " + time.ToString() + "\r\n\r\n" + FileInformation;
        }




        /*  -- Method Header Comment
            Name	:	GenerateServerResponseHtt 
            Purpose :	The purpose of this method is to return header data for htt files.
            Inputs	:	NONE
            Returns	:	string          -       Header data for htt files which will have
                                                Content type, Content Length, Server and Date.
        */
        public string GenerateServerResponseHtt()
        {
            //Current date and time.
            DateTime time = DateTime.Now;

            //Logs server response into log file.
            Logger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: text/webviewhtml Content-Length: " + ContentLength + " Server: " + Ip + " Date: " + time.ToString());

            return "HTTP/1.1\r\nContent-Type: text/webviewhtml\r\nContent-Length: " + ContentLength + "\r\nServer: " + Ip + "\r\nDate: " + time.ToString() + "\r\n\r\n" + FileInformation;
        }




        /*  -- Method Header Comment
            Name	:	GenerateServerResponseTxt 
            Purpose :	The purpose of this method is to return header data for txt files.
            Inputs	:	NONE
            Returns	:	string          -       Header data for txt files which will have
                                                Content type, Content Length, Server and Date.
        */
        public string GenerateServerResponseTxt()
        {
            //Current date and time.
            DateTime time = DateTime.Now;

            //Logs server response into log file.
            Logger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: text/plain Content-Length: " + ContentLength + " Server: " + Ip + " Date: " + time.ToString());

            return "HTTP/1.1\r\nContent-Type: text/plain\r\nContent-Length: " + ContentLength + "\r\nServer: " + Ip + "\r\nDate: " + time.ToString() + "\r\n\r\n" + FileInformation;
        }





        /*  -- Method Header Comment
            Name	:	GenerateWebResponseSite 
            Purpose :	The purpose of this method is to read all data from specified location.
            Inputs	:	NONE
            Returns	:	string          -       All contents of specified files.
        */
        public static string GenerateWebResponseSite(string path)
        {
            return File.ReadAllText(@"..\..\htmlPages\" + path);
        }
    }
}
