namespace EasyDapper.Data.Dapper.Tests.Data.Dummy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SimpleModel
    {
        public string Name { get; set; }
        public IEnumerable<int> Properties { get; set; }
    }
}
