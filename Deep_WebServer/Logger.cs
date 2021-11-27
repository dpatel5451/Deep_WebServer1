using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    class Logger
    {
        public FileStream MyFileStream { get; private set; }

        public Logger (string filePath)
        {
            this.MyFileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        }

        public void Log(string logMessage)
        {
            StreamWriter myStreamWriter = new StreamWriter(MyFileStream);
            myStreamWriter.WriteLine(DateTime.Now.ToString() + " " + logMessage);
            myStreamWriter.Flush();
        }

        public static void DeleteLog(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
