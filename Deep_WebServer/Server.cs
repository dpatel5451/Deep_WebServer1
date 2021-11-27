/*
* Filename		:	Program.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Kalpeshkumar Patel
* First Version	:	24/11/2021
* Description	:	
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
                                    string res = "HTTP/1.1\r\nContent-Type: text/html\r\nContent-Length: " + fileInformation.Length.ToString() + "\r\nServer: " + ip + "\r\nDate: " + time.ToString() + "\r\n\r\n" + fileInformation;

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

                                    byte[] bytess = new byte[msg.Length + fileInformation.Length];
                                    Buffer.BlockCopy(msg, 0, bytes, 0, msg.Length);
                                    Buffer.BlockCopy(fileInformation, 0, bytes, msg.Length, fileInformation.Length);

                                    //Writes data to NetworkStream.
                                    stream.Write(bytess, 0, bytess.Length);
                                }
                                else
                                {
                                    
                                    client.Close();
                                    break;
                                }

                                
                            }
                            else
                            {
                                client.Close();
                                break;
                            }


                        }

                        //Disposes TcpClient instance and requests that underlying TCP connection to be closed. 
                        client.Close();

                    }

                }
                

            }
            catch (Exception ex)
            {
                throw;
            }
            catch 
            {
                throw;
            }

        }


    }
}
