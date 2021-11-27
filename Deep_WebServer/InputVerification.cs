/*
* Filename		:	InputVerification.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Patel & Sean Mccarthy
* First Version	:	24/11/2021
* Description	:	It will verify that user input matches the requirements. 
*                   It will verify user entered webRoot, webIP & webPort.
*                   And will return the input accordingly after verifying.
*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace WDD_A6
{

    /* Name     : Program
    * Purpose   : The purpose of this class is to verify webRoot, webIP and webPort.
    */
    class InputVerification
    {

        /*  -- Method Header Comment
            Name	:	SearchTextForRegExp 
            Purpose :	It will matches the user entered input and 
                        will see if it matches the regex.
            Inputs	:	text        -   string      User entered input
                        regExp      -   string      Required regex 
            Returns	:	string      -   Returns captures substring.
        */
        private static string SearchTextForRegExp(string text, string regExp)
        {
            //Initializes a new instance of regex class.
            Regex myRegex = new Regex(regExp);

            //Matches user entered text and regex.
            var match = myRegex.Match(text);

            return match.Groups[1].Value;
        }




        /*  -- Method Header Comment
            Name	:	ValidateWebRoot 
            Purpose :	It will verify that web root directory exists.
            Inputs	:	webRoot     -   string
            Returns	:	Bool        -   True    If directory exists.
                                    -   False   If directory doesn't exists.
        */
        public static bool ValidateWebRoot(string webRoot)
        {

            //Checks if directory exists or not and returns bool accordingly.
            return Directory.Exists(webRoot);

        }




        /*  -- Method Header Comment
            Name	:	ValidateEntireWebRoot 
            Purpose :	Verify the web root using regex.
            Inputs	:	text            -   string
                        updateWebRoot   -   string
            Returns	:	Bool        -   True    If directory exists.
                                    -   False   If directory doesn't exists.
        */
        public static bool ValidateEntireWebRoot(string text, out string updateWebRoot)
        {

            //Creates regex for webRoot.
            string regExpRoot = @"^–webRoot=(.*)$";

            //Compares both user input and regex and will store it into webRoot string.
            string webRoot = SearchTextForRegExp(text, regExpRoot);

            //Stores webRoot into updateWebRoot string.
            updateWebRoot = webRoot;

            return ValidateWebRoot(webRoot);

        }


        /*  -- Method Header Comment
            Name	:	ValidateWebIp 
            Purpose :	Verify the host name.
            Inputs	:	host        -   string
            Returns	:	Bool        -   True    If specified host is a valid DNS name.
                                    -   False   If specified host is not a valid DNS name.
        */
        public static bool ValidateWebIp(string host)
        {
            return System.Uri.CheckHostName(host) != UriHostNameType.Unknown;
        }





        /*  -- Method Header Comment
            Name	:	ValidateEntireWebIp 
            Purpose :	Verify the web IP using regex.
            Inputs	:	text         -   string
                        updateWebIp  -   string
            Returns	:	Bool        -   True    If specified host is a valid DNS name.
                                    -   False   If specified host is not a valid DNS name.
        */
        public static bool ValidateEntireWebIp(string text, out string updateWebIp)
        {

            //Creates webIP regex.
            string regExpRoot = @"^–webIP=(.*)$";

            //Compares both user input and regex and will store it into webIp string.
            string webIp = SearchTextForRegExp(text, regExpRoot);

            //Stores webIp into updateWebIp string.
            updateWebIp = webIp;

            return ValidateWebIp(webIp);

        }





        /*  -- Method Header Comment
            Name	:	ValidateServerPort 
            Purpose :	Validates server port by parsing it to check if port is in range.
            Inputs	:	port        -   string
            Returns	:	Bool        -   True    If port is in range.
                                    -   False   If port is out of range.
        */
        public static bool ValidateServerPort(string port)
        {

            //Parses the integer and will store result of succession into result.
            bool result = int.TryParse(port, out int intPort);

            //Checks if 'result' bool is true and its in range and will return true.
            if (result == true && intPort > 0 && intPort <= 65535)
            {
                return true;
            }

            return false;
        }





        /*  -- Method Header Comment
            Name	:	ValidateEntireServerPort 
            Purpose :	Verify the server port using regex.
            Inputs	:	text                -   string
                        updateServerPort    -   string
            Returns	:	Bool        -   True    If port is in range.
                                    -   False   If port is out of range.
        */
        public static bool ValidateEntireServerPort(string text, out string updateServerPort)
        {

            //Creates webPort regex.
            string regExpRoot = @"^–webPort=(.*)$";

            //Compares both user input and regex and will store it into serverPort string.
            string serverPort = SearchTextForRegExp(text, regExpRoot);

            //Stores serverPort into updateServerPort string.
            updateServerPort = serverPort;

            return ValidateServerPort(serverPort);
        }
    }
}
