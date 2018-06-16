using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
    public static class Extensions
    {
        public static IDictionary<string, string> ToDictionary(this object value) =>
            value
                .GetType()
                .GetProperties()
                .ToDictionary(x => x.Name, x => x.GetValue(value).ToString());

    }
}
