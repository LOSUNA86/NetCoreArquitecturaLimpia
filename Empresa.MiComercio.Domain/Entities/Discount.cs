using Empresa.MiComercio.Domain.Common;
using Empresa.MiComercio.Domain.Enums;

namespace Empresa.MiComercio.Domain.Entities
{
    public class Discount : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Percent { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
