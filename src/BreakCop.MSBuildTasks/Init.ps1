param($installPath, $toolsPath, $package, $project)

# Get the root folder of the current solution.
$solutionPath = (Get-Item $installPath).Parent.Parent.FullName
Write-Host "Installing BreakCop in the solution..."

# Create the .breakcop folder if it doesn't exist yet.
$breakcopFolder = "$solutionPath\.breakcop\"
if (!(Test-Path $breakcopFolder)) {
	MkDir $breakcopFolder
}

# Copy the required files to the .breakcop folder.
Copy-Item (Join-Path $toolsPath 'Mono.Cecil.dll') $breakcopFolder -Force
Copy-Item (Join-Path $toolsPath 'BreakCop.dll') $breakcopFolder -Force
Copy-Item (Join-Path $toolsPath 'BreakCop.MSBuildTasks.dll') $breakcopFolder -Force
Copy-Item (Join-Path $toolsPath 'BreakCop.targets') $breakcopFolder -Force

Write-Host "Creating solution items..."
$solution = Get-Interface $dte.Solution ([EnvDTE80.Solution2])
 
# Create the solution folder.
$breakcopSolutionFolder = $solution.Projects | where-object { $_.ProjectName -eq ".breakcop" } | select -first 1
if(!$breakcopSolutionFolder) {
	$breakcopSolutionFolder = $solution.AddSolutionFolder(".breakcop")
}

# Add the solution folder items.
$breakcopSolutionFolderItems = Get-Interface $breakcopSolutionFolder.ProjectItems ([EnvDTE.ProjectItems])
 
$breakcopSolutionFolderItems.AddFromFile((Join-Path $breakcopFolder 'Mono.Cecil.dll'))
$breakcopSolutionFolderItems.AddFromFile((Join-Path $breakcopFolder 'BreakCop.dll'))
$breakcopSolutionFolderItems.AddFromFile((Join-Path $breakcopFolder 'BreakCop.MSBuildTasks.dll'))
$breakcopSolutionFolderItems.AddFromFile((Join-Path $breakcopFolder 'BreakCop.targets'))
