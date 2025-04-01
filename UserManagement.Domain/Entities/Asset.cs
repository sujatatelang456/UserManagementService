﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
    public class Asset
    {
        public int AssetId { get; set; }
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string AssetAddress { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}
