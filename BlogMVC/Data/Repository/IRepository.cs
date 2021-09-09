using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Data.Repository
{
    public interface IRepository
    {

        Content  GetContent(int id);
        List<Content> GetAllContent();
        void RemoveContent(int id);
        void UpdateContent(Content content);
        void AddContent(Content content);

        Task<bool> SaveChangesAsync();
    }
}
