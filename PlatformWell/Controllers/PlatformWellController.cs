using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using CommonLayer.DTO;
using CommonLayer.DTO.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace PlatformWell.Controllers
{
    public class PlatformWellController : Controller
    {
        private Authentication authentication;
        private Bearer bearer;
        PlatformWellUtilityBL platformWellUtilityBL;
        public PlatformWellController()
        {
            platformWellUtilityBL = new PlatformWellUtilityBL();
            bearer = new Bearer();
            Task<string> result = platformWellUtilityBL.GetBearerToken();
            bearer.token = result.Result;
        }

        public IActionResult Index()
        {
            var platformWellList = platformWellUtilityBL.GetPlatformWellList();
            return View(platformWellList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAPIDataAndLoad(PlatformWellActual platformWellActual)
        {
            if (!platformWellActual.Url.Equals(string.Empty))
            {
                string message = await platformWellUtilityBL.GetPlatformActualAndSaveorUpdate(platformWellActual,bearer);
                if (!message.Equals(string.Empty))
                {
                    ViewBag.Message = message;
                }
            }

            var platformWellList = platformWellUtilityBL.GetPlatformWellList();
            return View("Index", platformWellList);
        }
    }
}