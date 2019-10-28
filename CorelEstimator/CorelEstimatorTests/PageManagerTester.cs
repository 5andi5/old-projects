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
    public class PageManagerTester : BaseTester
    {
        [TestMethod]
        public void MergePages_NoFilesAreMultiPage_ReturnsTheSameList()
        {
            //Arrange
            var estimations = new List<Estimation>{
                new Estimation{
                        FilePath=@"C:\File2.svg",
                        Geometries=new List<IGeometry>{
                             new Circle(),
                             new Rectangle()
                        }
                },
                new Estimation{
                        FilePath=@"C:\File1.svg",
                        Geometries=new List<IGeometry>{
                             new Ellipse()
                        }
                }
            };

            //Act
            List<Estimation> result = PageManager.MergePages(estimations);

            //Assert
            Assert.AreEqual(estimations.Count, result.Count);
            Assert.AreEqual(estimations[0].FilePath, result[0].FilePath);
            Assert.AreEqual(estimations[0].Geometries.Count, result[0].Geometries.Count);
            Assert.AreEqual(estimations[1].FilePath, result[1].FilePath);
            Assert.AreEqual(estimations[1].Geometries.Count, result[1].Geometries.Count);
        }

        [TestMethod]
        public void MergePages_FileWithTwoPagesPassed_MergesFileName()
        {
            //Arrange
            var estimations = new List<Estimation>{
                new Estimation{FilePath=@"C:\File-1.svg"},
                new Estimation{FilePath=@"C:\File-2.svg"}
            };

            //Act
            List<Estimation> result = PageManager.MergePages(estimations);

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result[0].PageCount);
            Assert.AreEqual(@"C:\File.svg", result[0].FilePath);
        }

        [TestMethod]
        public void MergePages_FileWithThreePagesPassed_MergesGeometries()
        {
            //Arrange
            var estimations = new List<Estimation>{
                new Estimation{
                        FilePath=@"C:\MultiPage-1.svg",
                        Geometries=new List<IGeometry>{
                             new Circle(),
                             new Rectangle()
                        }
                },
                new Estimation{
                        FilePath=@"C:\MultiPage-2.svg",
                        Geometries=new List<IGeometry>{
                             new Ellipse()
                        }
                },
                new Estimation{
                        FilePath=@"C:\MultiPage-3.svg",
                        Geometries=new List<IGeometry>{
                             new Text()
                        }
                }
            };

            //Act
            List<Estimation> result = PageManager.MergePages(estimations);

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].PageCount);
            Assert.AreEqual(4, result[0].Geometries.Count);
            Assert.IsTrue(result[0].Geometries.Where(x => x is Circle).Any());
            Assert.IsTrue(result[0].Geometries.Where(x => x is Rectangle).Any());
            Assert.IsTrue(result[0].Geometries.Where(x => x is Ellipse).Any());
            Assert.IsTrue(result[0].Geometries.Where(x => x is Text).Any());
        }


        [TestMethod]
        public void MergePages_MultiLineAndNonMultiLineFilesPassed_MergesMultiPageFile()
        {
            //Arrange
            var estimations = new List<Estimation>{
                new Estimation{FilePath=@"C:\SingleFile.svg"},
                new Estimation{FilePath=@"C:\MultiPage-1.svg"},
                new Estimation{FilePath=@"C:\MultiPage-2.svg"},
                new Estimation{FilePath=@"C:\MultiPage-3.svg"}
            };

            //Act
            List<Estimation> result = PageManager.MergePages(estimations);

            //Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].PageCount);
            Assert.AreEqual(3, result[1].PageCount);
        }
    }
}
