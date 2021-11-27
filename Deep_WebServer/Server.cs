﻿/*
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
    * Purpose    : 
    */
    class Server
    {
        
        //Initializes variable that will listens connection from TCP network clients.
        private TcpListener server = null;
        public Logger MyLogger { get; private set; }


        public Server()
        {
            MyLogger = new Logger("myOwnWebServer.log");
            MyLogger.Log("Starting server...");
        }


        /*  -- Method Header Comment
            Name	:	StartServer 
            Purpose :	
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
                        NetworkStream stream = client.GetStream();


                        //Initializes local variables.
                        int length = 0;
                        int c = 0;

                        //Loop will continue until all data is not read.
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {

                            MyLogger.Log("[Server Request]" + " - " + "HTTP/1.1 Content-Type: text/html Content-Length: " + fileInformation.Length.ToString() + "Server: " + ip + "Date: " + time.ToString());
                            

                            //Stores the incoming message.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, length);

                            string request = data.Substring(0,3);

                            //If request is not GET, it will close the client.
                            if(request == "GET")
                            {

                                if (c == 0)
                                {
                                    inputData = data.Split(' ');
                                    c = 1;
                                }

                                int place = inputData[1].LastIndexOf(".");

                                string extension = inputData[1].Substring(place+1);

                                if(extension == "html" || extension == "htm" || extension =="htmls" || extension =="htx")
                                {
                                    file = root + inputData[1];

                                    string fileInformation = File.ReadAllText(file);
                                    DateTime time = DateTime.Now;
                                    string ContentLength = fileInformation.Length.ToString();

                                    string res = "HTTP/1.1\r\nContent-Type: text/html\r\nContent-Length: " + ContentLength + "\r\nServer: " + ip + "\r\nDate: " + time.ToString() + "\r\n\r\n" + fileInformation;

                                    MyLogger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: text/html Content-Length: " + fileInformation.Length.ToString() + "Server: " + ip + "Date: " + time.ToString());

                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);

                                    //Writes data to NetworkStream.
                                    stream.Write(msg, 0, msg.Length);
                                }
                                else if(extension =="htt")
                                {
                                    file = root + inputData[1];

                                    string fileInformation = File.ReadAllText(file);
                                    DateTime time = DateTime.Now;
                                    string res = "HTTP/1.1\r\nContent-Type: text/webviewhtml\r\nContent-Length: " + fileInformation.Length.ToString() + "\r\nServer: " + ip + "\r\nDate: " + time.ToString() + "\r\n\r\n" + fileInformation;
                                    

                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);

                                    //Writes data to NetworkStream.
                                    stream.Write(msg, 0, msg.Length);
                                }
                                else if(extension == "txt")
                                {
                                    file = root + inputData[1];

                                    string fileInformation = File.ReadAllText(file);
                                    DateTime time = DateTime.Now;
                                    string res = "HTTP/1.1\r\nContent-Type: text/plain\r\nContent-Length: " + fileInformation.Length.ToString() + "\r\nServer: " + ip + "\r\nDate: " + time.ToString() + "\r\n\r\n" + fileInformation;
                                    

                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);

                                    //Writes data to NetworkStream.
                                    stream.Write(msg, 0, msg.Length);
                                }
                                else if(extension == "jpg" || extension == "jpeg" || extension =="pjp" || extension =="jfif" || extension =="jfif")
                                {
                                    file = root + inputData[1];

                                    DateTime time = DateTime.Now;

                                    byte[] fileInformation = File.ReadAllBytes(file);

                                    string res = "HTTP/1.1\r\nContent-Type: image/jpeg\r\nContent-Length: " + fileInformation.Length.ToString() + "\r\nServer: " + ip + "\r\nDate: " + time.ToString() + "\r\n\r\n";

                                   
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);

                                    /*
                                    * Title			: Concat two or more byte arrays in C#
                                    * Author		: Techie Delight 
                                    * Date			: 2021-11-26
                                    * Version		: 1.1.26
                                    * Availability	: https://www.techiedelight.com/concatenate-byte-arrays-csharp/
                                    */

                                    byte[] bytess = new byte[msg.Length + fileInformation.Length];
                                    Buffer.BlockCopy(msg, 0, bytes, 0, msg.Length);
                                    Buffer.BlockCopy(fileInformation, 0, bytes, msg.Length, fileInformation.Length);

                                    //Writes data to NetworkStream.
                                    stream.Write(bytess, 0, bytess.Length);
                                }
                                else if(extension == "gif")
                                {
                                    file = root + inputData[1];

                                    DateTime time = DateTime.Now;

                                    byte[] fileInformation = File.ReadAllBytes(file);

                                    string res = "HTTP/1.1\r\nContent-Type: image/gif\r\nContent-Length: " + fileInformation.Length.ToString() + "\r\nServer: " + ip + "\r\nDate: " + time.ToString() + "\r\n\r\n";


                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(res);


                                    /*
                                    * Title			: Concat two or more byte arrays in C#
                                    * Author		: Techie Delight 
                                    * Date			: 2021-11-26
                                    * Version		: 1.1.26
                                    * Availability	: https://www.techiedelight.com/concatenate-byte-arrays-csharp/
                                    */

                                    byte[] bytess = new byte[msg.Length + fileInformation.Length];
                                    Buffer.BlockCopy(msg, 0, bytes, 0, msg.Length);
                                    Buffer.BlockCopy(fileInformation, 0, bytes, msg.Length, fileInformation.Length);

                                    //Writes data to NetworkStream.
                                    stream.Write(bytess, 0, bytess.Length);
                                }
                                else
                                {

                                    MyLogger.Log("415 Unsupported Media Type");

                                    break;
                                }

                                
                            }
                            else
                            {

                                MyLogger.Log("401 Unauthorized");
                                break;

                            }


                        }

                        //Disposes TcpClient instance and requests that underlying TCP connection to be closed. 
                        client.Close();

                    }

                }
                

            }
            catch (FileNotFoundException)
            {
                MyLogger.Log("404 Not Found");
            }
            catch(IOException)
            {

            }
            catch 
            {
                throw;
            }

        }


    }
}
