using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Models
{
    public class UserPhone
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PhoneId { get; set; }

        public User User { get; set; }
        public Phone Phone { get; set; }
    }
}
