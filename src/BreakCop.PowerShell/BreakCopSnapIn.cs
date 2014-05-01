using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BreakCop.PowerShell
{
    [RunInstaller(true)]
    [CLSCompliant(false)]
    public sealed class BreakCopSnapIn : PSSnapIn
    {
        public override string Description
        {
            get { return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>().Description; }
        }

        public override string Name
        {
            get { return "BreakCopSnapIn"; }
        }

        public override string Vendor
        {
            get { return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company; }
        }
    }
}
