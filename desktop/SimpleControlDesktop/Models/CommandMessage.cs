using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Models
{
    public class CommandMessage
    {
        public string Command { get; set; }
        public JsonElement Value { get; set; }
        public string? AccessToken { get; set; }
    }
}
