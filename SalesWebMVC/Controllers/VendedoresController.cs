using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Controllers
{
    public class VendedoresController:Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService,DepartamentoService departamentoService)
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
            if(id == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não foi fornecido" });
            }
            var obj = _vendedorService.FindbyId(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost] //dizer que o metodo é um post
        [ValidateAntiForgeryToken] //contra ameaças
        public IActionResult Apagar(int id)
        {
            _vendedorService.DeletarVendedor(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detalhes(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não fornecido" });
            }
            var obj = _vendedorService.FindbyId(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não encontrado" });
            }
            return View(obj);
        }
        public IActionResult Editar(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não fornecido" });
            }
            var obj = _vendedorService.FindbyId(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não encontrado" });
            }
            List<Departamento> departamentos = _departamentoService.FindAll();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj,Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost] //dizer que o metodo é um post
        [ValidateAntiForgeryToken] //contra ameaças
        public IActionResult Editar(int id,Vendedor vendedor)
        {
            if(id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id do vendedor inválido" });
            }
            try
            {
                _vendedorService.AtualizarVendedor(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error),new { mensagem = e.Message });
            }
            catch(DBConcurrencyException e)
            {
                return RedirectToAction(nameof(Error),new { mensagem = e.Message });
            }
        }

        public IActionResult Error(string mensagem)
        {
            var viewModel = new ErrorViewModel
            {
                Mensagem = mensagem,
                // Framework pega o id interno da requisição 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
