//-----------------------------------------------------------------------------
// ReferenceExplorer.cs
//
// Author: Stephan Brenner
// Date:   06/09/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using RefExplorer.Core.Configuration;
using RefExplorer.Core.Implementation;
using RefExplorer.Core.Result;

namespace RefExplorer.Core
{
    public class ExplorerEngine
    {
        private readonly ReferenceResult result = new ReferenceResult();

        public ExplorerEngine()
            : this(new ExplorerConfiguration())
        { }

        public ExplorerEngine(ExplorerConfiguration explorerConfiguration)
        {
            Configuration = explorerConfiguration;
        }

        public ExplorerConfiguration Configuration { get; set; }

        public ReferenceResult Result { get { return result; } }

        public void Explore()
        {
            result.References.Clear();

            var infoReader = new AssemblyInfoReader(Configuration);
            infoReader.Execute(Result.References);

            var dotTool = new DotTool(Configuration);
            dotTool.DrawGraph(Result);

            var xhtmlGenerator = new XhtmlGenerator(Configuration);
            xhtmlGenerator.Generate(Result);
        }
    }
}
