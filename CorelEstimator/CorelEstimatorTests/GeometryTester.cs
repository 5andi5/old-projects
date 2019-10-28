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
    public class GeometryTester : BaseTester
    {
        [TestMethod]
        public void ImageDescriptor_RecognizesEqualImages()
        {
            //Arrange
            Image image1 = new Image
            {
                X=55,
                Y=25,
                Width=15,
                Height=25
            };
            Image image2 = new Image
            {
                X = 55,
                Y = 25,
                Width = 15,
                Height = 25
            };

            //Act-Assert
            Assert.AreEqual(image1.Descriptor, image2.Descriptor);
        }

        [TestMethod]
        public void ImageDescriptor_DistinguishDifferentImages()
        {
            //Arrange
            Image image1 = new Image
            {
                X = 55,
                Y = 25,
                Width = 15,
                Height = 25
            };
            Image image2 = new Image
            {
                X = 15,
                Y = 7,
                Width = 10,
                Height = 4
            };

            //Act-Assert
            Assert.AreNotEqual(image1.Descriptor, image2.Descriptor);
        }

        [TestMethod]
        public void PathElementCount_ReturnsZerroWhenEmptyStringPassed()
        {
            //Arrange
            var path = new Path { PathData = "" };

            //Act
            int count = path.ElementCount;

            //Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void PathElementCount_ReturnsZerroWhenNullPassed()
        {
            //Arrange
            var path = new Path { PathData = null };

            //Act
            int count = path.ElementCount;

            //Assert
            Assert.AreEqual(0, count);
        }
    }
}
