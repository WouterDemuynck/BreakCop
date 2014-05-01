using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace BreakCop.PowerShell
{
    [Cmdlet(VerbsData.Compare, "Assemblies")]
    [CLSCompliant(false)]
    public class CompareAssembliesCmdlet : Cmdlet
    {
        public CompareAssembliesCmdlet()
        {
        }

        [Parameter(Position = 0, Mandatory = true)]
        public string Source
        {
            get;
            set;
        }

        [Parameter(Position = 1, Mandatory = true)]
        public string Target
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            AssemblyComparer comparer = new AssemblyComparer();
            var changes = comparer.Compare(Source, Target);
            this.WriteObject(changes.ToArray());
        }
    }
}
