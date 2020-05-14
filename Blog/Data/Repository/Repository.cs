using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddPost(Post post)
        {
            _appDbContext.Posts.Add(post);
         
        }

        public List<Post> GetAllPost()
        {
           return  _appDbContext.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            Post post = _appDbContext.Posts.FirstOrDefault(x => x.Id == id);
            return post;
        }

        public void RemovePost(int id)
        {
            _appDbContext.Posts.Remove(GetPost(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _appDbContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public void UpdatePost(Post post)
        {
            _appDbContext.Posts.Update(post);
        }
    }
}
