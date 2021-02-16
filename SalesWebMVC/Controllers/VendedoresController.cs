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

        public async Task<IActionResult> Index()
        {
            var list = await _vendedorService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Novo()
        {
            var departamentos = await _departamentoService.FindAllAsync();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }
        [HttpPost] //dizer que o metodo é um post
        [ValidateAntiForgeryToken] //contra ameaças
        public async Task<IActionResult> Novo(Vendedor vendedor)
        {
            if(!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor,Departamentos = departamentos };
                return View(viewModel);
            }
            await _vendedorService.InserirVendedorAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Apagar(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não foi fornecido" });
            }
            var obj = await _vendedorService.FindbyIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost] //dizer que o metodo é um post
        [ValidateAntiForgeryToken] //contra ameaças
        public async Task<IActionResult> Apagar(int id)
        {
            await _vendedorService.DeletarVendedorAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detalhes(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não fornecido" });
            }
            var obj = await _vendedorService.FindbyIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não encontrado" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Editar(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não fornecido" });
            }
            var obj = await _vendedorService.FindbyIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id não encontrado" });
            }
            List<Departamento> departamentos = await _departamentoService.FindAllAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj,Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost] //dizer que o metodo é um post
        [ValidateAntiForgeryToken] //contra ameaças
        public async Task<IActionResult> Editar(int id,Vendedor vendedor)
        {
            if(!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor,Departamentos = departamentos };
                return View(viewModel);
            }

            if(id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error),new { mensagem = "Id do vendedor inválido" });
            }
            try
            {
                await _vendedorService.AtualizarVendedorAsync(vendedor);
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
