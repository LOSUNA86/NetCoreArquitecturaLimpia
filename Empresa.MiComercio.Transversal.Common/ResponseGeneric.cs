using System.Net;

namespace Empresa.MiComercio.Transversal.Common
{
    public class ResponseGeneric<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<BaseError> Errors { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
