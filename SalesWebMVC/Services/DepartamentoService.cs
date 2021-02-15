using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;


namespace SalesWebMVC.Services
{
    public class DepartamentoService
    {
        private readonly SalesWebMVCContext _context;

        public DepartamentoService(SalesWebMVCContext context)
        {
            _context = context;
        }
        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(x => x.Nome).ToList();
        }
    }
}
