using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string AssetAddress { get; set; }
    }
}
