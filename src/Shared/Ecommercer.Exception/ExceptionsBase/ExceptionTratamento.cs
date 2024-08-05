using System.Collections.Generic;
using System.Net;

namespace Ecommercer.Exception.ExceptionsBase
{
    public abstract class ExceptionTratamento : SystemException
    {
        protected ExceptionTratamento(string message) : base(message) { }

        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
