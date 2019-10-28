using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using IO = System.IO;
using CorelEstimator.Entities;
using CorelEstimator.Geometries;
using CorelEstimator.Extensions;

namespace CorelEstimator.Managers
{
    public static class InfoReadingManager
    {
        public static void ReadSvcInfo(Estimation estimation)
        {
            XDocument doc = LoadXml(estimation.FilePath);
            ReadGeometries(estimation, doc.NsElement("svg"));
        }

        private static XDocument LoadXml(string xmlFilePath)
        {
            var xml = new StringBuilder();
            xml.Append(IO.File.ReadAllText(xmlFilePath));
            //XDocument does not like uppercase xml symbols, but corel generates them. 
            //A small hack to avoid this problem:
            xml.Replace("&AMP;", "&amp;");
            xml.Replace("&LT;", "&lt;");
            xml.Replace("&GT;", "&gt;");
            xml.Replace("&APOS;", "&apos;");
            xml.Replace("&QUOT;", "&quot;");
            var reader = new IO.StringReader(xml.ToString());
            XDocument doc = XDocument.Load(reader);
            return doc;
        }

        private static void ReadGeometries(Estimation estimation, XElement node)
        {
            ReadLines(estimation, node);
            ReadRectangles(estimation, node);
            ReadCircles(estimation, node);
            ReadEllipse(estimation, node);
            ReadImage(estimation, node);
            ReadPath(estimation, node);
            ReadPolygon(estimation, node);
            ReadPolyLine(estimation, node);
            ReadText(estimation, node);

            ReadGroups(estimation, node);
        }

        private static void ReadGroups(Estimation estimation, XElement node)
        {
            foreach (XElement group in node.NsElements("g"))
            {
                ReadGeometries(estimation, group);
            }
        }

        private static void ReadLines(Estimation estimation, XElement node)
        {
            foreach (XElement lineNode in node.NsElements("line"))
            {
                Line line = new Line();
                line.StartX = ReadAttribute(lineNode, "x1");
                line.StartY = ReadAttribute(lineNode, "y1");
                line.EndX = ReadAttribute(lineNode, "x2");
                line.EndY = ReadAttribute(lineNode, "y2");
                estimation.Geometries.Add(line);
            }
        }

        private static double ReadAttribute(XElement node, string attributeName)
        {
            XAttribute attribute = node.Attribute(attributeName);
            if (attribute == null)
            {
                return 0;
            }
            else
            {
                return ParseCoordinate(attribute.Value);
            }
        }

        private static string ReadStringAttribute(XElement node, string attributeName)
        {
            return node.Attribute(attributeName).Value;
        }

        private static void ReadRectangles(Estimation estimation, XElement node)
        {
            foreach (XElement rectangleNode in node.NsElements("rect"))
            {
                Rectangle rect = new Rectangle();
                rect.StartX = ReadAttribute(rectangleNode, "x");
                rect.StartY = ReadAttribute(rectangleNode, "y");
                rect.Width = ReadAttribute(rectangleNode, "width");
                rect.Height = ReadAttribute(rectangleNode, "height");
                estimation.Geometries.Add(rect);
            }
        }

        private static void ReadCircles(Estimation estimation, XElement node)
        {
            foreach (XElement circleNode in node.NsElements("circle"))
            {
                Circle circle = new Circle();
                circle.X = ReadAttribute(circleNode, "cx");
                circle.Y = ReadAttribute(circleNode, "cy");
                circle.Radius = ReadAttribute(circleNode, "r");
                estimation.Geometries.Add(circle);
            }
        }

        private static void ReadEllipse(Estimation estimation, XElement node)
        {
            foreach (XElement ellipseNode in node.NsElements("ellipse"))
            {
                Ellipse ellipse = new Ellipse();
                ellipse.X = ReadAttribute(ellipseNode, "cx");
                ellipse.Y = ReadAttribute(ellipseNode, "cy");
                ellipse.XRadius = ReadAttribute(ellipseNode, "rx");
                ellipse.YRadius = ReadAttribute(ellipseNode, "ry");
                estimation.Geometries.Add(ellipse);
            }
        }

        private static void ReadImage(Estimation estimation, XElement node)
        {
            foreach (XElement imageNode in node.NsElements("image"))
            {
                Image image = new Image();
                image.X = ReadAttribute(imageNode, "x");
                image.Y = ReadAttribute(imageNode, "y");
                image.Width = ReadAttribute(imageNode, "width");
                image.Height = ReadAttribute(imageNode, "height");
                estimation.Geometries.Add(image);
            }
        }

        private static void ReadPath(Estimation estimation, XElement node)
        {
            foreach (XElement pathNode in node.NsElements("path"))
            {
                Path image = new Path();
                string path = ReadStringAttribute(pathNode, "d");
                image.PathData = path;
                estimation.Geometries.Add(image);
            }
        }

        private static void ReadPolygon(Estimation estimation, XElement node)
        {
            foreach (XElement polygonNode in node.NsElements("polygon"))
            {
                Polygon polygon = new Polygon();
                string points = ReadStringAttribute(polygonNode, "points");
                polygon.PointData = points;
                estimation.Geometries.Add(polygon);
            }
        }

        private static void ReadPolyLine(Estimation estimation, XElement node)
        {
            foreach (XElement polyLineNode in node.NsElements("polyline"))
            {
                PolyLine polyLine = new PolyLine();
                string points = ReadStringAttribute(polyLineNode, "points");
                polyLine.PointData = points;
                estimation.Geometries.Add(polyLine);
            }
        }

        private static void ReadText(Estimation estimation, XElement node)
        {
            foreach (XElement textNode in node.NsElements("text"))
            {
                Text text = new Text();
                text.X = ReadAttribute(textNode, "x");
                text.Y = ReadAttribute(textNode, "y");
                text.TextData = textNode.Value;
                estimation.Geometries.Add(text);
            }
        }

        private static double ParseCoordinate(string value)
        {
            value = value.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            return Math.Round(double.Parse(value), 3);
        }
    }
}
