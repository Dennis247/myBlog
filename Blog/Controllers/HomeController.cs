using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View(_repository.GetAllPost());
        }

        [HttpGet]
        public IActionResult Post(int id)
        {
            Post post = _repository.GetPost(id);
            return View(post);
        }

        [HttpPost]
        public IActionResult Post(Post post)
        {
            _repository.AddPost(post);
            _repository.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());
            return View(_repository.GetPost(id.Value));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if(post .Id > 0)
                _repository.UpdatePost(post);
                else
            _repository.AddPost(post);
            if (await _repository.SaveChangesAsync())
                return RedirectToAction("Index");
            else
         
            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult>  Remove(int id)
        {
            _repository.RemovePost(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}