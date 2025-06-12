using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovision.Core.Infrastructure.PaymentServices.Models
{
    public class AccountBalance
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; }
        public  DateTimeOffset DateUpdated { get; set; }
    }
}
