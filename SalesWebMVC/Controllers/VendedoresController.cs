using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class VendedoresController:Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }
        public IActionResult Novo()
        {
            var departamentos = _departamentoService.FindAll();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }
        [HttpPost] //dizer que o metodo é um post
        [ValidateAntiForgeryToken] //contra ameaças
        public IActionResult Novo(Vendedor vendedor)
        {
            _vendedorService.InserirVendedor(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Apagar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _vendedorService.FindbyId(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost] //dizer que o metodo é um post
        [ValidateAntiForgeryToken] //contra ameaças
        public IActionResult Apagar (int id)
        {
            _vendedorService.DeletarVendedor(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
