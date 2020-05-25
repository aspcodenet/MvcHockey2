using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc2Hockey.Models;
using Mvc2Hockey.Services;
using Mvc2Hockey.ViewModels;

namespace Mvc2Hockey.Controllers
{
    public class PlayerController : Controller
    {
        private readonly HockeyDbContext _dbContext;
        private readonly ITransferService _transferService;

        public PlayerController(HockeyDbContext dbContext, ITransferService transferService)
        {
            _dbContext = dbContext;
            _transferService = transferService;
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
            model.AllTeams = GetAllTeams();
            
            model.AllPositions = Enum.GetValues(typeof(Position)).Cast<Position>()
                .Select(v => new SelectListItem {Text = v.ToString(), Value = ((int) v).ToString()})
                .ToList();

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
                p.Position = (Position) viewModel.PositionEnumValue;
                p.Team = _dbContext.Team.FirstOrDefault(t => t.Id == viewModel.TeamId);
                _dbContext.Players.Add(p);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            viewModel.AllTeams = GetAllTeams();
            viewModel.AllPositions = Enum.GetValues(typeof(Position)).Cast<Position>()
                .Select(v => new SelectListItem { Text = v.ToString(), Value = ((int)v).ToString() })
                .ToList();
            return View("Edit", viewModel);
        }




        public IActionResult Transfer(int id)
        {
            var p = _dbContext.Players.Include(r => r.Team).FirstOrDefault(a => a.Id == id);
            var viewModel = new PlayerTransferViewModel
            {
                Id = p.Id, Name = p.Name, CurrentTeam = p.Team.Name,
                AllTeams = GetAllTeams()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Transfer(PlayerTransferViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var result = _transferService.Transfer(viewModel.Id, viewModel.TeamId);
                if (result == TransferErrorCode.SameTeamError)
                {
                    ModelState.AddModelError("TeamId", "Cant transfer to same team as you are already in");
                    return View(viewModel);
                }

                //Save
                //Redirect ...
            }
            return View(viewModel);
        }




        public IActionResult Edit(int id)
        {
            var p = _dbContext.Players.Include(r=>r.Team).FirstOrDefault(a => a.Id == id);
            var viewModel = new PlayerViewModel {
                DbNumber = id, Name = p.Name,
                TeamId = p.Team == null ? 0 : p.Team.Id,
                AllTeams = GetAllTeams(),
                PositionEnumValue = (int)p.Position,
                JerseyNumber = p.JerseyNumber, Age = p.Age };
            viewModel.AllPositions = Enum.GetValues(typeof(Position)).Cast<Position>()
                .Select(v => new SelectListItem { Text = v.ToString(), Value = ((int)v).ToString() })
                .ToList();
            return View(viewModel);
        }

        private List<SelectListItem> GetAllTeams()
        {
            var l = _dbContext.Team.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();

            l.Insert(0,new SelectListItem("Select a team",""));

            return l;
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
                p.Team = _dbContext.Team.First(r => r.Id == viewModel.TeamId);
                p.JerseyNumber = viewModel.JerseyNumber;
                p.Position = (Position)viewModel.PositionEnumValue;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            viewModel.AllTeams = GetAllTeams();
            viewModel.AllPositions = Enum.GetValues(typeof(Position)).Cast<Position>()
                .Select(v => new SelectListItem { Text = v.ToString(), Value = ((int)v).ToString() })
                .ToList();
            return View(viewModel);
        }


    }
}