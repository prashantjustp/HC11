using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC11.Models
{
    [Node]
    public class TestHC11
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
