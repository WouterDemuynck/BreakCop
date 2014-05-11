param($installPath, $toolsPath, $package, $project)

Write-Host 'Setting up BreakCop...'
$solutionPath = (Get-Item $installPath).Parent.Parent.FullName
$targetsPath = "$solutionPath\.breakcop\BreakCop.targets"

# Make the path to the targets file relative...
$projectUri = new-object Uri('file://' + $project.FullName)
$targetsUri = new-object Uri('file://' + $targetsPath)
$relativePath = $projectUri.MakeRelativeUri($targetsUri).ToString().Replace([System.IO.Path]::AltDirectorySeparatorChar, [System.IO.Path]::DirectorySeparatorChar)
 
# Add the properties if they're not present...
Add-Type -AssemblyName 'Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
$msbuildProject =  [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($project.FullName) | Select-Object -First 1
$referenceAssemblyProperty = $msbuildProject.Xml.Properties | Where-Object { $_.Name -eq 'ReferenceAssembly' }
if (!$referenceAssemblyProperty) {
	Write-Host 'Adding ReferenceAssembly property...'
	$msbuildProject.Xml.AddProperty('ReferenceAssembly', 'PLACEHOLDER')
}

# Add the BreakCop.targets file if necessary...
$import = $msbuildProject.Xml.Imports | Where-Object { $_.Project.EndsWith('BreakCop.targets') }
if (!$import) {
	Write-Host 'Referencing BreakCop MSBuild targets file...'
	$msbuildProject.Xml.AddImport($relativePath)
}

# Save the changes...
$project.Save()
$msbuildProject.Save()

