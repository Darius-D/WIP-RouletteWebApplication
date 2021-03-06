﻿using GamblingGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace GamblingGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Game gm, int[] box, bool firstTwelve, bool secondTwelve, bool thirdTwelve, bool even, bool odd, bool high, bool red, bool black,bool low, double betAmount)
        {
            gm = new Game();
            foreach (var value in box)
            {
                gm.Box.Add(value);
            }
            gm.FirstTwelve = firstTwelve;
            gm.SecondTwelve = secondTwelve;
            gm.ThirdTwelve = thirdTwelve;
            gm.Even = even;
            gm.Odd = odd;
            gm.High = high;
            gm.Low = low;
            gm.Red = red;
            gm.Black = black;
            gm.BetAmount = betAmount;
            return View("ResultsView",gm);
        }

        
        
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
