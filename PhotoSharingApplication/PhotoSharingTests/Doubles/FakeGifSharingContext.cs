using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoSharingApplication.Models;

namespace PhotoSharingTests.Doubles
{
    class FakeGifSharingContext : IGifSharingContext
    {
        SetMap _map = new SetMap();

        public IQueryable<Gif> Gifs
        {
            get { return _map.Get<Gif>().AsQueryable(); }
            set { _map.Use<Gif>(value); }
        }

        public IQueryable<Comment> Comments
        {
            get { return _map.Get<Comment>().AsQueryable(); }
            set { _map.Use<Comment>(value); }
        }

        public bool ChangesSaved { get; set; }

        public int SaveChanges()
        {
            ChangesSaved = true;
            return 0;
        }

        public T Add<T>(T entity) where T : class
        {
            _map.Get<T>().Add(entity);
            return entity;
        }

        public Gif FindGifById(int ID)
        {
            Gif item = (from g in this.Gifs
                          where g.GifID == ID
                          select g).FirstOrDefault();

            return item;
        }

        public Gif FindGifByTitle(string Title)
        {
            Gif item = (from g in this.Gifs
                        where g.Title == Title
                          select g).FirstOrDefault();

            return item;
        }


        public Comment FindCommentById(int ID)
        {
            Comment item = (from c in this.Comments
                            where c.CommentID == ID
                            select c).First();
            return item;
        }


        public T Delete<T>(T entity) where T : class
        {
            _map.Get<T>().Remove(entity);
            return entity;
        }
        class SetMap : KeyedCollection<Type, object>
        {

            public HashSet<T> Use<T>(IEnumerable<T> sourceData)
            {
                var set = new HashSet<T>(sourceData);
                if (Contains(typeof(T)))
                {
                    Remove(typeof(T));
                }
                Add(set);
                return set;
            }

            public HashSet<T> Get<T>()
            {
                return (HashSet<T>)this[typeof(T)];
            }

            protected override Type GetKeyForItem(object item)
            {
                return item.GetType().GetGenericArguments().Single();
            }
        }
    }
}
