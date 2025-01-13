using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Models
{
    public class Connection
    {
        public Guid DesktopId { get; set; }
        public Guid UserPhoneId { get; set; }

        public DateTime ConnectedSince { get; set; }
        public string Status { get; set; }
        //Navigation Props
        public Desktop Desktop { get; set; }
        public UserPhone UserPhone { get; set; }

    }
}
