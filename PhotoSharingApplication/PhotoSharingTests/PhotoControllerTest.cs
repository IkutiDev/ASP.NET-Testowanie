using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSharingApplication.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using PhotoSharingTests.Doubles;
using PhotoSharingApplication.Models;

[TestClass]
    public class PhotoControllerTest
    {
        FakePhotoSharingContext context;
        PhotoController controller;
        [TestInitialize]
        public void TestInitialize()
        {
            context = new FakePhotoSharingContext();
            controller = new PhotoController(context);
        }
    [TestMethod]
        public void Test_Empty_Controller()
        {
            controller = new PhotoController();
            Assert.AreEqual(typeof(PhotoSharingContext),controller.context.GetType());
        }

        [TestMethod]
        public void Test_Index_Return_View()
        {
            //This test checks that the PhotoController Index action returns the Index View
            
            
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void Test_Display_Int_Parameter()
        {
        context.Photos = new[] {
                new Photo{ PhotoID = 1, Title="Photo1" },
                new Photo{ PhotoID = 2, Title="Photo2" },
                new Photo{ PhotoID = 3, Title="Photo3" },
                new Photo{ PhotoID = 4, Title="Photo4" }
            }.AsQueryable();
        var result = controller.Display(2) as ViewResult;
        var resultPhoto = (Photo)result.Model;
        Assert.AreEqual("Photo2", resultPhoto.Title);
        }
    [TestMethod]
    public void Test_Display_Nonexistant_Parameter()
    {
        context.Photos = new[] {
                new Photo{ PhotoID = 1, Title="Photo1" },
                new Photo{ PhotoID = 2, Title="Photo2" },
                new Photo{ PhotoID = 3, Title="Photo3" },
                new Photo{ PhotoID = 4, Title="Photo4" }
            }.AsQueryable();
        var result = controller.Display(5);
        Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
    }
        [TestMethod]
        public void Test_PhotoGallery_Model_Type()
        {
            //This test checks that the PhotoController _PhotoGallery action passes a list of Photos to the view
            context.Photos = new[] {
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo()
            }.AsQueryable();
            var result = controller._PhotoGallery() as PartialViewResult;
            Assert.AreEqual(typeof(List<Photo>), result.Model.GetType());
        }

        [TestMethod]
        public void Test_GetImage_Return_Type()
        {
            //This test checks that the PhotoController GetImage action returns a FileResult
            context.Photos = new[] {
                new Photo{ PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" }
            }.AsQueryable();
            var result = controller.GetImage(1) as ActionResult;
            Assert.AreEqual(typeof(FileContentResult), result.GetType());
        }

        [TestMethod]
        public void Test_DisplayByTitle_Return_Photo()
        {
            //This test checks that the PhotoController DisplayByTitle action returns the right photo
            context.Photos = new[] {
                new Photo{ PhotoID = 1, Title="Photo1" },
                new Photo{ PhotoID = 2, Title="Photo2" },
                new Photo{ PhotoID = 3, Title="Photo3" },
                new Photo{ PhotoID = 4, Title="Photo4" }
            }.AsQueryable();
            var result = controller.DisplayByTitle("Photo2") as ViewResult;
            var resultPhoto = (Photo)result.Model;
            Assert.AreEqual(2, resultPhoto.PhotoID);
        }

        [TestMethod]
        public void Test_DisplayByTitle_Return_Null()
        {
            //This test checks that the PhotoController DisplayByTitle action returns
            //HttpNotFound when you request a title that doesn't exist
            context.Photos = new[] {
                new Photo{ PhotoID = 1, Title="Photo1" },
                new Photo{ PhotoID = 2, Title="Photo2" },
                new Photo{ PhotoID = 3, Title="Photo3" },
                new Photo{ PhotoID = 4, Title="Photo4" }
            }.AsQueryable();
            var result = controller.DisplayByTitle("NonExistentTitle");
            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [TestMethod]
        public void Test_PhotoGallery_No_Parameter()
        {
            //This test checks that, when you call the _PhotoGallery action with no
            //parameter, all the photos in the context are returned
            context.Photos = new[] {
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo()
            }.AsQueryable();
            var result = controller._PhotoGallery() as PartialViewResult;
            var modelPhotos = (IEnumerable<Photo>)result.Model;
            Assert.AreEqual(4, modelPhotos.Count());
        }

        [TestMethod]
        public void Test_PhotoGallery_Int_Parameter()
        {
            //This test checks that, when you call the _PhotoGallery action with no
            //parameter, all the photos in the context are returned
            context.Photos = new[] {
                new Photo(),
                new Photo(),
                new Photo(),
                new Photo()
            }.AsQueryable();
            var result = controller._PhotoGallery(3) as PartialViewResult;
            var modelPhotos = (IEnumerable<Photo>)result.Model;
            Assert.AreEqual(3, modelPhotos.Count());
        }
    [TestMethod]
    public void Test_DeleteConfirmed_Int_Parameter()
    {
        context.Photos = new[] {
                new Photo{ PhotoID = 1, Title="Photo1" },
                new Photo{ PhotoID = 2, Title="Photo2" },
                new Photo{ PhotoID = 3, Title="Photo3" },
                new Photo{ PhotoID = 4, Title="Photo4" }
            }.AsQueryable();
        controller.DeleteConfirmed(3);
        Assert.AreEqual(3,context.Photos.Count());
    }
    [TestMethod]
    public void Test_DeleteConfirtmed_Wrong_Parameter()
    {
        context.Photos = new[] {
                new Photo{ PhotoID = 1, Title="Photo1" },
                new Photo{ PhotoID = 2, Title="Photo2" },
                new Photo{ PhotoID = 3, Title="Photo3" },
                new Photo{ PhotoID = 4, Title="Photo4" }
            }.AsQueryable();
        controller.DeleteConfirmed(5);
        Assert.AreEqual(4, context.Photos.Count());
    }
    [TestMethod]
    public void Test_GetImage_Null()
    {
                    context.Photos = new[] {
                new Photo{ PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" }
            }.AsQueryable();
            var result = controller.GetImage(5);
            Assert.AreEqual(null, result);
    }
    [TestMethod]
    public void Test_SlideShow()
    {
        context.Photos = new[] {
                new Photo{ PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" }
            }.AsQueryable();
        var result = controller.SlideShow() as ViewResult;
        Assert.AreEqual("SlideShow",result.ViewName);
    }
    [TestMethod]
    public void Test_Create()
    {
        var result = controller.Create() as ViewResult;
        Assert.AreEqual("Create",result.ViewName);
    }
    [TestMethod]
    public void Test_Delete_Int_Parameter()
    {
        context.Photos = new[] {
                new Photo{ PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" }
            }.AsQueryable();
        var result = controller.Delete(1) as ViewResult;
        Assert.AreEqual("Delete", result.ViewName);
    }
    [TestMethod]
    public void Test_Delete_Wrong_Parameter()
    {
        context.Photos = new[] {
                new Photo{ PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" }
            }.AsQueryable();
        var result = controller.Delete(5);
        Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
    }
    [TestMethod]
    public void Test_Add_Favorite_Wrong_Parameter()
    {

    }
    /*[TestMethod]
    public void Test_Add_Favorite_Int_Parameter()
    {
        context.Photos = new[] {
                new Photo{ PhotoID = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" },
                new Photo{ PhotoID = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg" }
            }.AsQueryable();
        //controller.Session["Favorites"]=Ses
        var result = controller.AddFavorite(2);
        Assert.AreEqual("The picture has been added to your favorites", result.Content);
    }*/
    public void TestCleanup()
    {
        controller = null;
        context = null;
    }
}

