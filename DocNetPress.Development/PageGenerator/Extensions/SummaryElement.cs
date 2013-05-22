﻿using DocNetPress.Development.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocNetPress.Development.PageGenerator.Extensions
{
    /// <summary>
    /// Generates the summary part of a documentation page
    /// </summary>
    public class SummaryElement : PageElement, IPostElement
    {
        /// <summary>
        /// Generates HTML-Code from the given assembly member ready to insert into the post content
        /// </summary>
        /// <param name="dllPath">The path to the DLL file for member access using reflection</param>
        /// <param name="nodeType">The type of the given documentation node</param>
        /// <param name="nodeContent">The content of the read member node</param>
        /// <param name="nodeMemberAttribute">The "member"-Attribute text</param>
        /// <param name="culture">The culture to output the HTML-Code in</param>
        /// <returns>The generated documentation HTML-Code ready to insert into the post content</returns>
        public String GetPostContent(String dllPath, String nodeContent, MemberType nodeType, String nodeMemberAttribute, CultureInfo culture = null)
        {
            XmlDocument.LoadXml(nodeContent);
            String summary = XmlDocument["summary"].InnerText;

            if (summary != null)
            {
                using (StringWriter sw = new StringWriter())
                using (var xWriter = XmlWriter.Create(sw))
                {
                    xWriter.WriteElementString("h" + HeadlineLevel.ToString(), Strings.Summary);
                    xWriter.WriteString(Environment.NewLine + summary);

                    return sw.ToString();
                }
            }
            
            return null;
        }
    }
}