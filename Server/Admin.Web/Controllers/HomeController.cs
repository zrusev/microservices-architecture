﻿namespace Admin.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Models;
    using StoreApi.Web.Infrastructure;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User.IsAdministrator())
            {
                return this.RedirectToAction(nameof(StatisticsController.Index), "Statistics");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            });
    }
}