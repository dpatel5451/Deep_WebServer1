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
        // validate web root
        public static bool ValidateWebRoot(string webRoot)
        {
            return Directory.Exists(webRoot);
        }

        public static bool ValidateEntireWebRoot(string text)
        {
            string regExpRoot = @"^–webRoot=(.*)$";
            Regex myRegex = new Regex(regExpRoot);
            var match = myRegex.Match(text);
            return true;
        }

        public static bool ValidateWebIp(string host)
        {
            return System.Uri.CheckHostName(host) != UriHostNameType.Unknown;
        }

        public static bool ValidateServerPort(string port)
        {
            bool result = int.TryParse(port, out int intPort);
            if (result == true && intPort > 0 && intPort <= 65535)
            {
                return true;
            }
            return false;
        }
    }
}
