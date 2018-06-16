using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class People:BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
