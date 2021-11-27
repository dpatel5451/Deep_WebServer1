using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer.Error
{
    static class ErrorType
    {
        enum ErrorTypes : short
        {
            CONTINUE = 100,
            OK = 200,
            MOVED_PERMANEMTNLY = 301,
            NOT_FOUND = 404,
            SERVER_ERROR = 500
        }
    }
}
