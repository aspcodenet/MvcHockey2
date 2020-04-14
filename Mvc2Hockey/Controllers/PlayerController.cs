using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mvc2Hockey.Models;
using Mvc2Hockey.ViewModels;

namespace Mvc2Hockey.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        // GET
        public IActionResult Index()
        {
            var viewModel = _playerRepository.GetAll().Select(p=>new PlayerListViewModel { Id = p.Id, Name = p.Name }).ToList();
            return View(viewModel);
        }


        // GET
        public IActionResult View(int id)
        {
            var p = _playerRepository.Get(id);
            var viewModel = new PlayerViewModel { Name =  p.Name, JerseyNumber = p.JerseyNumber};
            return View(viewModel);
        }

    }
}