using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class RoleUser
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
