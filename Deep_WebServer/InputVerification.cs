using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WDD_A6
{
    class InputVerification
    {
        private static string SearchTextForRegExp(string text, string regExp)
        {
            Regex myRegex = new Regex(regExp);
            var match = myRegex.Match(text);
            return match.Groups[1].Value;
        }

        // ******* web root *******
        // get web root directory

        // verify that web root directory exists
        public static bool ValidateWebRoot(string webRoot)
        {
            return Directory.Exists(webRoot);
        }

        // verify the web root
        public static bool ValidateEntireWebRoot(string text, out string updateWebRoot)
        {
            string regExpRoot = @"^–webRoot=(.*)$";
            string webRoot = SearchTextForRegExp(text, regExpRoot);
            updateWebRoot = webRoot;
            return ValidateWebRoot(webRoot);
        }



        // ******* web ip *******
        // verify the host name
        public static bool ValidateWebIp(string host)
        {
            return System.Uri.CheckHostName(host) != UriHostNameType.Unknown;
        }

        // get web ip directory
        public static bool ValidateEntireWebIp(string text, out string updateWebIp)
        {
            string regExpRoot = @"^–webIP=(.*)$";
            string webIp = SearchTextForRegExp(text, regExpRoot);
            updateWebIp = webIp;
            return ValidateWebIp(webIp);
        }



        // ******* server port *******
        // validate server port
        public static bool ValidateServerPort(string port)
        {
            bool result = int.TryParse(port, out int intPort);
            if (result == true && intPort > 0 && intPort <= 65535)
            {
                return true;
            }
            return false;
        }

        // verify the server port
        public static bool ValidateEntireServerPort(string text, out string updateServerPort)
        {
            string regExpRoot = @"^–webPort=(.*)$";
            string serverPort = SearchTextForRegExp(text, regExpRoot);
            updateServerPort = serverPort;
            return ValidateServerPort(serverPort);
        }
    }
}
