using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Domain.Identity;
using ScientificJournal.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScientificJournal.Web.Controllers
{
    [Authorize]
    public class ConferenceController : Controller
    {
        private readonly UserManager<ScienceUser> userManager;
        private readonly IConferenceService conferenceService;

        public ConferenceController(UserManager<ScienceUser> userManager, IConferenceService conferenceService)
        {
            this.userManager = userManager;
            this.conferenceService = conferenceService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddConference()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ScienceUser currentUser = userManager.FindByIdAsync(userId.ToString()).Result;
            if (currentUser.isAdmin)
            {
                return View();
            }
            else return StatusCode(403);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddConference([Bind("ConferenceName,ConferenceImage,Date,Price")] Conference conference)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ScienceUser currentUser = userManager.FindByIdAsync(userId.ToString()).Result;
            if (currentUser.isAdmin)
            {
                if (ModelState.IsValid)
                {

                    conferenceService.AddConference(conference);
                    return RedirectToAction("ShowAllConferences"); 
                }
                return View(conference);
            }
            else return StatusCode(403);

            
        }

        public IActionResult ShowAllConferences()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ScienceUser currentUser = userManager.FindByIdAsync(userId.ToString()).Result;
            if (currentUser.isAdmin)
            {

                List<Conference> conferences = conferenceService.GetConferences();
                return View(conferences);
                
            }
            else return StatusCode(403);

        }

        [AllowAnonymous]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conference = conferenceService.GetDetailsForConference(id);
           
            if (conference == null)
            {
                return NotFound();
            }
            

            return View(conference);
        }

    }
}
