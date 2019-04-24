using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharingApplication.Models
{
    public class GifSharingContext : DbContext, IGifSharingContext
    {
        public DbSet<Gif> Gifs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        IQueryable<Gif> IGifSharingContext.Gifs
        {
            get { return Gifs; }
        }

        IQueryable<Comment> IGifSharingContext.Comments
        {
            get { return Comments; }
        }

        int IGifSharingContext.SaveChanges()
        {
            return SaveChanges();
        }

        T IGifSharingContext.Add<T>(T entity)
        {
            return Set<T>().Add(entity);
        }

        Gif IGifSharingContext.FindGifById(int ID)
        {
            return Set<Gif>().Find(ID);
        }

        Gif IGifSharingContext.FindGifByTitle(string Title)
        {
            Gif gif = (from g in Set<Gif>()
                           where g.Title == Title
                           select g).FirstOrDefault();
            return gif;
        }

        Comment IGifSharingContext.FindCommentById(int ID)
        {
            return Set<Comment>().Find(ID);
        }


        T IGifSharingContext.Delete<T>(T entity)
        {
            return Set<T>().Remove(entity);
        }
    }
}