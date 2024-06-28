using Empresa.MiComercio.Transversal.Common;
using Empresa.MiComercio.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.MiComercio.Application.Interface.UseCases
{
    public interface ICategoriesApplication
    {
        Task<Response<IEnumerable<ResponseCategorie>>> GetAll();
    }
}
