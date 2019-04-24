using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSharingApplication.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using PhotoSharingTests.Doubles;
using PhotoSharingApplication.Models;
using Moq;

[TestClass]
public class PhotoControllerTestMock
    {
    [TestMethod]
    public void Test_PhotoGallery_Int_Parameter()
    {
        //Arrange
        var Photos = new[] {
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo()
            }.AsQueryable();
        var contextMock = new Mock<IPhotoSharingContext>();
        contextMock.Setup(x=>x.Photos).Returns(Photos);
        var controller = new PhotoController(contextMock.Object);
        //Act
        var result = controller._PhotoGallery(3) as PartialViewResult;
        //Assert
        var modelPhotos = (IEnumerable<Photo>)result.Model;
        Assert.AreEqual(3, modelPhotos.Count());
    }
    [TestMethod]
    public void Test_GetImage_Null()
    {
       var Photos = new[] {
                new Photo{ PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" }
            }.AsQueryable();
        var contextMock = new Mock<IPhotoSharingContext>();
        contextMock.Setup(x => x.FindPhotoById(5)).Returns(Photos.Where(x => x.PhotoID==5) as Photo);
        var controller = new PhotoController(contextMock.Object);
        var result = controller.GetImage(5);
        Assert.AreEqual(null, result);
    }
    [TestMethod]
    public void Test_PhotoGallery_Int_Parameter_Single()
    {
        //Arrange
        const string expectedName="Photo1";
        var Photos = new[] {
                new Photo{ PhotoID = 1, Title="Photo1" }
            }.AsQueryable();
        var contextMock = new Mock<IPhotoSharingContext>();
        contextMock.Setup(x => x.Photos).Returns(Photos);
        var controller = new PhotoController(contextMock.Object);
        //Act
        var result = controller._PhotoGallery(3) as PartialViewResult;
        //Assert
        var modelPhotos = (IEnumerable<Photo>)result.Model;
        var actualName = modelPhotos.First().Title;
        Assert.AreEqual(expectedName, actualName);
    }
    [TestMethod]
    public void Test_Display_Nonexistant_Parameter()
    {
        //Arrange
        var contextMock = new Mock<IPhotoSharingContext>();
        contextMock.Setup(x => x.FindPhotoById(It.IsAny<int>())).Returns((Photo) null);
        var controller = new PhotoController(contextMock.Object);
        //Act
        var result = controller.Display(5);
        //Assert
        Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
    }

}

