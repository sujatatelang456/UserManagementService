using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Entities
{
    public class ManualTask
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
    }
}
