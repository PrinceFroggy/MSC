using System;
using System.Net;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using System.IO;


namespace sorprendente
{
    /// <summary>
    /// The map name logic.
    /// </summary>
    class Map : Dictionary<int, string>
    {
        #region Variable

        private bool _isExecutedOnce = false;
        private string rawData;

        #endregion

        public Map()
        {
            if (!_isExecutedOnce)
            {
                try
                {
                    rawData = sorprendente.Properties.Resources.MAP.ToString();

                    _isExecutedOnce = true;
                }
                catch
                {
                    _isExecutedOnce = false;
                }
            }
            PopulateWith(rawData);
        }

        void PopulateWith(string rawText)
        {
            string pattern = @"ID: (?<id>\d*) NAME: (?<name>.*)";

            foreach (Match match in Regex.Matches(rawText, pattern))
            {
                int id = int.Parse(match.Groups["id"].Value);
                string name = match.Groups["name"].Value;

                this[id] = name;
            }
        }
    }

    /// <summary>
    /// The map dimensions logic.
    /// </summary>
    static class MDimensions
    {
        public static int GetInt(XElement data, string attribName)
        {
            var element = data.Descendants("int").FirstOrDefault(x => (string)x.Attribute("name") == attribName);

            if (element == null)
                return 0;

            var value = (int?)element.Attribute("value");

            if (value == null)
                return 0;

            return value.Value;
        }
    }

    /// <summary>
    /// The map draw logic.
    /// </summary>
    static class MDraw
    {
        public static void DrawPortal(XElement data, Graphics g, Image i, int cx, int cy, int rx, int ry)
        {
            var interestingImgDirs = data.XPathSelectElements("/imgdir[@name='portal']/imgdir[(string[@name='pn' and @value!='sp']) and (int[@name='tm' and @value!=999999999] )]");

            foreach (var el in interestingImgDirs)
            {
                var lX = int.Parse((string)el.XPathSelectElement("./int[@name='x']").Attribute("value"));
                var lY = int.Parse((string)el.XPathSelectElement("./int[@name='y']").Attribute("value"));

                int portalX = (cx / rx) + (lX / rx);
                int portalY = (cy / ry) + (lY / ry);

                g.DrawImage(i, portalX - 6, portalY - 12);
            }
        }

        public static void DrawPlayer(Graphics g, Image i, int x, int y)
        {
            g.DrawImage(i, x - 6, y - 10);
        }
    }
}