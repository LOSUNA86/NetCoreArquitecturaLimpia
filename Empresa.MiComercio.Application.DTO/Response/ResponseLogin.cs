using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.MiComercio.Application.DTO.Response
{
    public class ResponseLogin
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }        
        public string Token { get; set; }
    }
}
