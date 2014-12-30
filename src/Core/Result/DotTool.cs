//-----------------------------------------------------------------------------
// $Workfile$
//
// Author: SNB
// Date:   09/23/2006
//-----------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using RefExplorer.Core.Configuration;

namespace RefExplorer.Core.Result
{
    public class DotTool
    {
        private ExplorerConfiguration configuration;

        public DotTool(ExplorerConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void DrawGraph(ReferenceResult referenceResult)
        {
            var diGraph = new DiGraph(referenceResult, configuration);
            string imageFile = Path.Combine(configuration.BaseDirectory.FullName, referenceResult.ImageFileName);
            DrawGraph(diGraph, imageFile);
        }

        public void DrawGraph(DiGraph diGraph, string outputPath)
        {
            string diGraphPath = Path.Combine(configuration.BaseDirectory.FullName, "RefExp-Dot.txt");
            using (FileStream fileStream = new FileStream(diGraphPath, FileMode.Create))
            {
                diGraph.Write(fileStream);
            }

            DrawGraph(diGraphPath, outputPath);
        }

        public void DrawGraph(string dotFilePath, string outputPath)
        {
            var process = new Process();
            process.StartInfo.FileName = Path.Combine(configuration.BaseDirectory.FullName, "dot.exe");
            process.StartInfo.Arguments = string.Format("-Tpng \"{0}\" -o \"{1}\"", dotFilePath, outputPath);
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception(string.Format("dot.exe exited with error code {0}.", process.ExitCode));
            }
        }

    }
}
