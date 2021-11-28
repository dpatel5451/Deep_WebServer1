/*
* Filename		:	ServerMessage.cs
* Project		:	PROG2001 - Assignment 06 
* Programmer	:	Deep Patel & Sean Mccarthy
* First Version	:	24/11/2021
* Description	:	It will verify that user input matches the requirements. 
*                   It will verify user entered webRoot, webIP & webPort.
*                   And will return the input accordingly after verifying.
*/


using System;

public class ClientRequest
{
    public string Request { get; private set; }
    public string[] RequestSplit { get; private set; }
    public string RequestType { get; private set; }
    public string Resource { get; private set; }
    public string ResourceExtension { get; private set; }
    public string HttpSpecification { get; private set; }
    public string HostLabel { get; private set; }
    public string Host { get; private set; }

    public ClientRequest(string inData)
	{
        Request = inData;
        RequestSplit = Request.Split(new string[] { "\r\n", " " }, StringSplitOptions.None);
        RequestType = RequestSplit[0];
        Resource = RequestSplit[1];

        if (Resource.Contains("."))
        {
            ResourceExtension = Resource.Substring(Resource.LastIndexOf(".") + 1);
        }

        HttpSpecification = RequestSplit[2];
        HostLabel = RequestSplit[3];
        Host = RequestSplit[4];
    }

    public bool VerifyHttpSpecification()
    {
        bool isValid = false;
        if (HttpSpecification == "HTTP/1.1")
        {
            isValid = true;
        }
        return isValid;
    }

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

    public bool VerifyEndLine()
    {
        bool isValid = false;
        if (RequestSplit.Length > 2)
        {
            string secondLast = RequestSplit[RequestSplit.Length - 2];
            string last = RequestSplit[RequestSplit.Length - 1];

            if (secondLast == "" && last == "")
            {
                isValid = true;
            }
        }
        return isValid;
    }

    public bool VerifyResourceExtensionHtmlFiles()
    {
        bool isValid = false;
        if (ResourceExtension == "html" || ResourceExtension == "htm" || ResourceExtension == "htmls" || ResourceExtension == "htx")
        {
            isValid = true;
        }
        return isValid;
    }

    public bool VerifyResourceExtensionHttFile()
    {
        bool isValid = false;
        if (ResourceExtension == "htt")
        {
            isValid = true;
        }
        return isValid;
    }

    public bool VerifyResourceExtensionTxtFiles()
    {
        bool isValid = false;
        if (ResourceExtension == "txt")
        {
            isValid = true;
        }
        return isValid;
    }

    public bool VerifyResourceExtensionJpgImages()
    {
        bool isValid = false;
        if (ResourceExtension == "jpg" || ResourceExtension == "jpeg" || ResourceExtension == "pjp" || ResourceExtension == "jfif" || ResourceExtension == "jfif")
        {
            isValid = true;
        }
        return isValid;
    }

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
