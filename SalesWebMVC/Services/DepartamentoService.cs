using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class DepartamentoService
    {
        private readonly SalesWebMVCContext _context;

        public DepartamentoService(SalesWebMVCContext context)
        {
            _context = context;
        }
        /*sincrona
        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(x => x.Nome).ToList();
        }*/
        //assicrona
        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
