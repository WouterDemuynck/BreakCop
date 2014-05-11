BreakCop README
===============

You have just installed the BreakCop NuGet package. It has prepared your solution for
supporting the BreakCop MSBuild tasks and registered it with the target project in your 
solution. It has also added a placeholder ReferenceAssembly MSBuild property to the
project file.

To start guarding your project from breaking API changes, you should unload your project 
and edit the raw MSBuild file, find and edit the <ReferenceAssembly> property and replace
its value with the full or relative path to the older version with which you want to be
compatible:

<PropertyGroup>
	<ReferenceAssembly>\\BUILDSERVER\Drops\MyLibrary\v1.0\MyLibrary.dll</ReferenceAssembly>
</PropertyGroup>

By default, BreakCop will fail the build if any breaking changes are detected, but you
can customize this to only warn you of breaking changes like this:

<PropertyGroup>
	<FailOnBreakingChanges>false</FailOnBreakingChanges>
</PropertyGroup>
