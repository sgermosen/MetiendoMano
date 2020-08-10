using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralWebHelpers.Structs
{
    public class JsonResponseBase
    {
        public static object ErrorResponse(string error)
        {
            return new { Success = false, Message = error };
        }

        public static object SuccessResponse()
        {
            return new { Success = true };
        }

        public static object SuccessResponse(object referenceObject)
        {
            return new { Success = true, Object = referenceObject };
        }

        public static object SuccessResponse(string Message)
        {
            return new { Success = true, Message = Message };
        }

        public static object SuccessResponse(dynamic referenceObject, String Messsage)
        {
            return new { Success = true, Object = referenceObject, Message = Messsage };
        }
    }
}