using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Services.BotTemplates.wts.DefaultProject.Host.Models;

namespace Microsoft.Services.BotTemplates.wts.DefaultProject.Host.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get users's inf from AAD.
                var email = User.Identity.Name ?? User.FindFirst("preferred_username").Value;
                var firstAndLast = GetUserFirstAndLastName(User.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value);
                
                ViewData[DataKeys.UserId] = email;
                ViewData[DataKeys.Fob] = "FOB HERE";//fob ?? vehicleInfo?.FobId;
                ViewData[DataKeys.Vin] = "VIN HERE"; //vin ?? vehicleInfo?.Vin;
                ViewData[DataKeys.FirstName] = string.IsNullOrEmpty(firstAndLast.FirstName) ? "Name claim not found" : firstAndLast.FirstName;
                ViewData[DataKeys.LastName] = string.IsNullOrEmpty(firstAndLast.FirstName) ? "Name claim not found" : firstAndLast.FirstName;
                ViewData[DataKeys.EmailAddress] = email;
                ViewData[DataKeys.WebChatSecret] = _configuration.GetSection("WebChat:Secret").Value;
                ViewData[DataKeys.SpeechRecognizerSecret] = _configuration.GetSection("BingSpeech:Secret").Value;
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        private (string FirstName, string LastName) GetUserFirstAndLastName(string nameClaim)
        {
            var retValue = (string.Empty, string.Empty);
            if (string.IsNullOrEmpty(nameClaim))
            {
                return retValue;
            }

            var nameParts = nameClaim.Split(' ');
            retValue.Item1 = nameParts[0];
            if (nameParts.Length > 1)
            {
                retValue.Item2 = nameParts[nameParts.Length - 1];
            }

            return retValue;
        }
    }
}