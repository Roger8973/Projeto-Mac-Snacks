﻿using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LanchesMac.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISnacksRepository _snackRepository;

        public HomeController(ISnacksRepository snackRepository)
        {
            _snackRepository = snackRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                FavoriteSnacks = _snackRepository.FavoriteSnacks,
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}