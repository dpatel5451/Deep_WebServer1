/*
* Filename		:	ServerMessage.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Patel & Sean Mccarthy
* First Version	:	24/11/2021
* Description	:	This class holds the definition for the Client Request.
*/


using System;


/* Name     : Program
* Purpose   : The purpose of the ClientRequest is to hold the request message and parse its contents.
*             The class has vitrifaction members to validate the request's contents.
*/
public class ClientRequest
{
    // properties for the request
    public string Request { get; private set; } // entire request string
    public string[] RequestSplit { get; private set; } // array for message
    public string RequestType { get; private set; } // GET or POST
    public string Resource { get; private set; } // the resource requested e.g. "test.html"
    public string ResourceExtension { get; private set; } // the extension of the resource e.g. "html"
    public string HttpSpecification { get; private set; } // the HTTP specification
    public string HostLabel { get; private set; } // the label "HOST"
    public string Host { get; private set; } // the host IP

    /*  -- Method Header Comment
	    * Name	    :	ClientRequest -- CONSTRUCTOR
	    * Purpose   :	It takes in the client request string and parses it into the properties of the class.
	    * Inputs	:	inData        -   string
	    * Outputs	:	NONE
	    * Returns	:	Nothing
        */
    public ClientRequest(string inData)
	{
        // parse the input data
        Request = inData;
        RequestSplit = Request.Split(new string[] { "\r\n", " " }, StringSplitOptions.None); // split based on new line and spaces
        RequestType = RequestSplit[0];
        Resource = RequestSplit[1];

        // check for "." in the Resource
        if (Resource.Contains("."))
        {
            // get the extension
            ResourceExtension = Resource.Substring(Resource.LastIndexOf(".") + 1);
        }

        // parse the remaining input data
        HttpSpecification = RequestSplit[2];
        HostLabel = RequestSplit[3];
        Host = RequestSplit[4];
    }

    /*  -- Method Header Comment
	    * Name	    :	VerifyHttpSpecification
	    * Purpose   :	This method confirms that the request is in specification.
	    * Inputs	:	NONE
	    * Returns	:	Bool - true if valid otherwise false
        */
    public bool VerifyHttpSpecification()
    {
        bool isValid = false;
        if (HttpSpecification == "HTTP/1.1" && HostLabel == "Host:")
        {
            isValid = true;
        }
        return isValid;
    }

    /*  -- Method Header Comment
	    * Name	    :	VerifyRequest
	    * Purpose   :	This method verifies that the server is receiving a post method.
	    * Inputs	:	NONE
	    * Returns	:	Bool - true if valid otherwise false
        */
    public bool VerifyRequest()
    {
        bool result;
        if (RequestType == "GET")
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    /*  -- Method Header Comment
	    * Name	    :	VerifyEndLine
	    * Purpose   :	This method verifies that the servers receives a "required blank line"
	    *               at the end of the request.
	    * Inputs	:	NONE
	    * Returns	:	Bool - true if valid otherwise false
        */
    public bool VerifyEndLine()
    {
        bool isValid = false;

        // make sure that the user doesn't send a message that is clearly invalid
        if (RequestSplit.Length > 2)
        {
            // get second last and last split strings
            string secondLast = RequestSplit[RequestSplit.Length - 2];
            string last = RequestSplit[RequestSplit.Length - 1];

            // verify that the "required blank line" exists
            if (secondLast == "" && last == "")
            {
                isValid = true;
            }
        }
        return isValid;
    }

    /*  -- Method Header Comment
	    * Name	    :	VerifyResourceExtensionHtmlFiles
	    * Purpose   :	This method verifies that the resource is an HTML file.
	    *               at the end of the request.
	    * Inputs	:	NONE
	    * Returns	:	Bool - true if valid otherwise false
        */
    public bool VerifyResourceExtensionHtmlFiles()
    {
        bool isValid = false;

        // HTML files includes the following extensions
        if (ResourceExtension == "html" || ResourceExtension == "htm" || ResourceExtension == "htmls" || ResourceExtension == "htx")
        {
            isValid = true;
        }
        return isValid;
    }

    /*  -- Method Header Comment
	    * Name	    :	VerifyResourceExtensionHttFile
	    * Purpose   :	This method that the resource is an HTT file (family of HTML).
	    * Inputs	:	NONE
	    * Returns	:	Bool - true if valid otherwise false
        */
    public bool VerifyResourceExtensionHttFile()
    {
        bool isValid = false;
        if (ResourceExtension == "htt")
        {
            isValid = true;
        }
        return isValid;
    }

    /*  -- Method Header Comment
	    * Name	    :	VerifyResourceExtensionTxtFiles
	    * Purpose   :	This method verifies that the resource is an TXT file.
	    *               at the end of the request.
	    * Inputs	:	NONE
	    * Returns	:	Bool - true if valid otherwise false
        */
    public bool VerifyResourceExtensionTxtFiles()
    {
        bool isValid = false;
        if (ResourceExtension == "txt")
        {
            isValid = true;
        }
        return isValid;
    }

    /*  -- Method Header Comment
	    * Name	    :	VerifyResourceExtensionJpgImages
	    * Purpose   :	This method verifies that the resource is one of the JPG family.
	    *               at the end of the request.
	    * Inputs	:	NONE
	    * Returns	:	Bool - true if valid otherwise false
        */
    public bool VerifyResourceExtensionJpgImages()
    {
        bool isValid = false;
        if (ResourceExtension == "jpg" || ResourceExtension == "jpeg" || ResourceExtension == "pjp" || ResourceExtension == "jfif" || ResourceExtension == "jfif")
        {
            isValid = true;
        }
        return isValid;
    }

    /*  -- Method Header Comment
	    * Name	    :	VerifyResourceExtensionGif
	    * Purpose   :	This method verifies that the resource is an GIF file.
	    *               at the end of the request.
	    * Inputs	:	NONE
	    * Returns	:	Bool - true if valid otherwise false
        */
    public bool VerifyResourceExtensionGif()
    {
        bool isValid = false;
        if (ResourceExtension == "gif")
        {
            isValid = true;
        }
        return isValid;
    }
}
