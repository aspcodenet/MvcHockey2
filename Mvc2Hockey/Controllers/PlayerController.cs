using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Mvc2Hockey.Models;
using Mvc2Hockey.ViewModels;

namespace Mvc2Hockey.Controllers
{
    public class PlayerController : Controller
    {
        private readonly HockeyDbContext _dbContext;

        public PlayerController(HockeyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET
        public IActionResult Index()
        {
            var viewModel = _dbContext.Players.Select(p=>new PlayerListViewModel { Id = p.Id, Name = p.Name }).ToList();
            return View(viewModel);
        }


        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckJersey(int jerseyNumber)
        {
            if (_dbContext.Players.Any(p => p.JerseyNumber == jerseyNumber))
            {
                return Json("Träöjan finns redan");
            }

            return Json(true);

        }


        public IActionResult New()
        {
            var model = new PlayerViewModel();
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(PlayerViewModel viewModel)
        {
            //19720808-1231
            //720101-0101
            //if (viewModel.PersonNummer.Length == 10 || viewModel.PersonNummer.Length == 12) //7208021111
            //{
            //    //Checksiffera a
            //    ModelState.AddModelError("PersonNummer", "Fel personnumer");
            //    return View("Edit", viewModel);
            //}

            //if (_dbContext.Players.Any(p => p.JerseyNumber == viewModel.JerseyNumber))
            //{
            //    ModelState.AddModelError("JerseyNumber", "Jersey is taken");
            //    return View("Edit", viewModel);
            //}

            if (ModelState.IsValid)
            {
                var p = new Player();
                p.Name = viewModel.Name;
                p.Age = viewModel.Age;
                p.JerseyNumber = viewModel.JerseyNumber;
                _dbContext.Players.Add(p);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", viewModel);
        }




        public IActionResult Edit(int id)
        {
            var p = _dbContext.Players.FirstOrDefault(a => a.Id == id);
            var viewModel = new PlayerViewModel {
                DbNumber = id, Name = p.Name, 
                JerseyNumber = p.JerseyNumber, Age = p.Age };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlayerViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var p = _dbContext.Players.FirstOrDefault(r => r.Id == viewModel.DbNumber);
                p.Name = viewModel.Name;
                p.Age = viewModel.Age;
                p.JerseyNumber = viewModel.JerseyNumber;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }


    }
}