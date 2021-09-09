using BlogMVC.Data;
using BlogMVC.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController: Controller
    {

        private IRepository _repo;
        public PanelController(IRepository repo)
        {
            _repo = repo;
        }


        public IActionResult Index()
        {
            var contents = _repo.GetAllContent();
            return View(contents);
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





    }
}
