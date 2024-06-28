using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Transversal.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.MiComercio.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery
{
    public sealed record GetAllWithPaginationCustomerQuery : IRequest<ResponsePagination<IEnumerable<CustomersDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
