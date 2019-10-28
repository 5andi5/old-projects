using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CorelEstimator.Managers;
using CorelEstimator.Entities;
using CorelEstimator.Geometries;

namespace CorelEstimatorTests
{
    [TestClass]
    public class InfoReadingManagerTester : BaseTester
    {
        [TestMethod]
        public void ReadSvcInfo_ReadsLine()
        {
            //Arrange
            string fileName = "Lines.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Lines);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Geometries.Count);
            Assert.AreEqual(GeometryType.Line, estimation.Geometries[0].Type);
            Line line = estimation.Geometries[0] as Line;
            Assert.AreEqual(1.0, line.StartX);
            Assert.AreEqual(9.0, line.StartY);
            Assert.AreEqual(3.0, line.EndX);
            Assert.AreEqual(9.0, line.EndY);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsRectangle()
        {
            //Arrange
            string fileName = "Rectangles.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Rectangles);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Geometries.Count);
            Assert.AreEqual(GeometryType.Rectangle, estimation.Geometries[0].Type);
            Rectangle rect = estimation.Geometries[0] as Rectangle;
            Assert.AreEqual(1.0, rect.StartX);
            Assert.AreEqual(6.0, rect.StartY);
            Assert.AreEqual(3.0, rect.Width);
            Assert.AreEqual(4.0, rect.Height);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsCircle()
        {
            //Arrange
            string fileName = "Circles.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Circles);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Geometries.Count);
            Assert.AreEqual(GeometryType.Circle, estimation.Geometries[0].Type);
            Circle circle = estimation.Geometries[0] as Circle;
            Assert.AreEqual(1.0, circle.Radius);
            Assert.AreEqual(1.723, circle.X);
            Assert.AreEqual(3.211, circle.Y);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsEllipses()
        {
            //Arrange
            string fileName = "Ellipses.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Ellipeses);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Geometries.Count);
            Assert.AreEqual(GeometryType.Ellipse, estimation.Geometries[0].Type);
            Ellipse ellipse = estimation.Geometries[0] as Ellipse;
            Assert.AreEqual(1.723, ellipse.X);
            Assert.AreEqual(3.211, ellipse.Y);
            Assert.AreEqual(0.5, ellipse.XRadius);
            Assert.AreEqual(1, ellipse.YRadius);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsImages()
        {
            //Arrange
            string fileName = "Images.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Images);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Geometries.Count);
            Assert.AreEqual(GeometryType.Image, estimation.Geometries[0].Type);
            Image image = estimation.Geometries[0] as Image;
            Assert.AreEqual(4.331, image.X);
            Assert.AreEqual(-6.168, image.Y);
            Assert.AreEqual(5.819, image.Width);
            Assert.AreEqual(8.333, image.Height);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsPaths()
        {
            //Arrange
            string fileName = "Paths.svg";
            string filePath = SaveToDisk(fileName, Resources.Resources.Paths);
            var estimation = new Estimation(filePath);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Geometries.Count);
            Assert.AreEqual(GeometryType.Path, estimation.Geometries[0].Type);
            Path path = estimation.Geometries[0] as Path;
            Assert.AreEqual(
                "M2.88504 3.8532l0 2.42694c0.784988,0.123114 0.637024,-0.932217 1.32378,-0.735433l1.47087 1.54441",
                path.PathData);
            Assert.AreEqual(4, path.ElementCount);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsPolygons()
        {
            //Arrange
            string fileName = "Polygons.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Polygons);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Geometries.Count);
            Assert.AreEqual(GeometryType.Polygon, estimation.Geometries[0].Type);
            Polygon polygon = estimation.Geometries[0] as Polygon;
            Assert.AreEqual(
                "2.55138,5.85076 3.30138,6.04174 4.05138,6.23272 3.76491,6.54174 3.47843,6.85076 2.55138,6.85076 1.62433,6.85076 1.33785,6.54174 1.05138,6.23272 1.80139,6.04174 ",
                polygon.PointData);
            Assert.AreEqual(5, polygon.PointCount);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsPolyLines()
        {
            //Arrange
            string fileName = "Polylines.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Polylines);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Geometries.Count);
            Assert.AreEqual(GeometryType.PolyLine, estimation.Geometries[0].Type);
            PolyLine polyLine = estimation.Geometries[0] as PolyLine;
            Assert.AreEqual("0.999992,6.26772 1.99999,6.26772 1.99999,4.26772 ", polyLine.PointData);
            Assert.AreEqual(3, polyLine.PointCount);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsText()
        {
            //Arrange
            string fileName = "Texts.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Texts);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(2, estimation.Geometries.Count);

            Assert.AreEqual(GeometryType.Text, estimation.Geometries[0].Type);
            Text text = estimation.Geometries[0] as Text;
            Assert.AreEqual("What for", text.TextData);
            Assert.AreEqual(0.635, text.X);
            Assert.AreEqual(2.748, text.Y);

            Assert.AreEqual(GeometryType.Text, estimation.Geometries[1].Type);
            text = estimation.Geometries[1] as Text;
            Assert.AreEqual(" are we living?", text.TextData);
            Assert.AreEqual(0.635, text.X);
            Assert.AreEqual(3.120, text.Y);
        }

        [TestMethod]
        public void ReadSvcInfo_ReadsMultipleElements()
        {
            //Arrange
            string fileName = "Complex.svg";
            string path = SaveToDisk(fileName, Resources.Resources.Complex);
            var estimation = new Estimation(path);

            //Act
            InfoReadingManager.ReadSvcInfo(estimation);

            //Assert
            Assert.AreEqual(19, estimation.Geometries.Count);
            
        }
    }
}
