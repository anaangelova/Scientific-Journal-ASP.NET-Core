using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Domain.DTO;
using ScientificJournal.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificJournal.Web.Controllers
{
    public class PaperController : Controller
    {

        private readonly IPaperService paperService;

        public PaperController(IPaperService paperService)
        {
            this.paperService = paperService;
        }

        public IActionResult Index()
        {
            //gi lista site papers
            List<Paper> allPapers = paperService.GetAllPapers();
            return View(allPapers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] PaperDTO paperDTO, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                
                paperService.CreateNewPaper(paperDTO,file);
                return RedirectToAction("Index");


            }
            return View(paperDTO);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = paperService.GetDetailsForPaper(id);
            if (paper == null)
            {
                return NotFound();
            }

            return View(paper);
        }

        public IActionResult GetPdfDocument(string name)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{name}";
            var stream = new FileStream(pathToUpload, FileMode.Open);
            return File(stream, "application/pdf", name);
        }
    }
}
