//-----------------------------------------------------------------------------
// File:    XhtmlGenerator.cs
// Author:  snb
// Created: 12/24/2006
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml;
using RefExplorer.Core.Configuration;
using RefExplorer.Core.Result;
using System.IO;

namespace RefExplorer.Core.Implementation
{
    internal class XhtmlGenerator
    {
        private readonly ExplorerConfiguration configuration;
        private const string Xmlns = "http://www.w3.org/1999/xhtml";

        public XhtmlGenerator(ExplorerConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Generate(ReferenceResult result)
        {
            var document = new XmlDocument();
            XmlElement html = document.CreateElement("html", Xmlns);
            document.AppendChild(html);
            RenderHeader(document, html);
            RenderBody(document, html, result);
            document.Save(Path.Combine(configuration.BaseDirectory.FullName, configuration.OutputFileName));
        }

        private void RenderBody(XmlDocument document, XmlElement html, ReferenceResult result)
        {
            XmlElement body = document.CreateElement("body", Xmlns);

            RenderFrameworkVersion(body);
            RenderPaths(body);

            int exceptionCount = RenderExceptions(result.References.Values, body);
            if (exceptionCount == 0)
            {
                RenderSuccess(body);
            }

            if (!string.IsNullOrEmpty(result.ImageFileName))
            {
                RenderImage(body, document, html, result.ImageFileName);
            }
        }

        private void RenderPaths(XmlElement body)
        {
            XmlElement p = body.OwnerDocument.CreateElement("p", Xmlns);
            foreach (AssemblyPath path in configuration.AssemblyPaths)
            {
                p.AppendChild(body.OwnerDocument.CreateTextNode(path.ToString()));
                p.AppendChild(body.OwnerDocument.CreateElement("br", Xmlns));
            }
            body.AppendChild(p);
        }

        private void RenderFrameworkVersion(XmlElement body)
        {
            XmlElement p = body.OwnerDocument.CreateElement("p", Xmlns);
            p.AppendChild(body.OwnerDocument.CreateTextNode(string.Format(".NET Framework {0}", Environment.Version)));
            body.AppendChild(p);
        }        
        
        private static void RenderHeader(XmlDocument document, XmlElement html)
        {
            XmlElement header = document.CreateElement("header", Xmlns);
            html.AppendChild(header);

            XmlElement title = document.CreateElement("title", Xmlns);
            title.InnerText = "Result";    // O-XXX-SNB/SNB: Add title
            header.AppendChild(title);

            XmlElement style = document.CreateElement("style", Xmlns);
            style.InnerText = "body {font: 0.8em Tahoma, Arial; margin: 5px}" +
                              "p {margin-bottom: 0.5em; margin-top: 0.5em}" +
                              ".success {color: Green;}" +
                              ".exception {color: Red;}";
            header.AppendChild(style);
        }

        private static void RenderImage(XmlElement body, XmlDocument document, XmlElement html, string imageFile)
        {
            html.AppendChild(body);
            XmlElement img = document.CreateElement("img", Xmlns);
            img.SetAttribute("src", imageFile);
            body.AppendChild(img);
        }

        private void RenderSuccess(XmlElement body)
        {
            XmlElement p = body.OwnerDocument.CreateElement("p", Xmlns);
            p.SetAttribute("class", "success");
            p.InnerText = "All references could be found and loaded.";
            body.AppendChild(p);
        }

        private int RenderExceptions(ICollection<AssemblyResult> infos, XmlElement body)
        {
            int count = 0;
            foreach (AssemblyResult assemblyResult in infos)
            {
                count += HandleException(assemblyResult.Exception, body);
                foreach (AssemblyInfo assemblyInfo in assemblyResult.Assemblies.Values)
                {
                    count += HandleException(assemblyInfo.Exception, body);
                    foreach (ReferenceInfo referenceInfo in assemblyInfo.References.Values)
                    {
                        count += HandleException(referenceInfo.Exception, body);
                    }
                }
            }
            return count;
        }

        private int HandleException(Exception exception, XmlElement body)
        {
            if (exception != null)
            {
                XmlElement p = body.OwnerDocument.CreateElement("p", Xmlns);
                p.SetAttribute("class", "exception");
                p.InnerText = exception.Message;
                body.AppendChild(p);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}