using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSharingApplication.Models
{
    public interface IGifSharingContext
    {
        IQueryable<Gif> Gifs { get; }
        IQueryable<Comment> Comments { get; }
        int SaveChanges();
        T Add<T>(T entity) where T : class;
        Gif FindGifById(int ID);
        Gif FindGifByTitle(string Title);
        Comment FindCommentById(int ID);
        T Delete<T>(T entity) where T : class;
    }
}
