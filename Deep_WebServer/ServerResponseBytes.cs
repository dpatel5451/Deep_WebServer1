using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    class ServerResponseBytes
    {
        public string FilePath { get; private set; }
        public string Response { get; private set; }
        public byte[] FileInformation { get; private set; }
        public string ContentLength { get; private set; }
        public string Ip { get; private set; }

        readonly Logger MyLogger;
        public ServerResponseBytes(string root, string fileResource, string ip, Logger inLogger)
        {
            FilePath = root + fileResource;

            Ip = ip;

            //Reads the whole file and will store it into 'fileInformation' string.
            FileInformation = File.ReadAllBytes(FilePath);

            //Stores fileInformation length into 'contentLength' string.
            ContentLength = FileInformation.Length.ToString();

            MyLogger = inLogger;
        }

        public string GenerateServerResponseJpg()
        {
            //Current date and time.
            DateTime time = DateTime.Now;

            //Logs server response into log file.
            MyLogger.Log("[Server Response]" + " - " + "HTTP/1.1 200 Content-Type: image/jpeg Content-Length: " + ContentLength + " Server: " + Ip + " Date: " + time.ToString());

            return "HTTP/1.1\r\nContent-Type: image/jpeg\r\nContent-Length: " + ContentLength + "\r\nServer: " + Ip + "\r\nDate: " + time.ToString() + "\r\n\r\n";
        }
    }
}
