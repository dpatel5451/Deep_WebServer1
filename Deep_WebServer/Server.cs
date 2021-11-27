/*
* Filename		:	Server.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Patel & Sean Mccarthy
* First Version	:	24/11/2021
* Description	:	It will start the server using user entered webRoot, webIP and
*                   webPort. It will also checks if the request type is GET. If it
*                   is not, it will prompt error to user if user doesnt enter extensions
*                   as specified. It will catch all the important exceptions and will
*                   create a log entry for each exceptions
*/





using System;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Threading;
using System.IO;



namespace myOwnWebServer
{

    /* Name      : Server
    * Purpose    : The purpose of this class is to set up an server. It gives user an awesome experience of real server.
    */
    class Server
    {
        
        //Initializes variable that will listens connection from TCP network clients.
        private TcpListener server = null;

        //Gets and sets MyLogger.
        public Logger MyLogger { get; private set; }
        
        //Underlying data stream
        NetworkStream Stream { get; set; }


        /*  -- Method Header Comment
	    * Name	    :	Server -- CONSTRUCTOR
	    * Purpose   :	To instantiate MyLogger as an Logger class object & will 
	    *               log that Server as started. 
	    * Inputs	:	NONE
	    * Outputs	:	NONE
	    * Returns	:	Nothing
        */
        public Server()
        {
            MyLogger = new Logger("myOwnWebServer.log");
            MyLogger.Log("Starting server...");
        }


