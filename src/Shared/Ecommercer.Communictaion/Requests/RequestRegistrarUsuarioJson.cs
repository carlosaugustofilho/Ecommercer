using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercer.Communictaion.Requests
{
    public class RequestRegistrarUsuarioJson
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; } 
    }
}
