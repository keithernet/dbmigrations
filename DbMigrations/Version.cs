using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMigrationHelpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute: System.Attribute
    {
        public readonly int Version;

        public VersionAttribute(int version)
        {
            this.version = version;
        }
    }
}
