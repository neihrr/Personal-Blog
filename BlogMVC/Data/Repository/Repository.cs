using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Data.Repository
{
    public class Repository : IRepository
    {
        //private AppDBContext _ctx;

        private AppDBContext _ctx;

        private readonly ILogger<Repository> _logger;

        public Repository(ILogger<Repository> logger, AppDBContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public void AddContent(Content content)
        {
            

            _ctx.Contents.Add(content);
            
        }

        public List<Content> GetAllContent()
        {
            return _ctx.Contents.ToList();
        }

        public Content GetContent(int id)
        {
           return  _ctx.Contents.FirstOrDefault(c=> c.Id == id);
        }

        

        public void RemoveContent(int id)
        {
            _ctx.Contents.Remove(GetContent(id));
        }

        public void UpdateContent(Content content)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            //_ctx.SaveChanges();

            return false;
        }
    }
}
