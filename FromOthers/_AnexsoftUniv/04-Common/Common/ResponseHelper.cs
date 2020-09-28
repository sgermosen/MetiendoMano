using System.Collections.Generic;

namespace Common
{
    public abstract class ResponseHelperBase
    {
        public bool Response { get; set; }
        public string Message { get; set; }
        public string Function { get; set; }
        public string Href { get; set; }
        public Dictionary<string, string> Validations { get; set; }

        public void SetValidations(Dictionary<string, string> vals)
        {
            if (vals != null && vals.Count > 0)
            {
                Validations = vals;
                PrepareResponse(false, "La validación no ha sido superada");
            }
        }

        protected void PrepareResponse(bool r, string m = "")
        {
            Response = r;

            if (r)
            {
                Message = m;
            }
            else
            {
                Message = (m == "" ? "Ocurrió un error inesperado" : m);
            }
        }

        public ResponseHelperBase()
        {
            PrepareResponse(false);
        }
    }

    public class ResponseHelper : ResponseHelperBase
    {
        public dynamic Result { get; set; }

        public ResponseHelper SetResponse(bool r, string m = "")
        {
            PrepareResponse(r, m);
            return this;
        }
    }

    public class ResponseHelper<T> : ResponseHelperBase where T : class
    {
        public T Result { get; set; }

        public ResponseHelper<T> SetResponse(bool r, string m = "")
        {
            PrepareResponse(r, m);
            return this;
        }
    }
}
