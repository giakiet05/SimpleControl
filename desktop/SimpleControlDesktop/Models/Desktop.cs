using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Models
{
    public class Desktop
    {
        public Guid Id = Guid.NewGuid();
        public string Name { get; set; }
    }
}
