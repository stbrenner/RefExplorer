.NET Reference Explorer
=======================

The .NET Reference Explorer (in short RefExplorer) is a Windows software to display dependencies between .NET assemblies.

You can use it for verifying your software architecture or for finding bugs. The result is a directed graph which shows the referenced assemblies and if they could be successfully loaded. Dependencies to legacy DLLs (via P/Invoke) are also displayed.

<table>
<tr><th>Binary Download:</th><td><a href="../../releases/download/v1.6/RefExplorer_1.6.zip"><strong>RefExplorer_1.6.zip</strong></a> (2 MB)</td></tr>
<tr><th>Operating System:</th><td>Windows 7 or newer</td></tr>
<tr><th>License:</th><td><a href="/LICENSE?raw=true">MIT</a></td></tr>
</table>

![Screenshot](/screenshot.png?raw=true)

It is very easy to use: Just add the assembly you want to inspect as entry point. You even can add a complete folder or assemblies from the global assembly cache (GAC).

If any references are corrupt, a detailed error message gets displayed and the position is accordingly marked in the graph.

Development Environment
-----------------------

The solution can be built with Visual Studio 2013. The necessary dependencies are downloaded automatically via NuGet.

```src\BuildRelease.bat``` builds the whole solution, merges the assemblies and stores the binaries in a ZIP file. Before it is executed, ```3p\MSBuildCommunityTasks``` should be copied to ```C:\Program Files (x86)\MSBuild```.

