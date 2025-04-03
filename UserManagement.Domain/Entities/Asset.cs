using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
    public class Asset
    {
        public string AssetId { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LoanNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string TransactionType { get; set; }
        public string Client { get; set; }
        public string SellerCode { get; set; }
        public string AssetStatus { get; set; }
        public string PropertyType { get; set; }
        public string AuctionFlag { get; set; } // Yes / No
        public DateTime SaleDate { get; set; }
    }
}
