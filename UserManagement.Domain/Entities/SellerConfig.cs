using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
    public class SellerConfig
    {
        public int Id { get; set; }
        public string FundTracking { get; set; }

        public bool Status { get; set; }
    }
}
