using System.Net;

namespace GestionRepuestoAPI.Modelos
{
    public class RespuestaAPI
    {
        public RespuestaAPI()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public string Message { get; set; } = string.Empty;

        public bool Success { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        public object Result { get; set; } = null;
    }
}
