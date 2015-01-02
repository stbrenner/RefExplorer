RefExplorer
===========

Tool to display dependencies between .NET assemblies

You can use it for verifying your software architecture or for finding bugs. The result is a directed graph which shows the referenced assemblies and if they could be successfully loaded. Dependencies to legacy DLLs (via P/Invoke) are also displayed.

Build environment: Visual Studio 2013   
License:  MIT   
Binary download: [RefExplorer_1.6.zip](https://github.com/ymx/RefExplorer/releases/download/v1.6/RefExplorer_1.6.zip)   
Operating systems: Windows 7 or newer   

It is very easy to use: Just add the assembly you want to inspect as entry point. You even can add a complete folder or assemblies from the global assembly cache (GAC).

If any references are corrupt, a detailed error message gets displayed and the position is accordingly marked in the graph.
