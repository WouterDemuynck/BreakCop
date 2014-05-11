using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace BreakCop.MSBuildTasks
{
    public class CompareAssemblies : Task
    {
        public CompareAssemblies()
        {
            FailOnBreakingChanges = true;
        }

        [Required]
        public string Source
        {
            get;
            set;
        }

        [Required]
        public string Target
        {
            get;
            set;
        }

        [DefaultValue(true)]
        public bool FailOnBreakingChanges
        {
            get;
            set;
        }

        public override bool Execute()
        {
            if (Source != null && !File.Exists(Source))
            {
                Log.LogWarning("BreakCop could not find the reference assembly to compare with. Have you configured the ReferenceAssembly MSBuild property?");
                return true;
            }

            AssemblyComparer comparer = new AssemblyComparer();
            var changes = comparer.Compare(Source, Target).ToList();

            var breakingChanges = changes.Where(c => c.Category == ChangeCategory.Breaking).ToList();
            Action<string, string, string, string, int, int, int, int, string> action;
            
            if (FailOnBreakingChanges) action = (subcategory, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message) => Log.LogError(subcategory, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message);
            else action = (subcategory, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message) => Log.LogWarning(subcategory, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message);

            breakingChanges.ForEach(c => action("BreakCop", string.Format("BC{0:000}", (int)c.ChangeType), null, Target, 0, 0, 0, 0, c.ToString()));
            if (FailOnBreakingChanges && breakingChanges.Any()) return false;

            var nonBreakingChanges = changes.Except(breakingChanges).ToList();
            nonBreakingChanges.ForEach(c => Log.LogMessage("BreakCop", string.Format("BC{0:000}", (int)c.ChangeType), null, Target, 0, 0, 0, 0, c.ToString()));

            return true;
        }
    }
}
