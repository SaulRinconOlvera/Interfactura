using Interfactura.Repository.Alumno;
using Interfactura.ViewModel;
using Interfactura.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Interfactura.Web.UI.Controllers
{
    public class AlumnoController : Controller
    {
        private IAlumnoRepository _repository;
        public AlumnoController(IAlumnoRepository repository) 
        {
            _repository = repository;
        }

        // GET: Alumno
        public ActionResult Index()
        {
            AlumnoViewModel viewModel = new AlumnoViewModel();
            return View(viewModel);
        }

        // POST: Alumno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AlumnoViewModel viewModel)
        {
            try
            {
               _repository.Add(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                string a = ex.Message;
                return View();
            }
        }
    }
}