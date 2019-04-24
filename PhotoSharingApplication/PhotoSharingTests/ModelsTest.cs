using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSharingApplication.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using PhotoSharingTests.Doubles;
using PhotoSharingApplication.Models;
using System.ComponentModel.DataAnnotations;

[TestClass]
public class ModelsTest
    {
    [TestMethod]
    public void Test_Photo_Title()
    {
        const string expectedName = "TestPhoto";
        Photo photo = new Photo { PhotoID=1,Title="TestPhoto"};
        Assert.AreEqual(expectedName,photo.Title);
    }
    [TestMethod]
    public void Test_Photo_Title_Empty()
    {
        Photo photo = new Photo { PhotoID = 1, Title = "" };
        Assert.IsTrue(ValidateModel(photo).Count > 0);
    }
    [TestMethod]
    public void Test_Photo_PhotoFile()
    {
        Photo photo = new Photo { PhotoID = 1, Title = "TestPhoto",PhotoFile= new byte[1] };
        Assert.IsNotNull(photo.PhotoFile);
    }
    [TestMethod]
    public void Test_Photo_ImageMimeType()
    {
        const string expectedName = "image/jpeg";
        Photo photo = new Photo { PhotoID = 1, Title = "TestPhoto", ImageMimeType = "image/jpeg" };
        Assert.AreEqual(expectedName, photo.ImageMimeType);
    }
    [TestMethod]
    public void Test_Photo_CreatedDate()
    {
        DateTime expectedDate = DateTime.Today.AddDays(-14);
        Photo photo = new Photo { PhotoID = 1, Title = "TestPhoto", CreatedDate= DateTime.Today.AddDays(-14) };
        Assert.AreEqual(expectedDate, photo.CreatedDate);
    }
    [TestMethod]
    public void Test_Comment_Subject_Empty()
    {
        Photo photo = new Photo { PhotoID = 1, Title = "TestPhoto" };
        Comment comment = new Comment { CommentID=1,PhotoID=1,Photo=photo, Subject=""};
        Assert.IsTrue(ValidateModel(comment).Count > 0);
    }
    [TestMethod]
    public void Test_Comment_Subject()
    {
        Photo photo = new Photo { PhotoID = 1, Title = "TestPhoto" };
        Comment comment = new Comment { CommentID = 1, PhotoID = 1, Photo = photo, Subject = "Test Comment Cool" };
        Assert.IsTrue(ValidateModel(comment).Count == 0);
    }
    [TestMethod]
    public void Test_Comment_Subject_Body()
    {
        Photo photo = new Photo { PhotoID = 1, Title = "TestPhoto" };
        Comment comment = new Comment { CommentID = 1, PhotoID = 1, Photo = photo, Body = "long long long long long long long long long long long long long long long long long long long long long long long long" };
        Assert.IsNotNull(comment.Body);
    }
    private IList<ValidationResult> ValidateModel(object model)
{
    var validationResults = new List<ValidationResult>();
    var ctx = new ValidationContext(model, null, null);
    Validator.TryValidateObject(model, ctx, validationResults, true);
    return validationResults;
}
}

