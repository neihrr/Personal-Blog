using BlogMVC.Data;
using BlogMVC.Data.Repository;
using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlogMVC.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private AppDBContext _ctx;
        public HomeController(IRepository repo, AppDBContext ctx)
        {
            _repo = repo;
            _ctx = ctx;
        }
        

        public IActionResult Index()
        {
            var contents = _repo.GetAllContent();
            return View(contents);
        }

       

        public IActionResult Post(int id)
        {
            var contents = _repo.GetAllContent().ToArray();

            if(contents == null || contents.Length == 0)
                return View();

            if (id == 0)
                id = contents[0].Id;

            var content = _repo.GetContent(id);
            return View(content);
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new Content());

            }

            else
            {
                var content = _repo.GetContent((int)id);
                return View(content);
            }
                
           
        }

        

        [HttpPost]
        public async Task<IActionResult> Edit(Content content)
        {
            content.Created = DateTime.Now;
            _repo.AddContent(content);
            if (await _repo.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            else
                return View(content);

        }

        [HttpGet]

        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemoveContent(id);
            await _repo.SaveChangesAsync();

            return RedirectToAction("index");

        }

       
        
        public IActionResult Search(string searchString)
        {


            Console.WriteLine(searchString);
           if(_ctx.Contents.Any(post => post.Title.IndexOf(searchString) != -1))
           {


                var results = _ctx.Contents.Where(content => content.Title.IndexOf(searchString) != -1).ToList();

                return View(results);
           }








            return View();
        }

        
        /*public IActionResult Edit(int id, ContentViewModel model)
        {
            var content = new Content { Id=id, Title = model.Title };
            
            return RedirectToAction("Index");
        }*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        
    }
}
