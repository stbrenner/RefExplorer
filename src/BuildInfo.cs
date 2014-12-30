//-----------------------------------------------------------------------------
// BuildInfo.cs
//
// Author: Stephan Brenner
// Date:   08/20/2006
//-----------------------------------------------------------------------------

using System.Reflection;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyCompany("Stephan Brenner")]
[assembly: AssemblyProduct(".NET Reference Explorer")]
[assembly: AssemblyCopyright("© Stephan Brenner 2006-2014")]
[assembly: AssemblyTrademark("")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.6.0.0")]
[assembly: AssemblyFileVersion("1.6.0.0")]

#if DEBUG
[assembly: AssemblyDescription("DEBUG")]
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyDescription("RELEASE")]
[assembly: AssemblyConfiguration("RELEASE")]
#endif