        /*  -- Method Header Comment
            Name	:	StartServer 
            Purpose :	It will setup an server and will start the server.
                        It will take one request at an time only. It will 
                        verify all the user request and will match if user 
                        inputs matches required configurations.
            Inputs	:	root        -   string
                        ip          -   string
                        portNum     -   string
            Returns	:	Nothing
        */
        public void StartServer(string root, string ip, string portNum)
        {
            try
            { 
                //Instantiating localAddr as an IPAddress object.
                IPAddress localAddr = IPAddress.Parse(ip);

                //Passes localAddr & parsed portNum to TcpListener and will store into server. 
                server = new TcpListener(localAddr, Int32.Parse(portNum));

                //Starts the server.
                server.Start();

                while (true)
                {
                    //Checks if there are any pending connection requests.
                    if (!server.Pending())
                    {
                        // do nothing
                    }
                    else
                    {
                        //Initializes local variables.
                        Byte[] bytes = new Byte[256];
                        string data = null;

                        //Instantiating client as an TcpClient object & It also accepts a pending server connection request.
                        TcpClient client = server.AcceptTcpClient();

                        //Initializes local variables.
                        string[] inputData = {""};
                        string file;
                        
                        //Declaring "data" to null.
                        data = null;

                        //Instantiating stream as an NetworkStream object and It also sends and recieve data.
                        Stream = client.GetStream();

                        //Initializes local variables.
                        int length = 0;

                        //Loop will continue until all data is not read.
                        while ((length = Stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            //Stores the incoming message.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, length);

                            ClientRequest myClientRequest = new ClientRequest(data);

                            //Logs server request into log file.
                            MyLogger.Log("[Server Request] " + myClientRequest.RequestType + " " + myClientRequest.Resource);

                            //Checks if request type is GET
                            if (myClientRequest.VerifyRequest())
                            {
                                //Checks if the extension is allowed.
                                if(myClientRequest.VerifyResourceExtensionHtmlFiles())
                                {
                                    ServerResponse myServerResponse = new ServerResponse(root, myClientRequest.Resource, ip, MyLogger);

                                    string res = myServerResponse.GenerateServerResponseHtml();

                                    //Encodes all the characters of 'res' string and stores it in 'msg' as an byte array.
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);

                                    //Writes data to NetworkStream.
                                    Stream.Write(msg, 0, msg.Length);
                                }
                                else if(myClientRequest.VerifyResourceExtensionHttFile())
                                {
                                    ServerResponse myServerResponse = new ServerResponse(root, myClientRequest.Resource, ip, MyLogger);

                                    string res = myServerResponse.GenerateServerResponseHtt();

                                    //Encodes all the characters of 'res' string and stores it in 'msg' as an byte array.
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);

                                    //Writes data to NetworkStream.
                                    Stream.Write(msg, 0, msg.Length);
                                }
                                else if(myClientRequest.VerifyResourceExtensionTxtFiles())
                                {
                                    ServerResponse myServerResponse = new ServerResponse(root, myClientRequest.Resource, ip, MyLogger);

                                    string res = myServerResponse.GenerateServerResponseTxt();

                                    //Encodes all the characters of 'res' string and stores it in 'msg' as an byte array.
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);

                                    //Writes data to NetworkStream.
                                    Stream.Write(msg, 0, msg.Length);
                                }
                                else if (myClientRequest.VerifyResourceExtensionJpgImages()) {

                                    //Stores the File root and file location into 'file' string.
                                    file = root + myClientRequest.Resource;

                                    //Logs server request into log file.
                                    MyLogger.Log("[Server Request] " + myClientRequest.RequestType + " " + file);

                                    //Current date and time.
                                    DateTime time = DateTime.Now;

                                    //Reads all bytes from 'file' and will store it as an bytes array into 'fileInformation'.
                                    byte[] fileInformation = File.ReadAllBytes(file);

                                    //Contains Content-Type, ContentLength, Server, Data and Contents of the file. 
                                    string res = "HTTP/1.1\r\nContent-Type: image/jpeg\r\nContent-Length: " + fileInformation.Length.ToString() + "\r\nServer: " + ip + "\r\nDate: " + time.ToString() + "\r\n\r\n";

                                    //Logs server response into log file.
                                    MyLogger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: text/html Content-Length: " + fileInformation.Length.ToString() + " Server: " + ip + " Date: " + time.ToString());

                                    //Encodes all the characters of 'res' string and stores it in 'msg' as an byte array.
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);

                                    /*
                                    * Title			: Concat two or more byte arrays in C#
                                    * Author		: Techie Delight 
                                    * Date			: 2021-11-26
                                    * Version		: 1.1.26
                                    * Availability	: https://www.techiedelight.com/concatenate-byte-arrays-csharp/
                                    */

                                    //Merges two bytes of arrays using both arrays Length and by using BlockCopy.
                                    byte[] bytess = new byte[msg.Length + fileInformation.Length];
                                    Buffer.BlockCopy(msg, 0, bytes, 0, msg.Length);
                                    Buffer.BlockCopy(fileInformation, 0, bytes, msg.Length, fileInformation.Length);

                                    //Writes data to NetworkStream.
                                    Stream.Write(bytess, 0, bytess.Length);
                                }
                                else if(myClientRequest.VerifyResourceExtensionGif())
                                {

                                    //Stores the File root and file location into 'file' string.
                                    file = root + inputData[1];

                                    //Logs server request into log file.
                                    MyLogger.Log("[Server Request] " + myClientRequest.RequestType + " " + file);

                                    //Current date and time.
                                    DateTime time = DateTime.Now;

                                    //Reads all bytes from 'file' and will store it as an bytes array into 'fileInformation'.
                                    byte[] fileInformation = File.ReadAllBytes(file);

                                    //Contains Content-Type, ContentLength, Server, Data and Contents of the file. 
                                    string res = "HTTP/1.1\r\nContent-Type: image/gif\r\nContent-Length: " + fileInformation.Length.ToString() + "\r\nServer: " + ip + "\r\nDate: " + time.ToString() + "\r\n\r\n";

                                    //Logs server response into log file.
                                    MyLogger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: text/html Content-Length: " + fileInformation.Length.ToString() + " Server: " + ip + " Date: " + time.ToString());

                                    //Encodes all the characters of 'res' string and stores it in 'msg' as an byte array.
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);


                                    /*
                                    * Title			: Concat two or more byte arrays in C#
                                    * Author		: Techie Delight 
                                    * Date			: 2021-11-26
                                    * Version		: 1.1.26
                                    * Availability	: https://www.techiedelight.com/concatenate-byte-arrays-csharp/
                                    */

                                    //Merges two bytes of arrays using both arrays Length and by using BlockCopy.
                                    byte[] bytess = new byte[msg.Length + fileInformation.Length];
                                    Buffer.BlockCopy(msg, 0, bytes, 0, msg.Length);
                                    Buffer.BlockCopy(fileInformation, 0, bytes, msg.Length, fileInformation.Length);

                                    //Writes data to NetworkStream.
                                    Stream.Write(bytess, 0, bytess.Length);
                                }
                                else
                                {
                                    //If the extension is not allowed it will report it in Log File.
                                    MyLogger.Log("415 Unsupported Media Type");
                                }
                            }
                            else
                            { 
                                //If Request type is not GET, it will report it in Log File.
                                MyLogger.Log("401 Unauthorized");
                            }
                        }
                        //Disposes TcpClient instance and requests that underlying TCP connection to be closed.

                        client.Close();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                //Catches FileNotFoundException and will report into Log file. 
                MyLogger.Log("404 Not Found");
            }
            catch(SocketException)
            {
                //Catches SocketException and will report into Log file.
                MyLogger.Log("500 Server Error");

                //Prints Server Shutdown message.
                Console.WriteLine("500 Server Error");

                //Stops the server.
                server.Stop();
            }
        }
    }
}

// make PROPRERTY