using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercer.Exception.ExceptionsBase
{
    public class ErroException : ExceptionTratamento
    {
        public IList<string> ErroMessages { get; set; }

        public ErroException(IList<string> errorMessages) : base(string.Join("; ", errorMessages))
        {
            ErroMessages = errorMessages;
        }

        public override IList<string> GetErrorMessages() => ErroMessages;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
