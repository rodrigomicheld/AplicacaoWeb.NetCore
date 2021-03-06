﻿using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class VendedorService
    {
        private readonly SalesWebMVCContext _context;

        public VendedorService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.ToList();
        }

        public void InserirVendedor(Vendedor vendedor)
        {
            _context.Add(vendedor);
            _context.SaveChanges();
        }

        public Vendedor FindbyId(int id)
        {
            return _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(obj => obj.Id == id);

        }

        public void DeletarVendedor(int id)
        {
            var obj = _context.Vendedor.Find(id);
            _context.Vendedor.Remove(obj);
            _context.SaveChanges();
        }

        public void AtualizarVendedor(Vendedor vendedor)
        {
            if(!_context.Vendedor.Any(x => x.Id == vendedor.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(vendedor);
                _context.SaveChanges();

            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
    }
}
