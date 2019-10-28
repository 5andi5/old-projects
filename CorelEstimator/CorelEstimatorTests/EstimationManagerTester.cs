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
    public class EstimationManagerTester : BaseTester
    {
        [TestMethod]
        public void Estimate_CountsCircles()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                     new Circle{Radius=5, X=1, Y=2},
                     new Rectangle(),
                     new Circle{Radius=5, X=1, Y=1},
                     new Text(),
                     new Circle{Radius=7, X=5, Y=5},
                     new Circle{Radius=9, X=3, Y=3},
                     new Circle{Radius=7, X=2, Y=2}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(5, estimation.Result.GetCount(GeometryType.Circle));
            Assert.AreEqual(3, estimation.Result.GetUniqueCount(GeometryType.Circle));
        }

        [TestMethod]
        public void Estimate_CountsEllipses()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                     new Ellipse{XRadius=1, YRadius=2, X=1, Y=1},//Equal
                     new Circle{Radius=5, X=1, Y=2},
                     new Ellipse{XRadius=5, YRadius=2, X=2, Y=2},
                     new Ellipse{XRadius=1, YRadius=2, X=3, Y=3},//Equal
                     new Circle{Radius=7, X=2, Y=2}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(3, estimation.Result.GetCount(GeometryType.Ellipse));
            Assert.AreEqual(2, estimation.Result.GetUniqueCount(GeometryType.Ellipse));
        }

        [TestMethod]
        public void Estimate_CountsImages()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Image{X=5},
                    new Ellipse{XRadius=1, YRadius=2, X=1, Y=1},
                     new Image{X=5}, //Not unique
                     new Image{X=7}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(3, estimation.Result.GetCount(GeometryType.Image));
            Assert.AreEqual(2, estimation.Result.GetUniqueCount(GeometryType.Image));
        }

        [TestMethod]
        public void Estimate_CountsLines()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Image{X=5},
                    new Line{StartX=1, StartY=1, EndX=2, EndY=2},
                    new Line{StartX=10, StartY=1, EndX=30, EndY=1},
                    new Line{StartX=2, StartY=2, EndX=3, EndY=3} //Not unique
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(3, estimation.Result.GetCount(GeometryType.Line));
            Assert.AreEqual(2, estimation.Result.GetUniqueCount(GeometryType.Line));
        }

        [TestMethod]
        public void Estimate_CountsPaths()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Path{PathData="M2.88504 3.8532l0 2.42694c0.784988,0.123114 0.637024,-0.932217 1.32378,-0.735433l1.47087 1.54441"}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Result.GetCount(GeometryType.Path));
            Assert.AreEqual(1, estimation.Result.GetUniqueCount(GeometryType.Path));
        }

        [TestMethod]
        public void Estimate_CountsPolygons()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Polygon{PointData="2.55138,5.85076 3.30138,6.04174 4.05138,6.23272 3.76491,6.54174 3.47843,6.85076 2.55138,6.85076 1.62433,6.85076 1.33785,6.54174 1.05138,6.23272 1.80139,6.04174 "}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Result.GetCount(GeometryType.Polygon));
            Assert.AreEqual(1, estimation.Result.GetUniqueCount(GeometryType.Polygon));
        }

        [TestMethod]
        public void Estimate_CountsPolyLines()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new PolyLine{ PointData="0.999992,6.26772 1.99999,6.26772 1.99999,4.26772 "}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Result.GetCount(GeometryType.PolyLine));
            Assert.AreEqual(1, estimation.Result.GetUniqueCount(GeometryType.PolyLine));
        }

        [TestMethod]
        public void Estimate_CountsRectangles()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Rectangle{StartX=1, StartY=1, Width=10, Height=5}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Result.GetCount(GeometryType.Rectangle));
            Assert.AreEqual(1, estimation.Result.GetUniqueCount(GeometryType.Rectangle));
        }

        [TestMethod]
        public void Estimate_CountsText()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Text{TextData="Vizītkarte"}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(1, estimation.Result.GetCount(GeometryType.Text));
            Assert.AreEqual(1, estimation.Result.GetUniqueCount(GeometryType.Text));
        }

        [TestMethod]
        public void Estimate_NoElements_SetsAllCountsToZerro()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>()
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            foreach (GeometryType geometryType in Enum.GetValues(typeof(GeometryType)))
            {
                Assert.AreEqual(0, estimation.Result.GetCount(geometryType));
                Assert.AreEqual(0, estimation.Result.GetUniqueCount(geometryType));
            }
        }

        [TestMethod]
        public void Estimate_CalculatesCharCount()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Text{TextData="Reiz sensenos laikos"},
                    new Text{TextData="dzīvoja karalis Koļa"},
                    new Text{TextData="!"}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert
            Assert.AreEqual(41, estimation.Result.CharCount);
            Assert.AreEqual(0, estimation.Result.ComplexObjectPointCount);
        }

        [TestMethod]
        public void Estimate_IncludesPathPointCountInComplexObjectPointCount()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Path{PathData="M2.88504 3.8532l0 2.42694c0.784988,0.123114 0.637024,-0.932217 1.32378,-0.735433l1.47087 1.54441"}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert            
            Assert.AreEqual(4, estimation.Result.ComplexObjectPointCount);
            Assert.AreEqual(0, estimation.Result.CharCount);
        }

        [TestMethod]
        public void Estimate_IncludesPolygonsPointCountInComplexObjectPointCount()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new Polygon{PointData ="2.5,5.5 3.0,6.0 4.5,6.5 4.0,6.0"}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert            
            Assert.AreEqual(2, estimation.Result.ComplexObjectPointCount);
            Assert.AreEqual(0, estimation.Result.CharCount);
        }

        [TestMethod]
        public void Estimate_IncludesPolyLinesPointCountInComplexObjectPointCount()
        {
            //Arrange
            var estimation = new Estimation
            {
                Geometries = new List<IGeometry>{
                    new PolyLine{PointData ="2,2 3,3 4,2"}
                }
            };

            //Act
            EstimationManager.Estimate(estimation);

            //Assert            
            Assert.AreEqual(3, estimation.Result.ComplexObjectPointCount);
            Assert.AreEqual(0, estimation.Result.CharCount);
        }
    }
}
