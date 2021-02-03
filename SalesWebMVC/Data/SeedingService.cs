using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;

        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Departamento.Any() || _context.RegistroVendas.Any() || _context.Vendedor.Any())
           {
                return;
           }
            Departamento d1 = new Departamento(1,"Computers");
            Departamento d2 = new Departamento(2,"Eletronics");
            Departamento d3 = new Departamento(3,"Fashion");
            Departamento d4 = new Departamento(4,"Books");

            Vendedor v1 = new Vendedor(1,"Rodrigo","rod@eu.com.br",new DateTime(1986,5,10),3500.00,d1);
            Vendedor v2 = new Vendedor(2,"Michel","mi@eu.com.br",new DateTime(1986,3,10),3500.00,d2);
            Vendedor v3 = new Vendedor(3,"Dias","di@eu.com.br",new DateTime(1986,12,10),3500.00,d1);
            Vendedor v4 = new Vendedor(4,"Meneses","me@eu.com.br",new DateTime(1986,11,10),3500.00,d4);
            Vendedor v5 = new Vendedor(5,"Didi","didi@eu.com.br",new DateTime(1986,5,24),3500.00,d2);
            Vendedor v6 = new Vendedor(6,"Eu","eu@eu.com.br",new DateTime(1986,1,31),3500.00,d3);

            Vendas r1 = new Vendas(1,new DateTime(2021,2,2),500.00,StatusVendas.Faturado,v1);
            Vendas r2 = new Vendas(2,new DateTime(2021,2,1),1000.00,StatusVendas.Pendnete,v1);
            Vendas r3 = new Vendas(3,new DateTime(2021,2,2),20000.00,StatusVendas.Cancelado,v2);
            Vendas r4 = new Vendas(4,new DateTime(2021,2,1),11000.00,StatusVendas.Faturado,v3);
            Vendas r5 = new Vendas(5,new DateTime(2021,2,2),1500.00,StatusVendas.Faturado,v4);
            Vendas r6 = new Vendas(6,new DateTime(2021,2,1),5000.00,StatusVendas.Faturado,v5);
            Vendas r7 = new Vendas(7,new DateTime(2021,2,2),700.00,StatusVendas.Faturado,v6);
            Vendas r8 = new Vendas(8,new DateTime(2021,2,1),8000.00,StatusVendas.Faturado,v1);

            _context.Departamento.AddRange(d1,d2,d3,d4);

            _context.Vendedor.AddRange(v1,v2,v3,v4,v5,v6);

            _context.RegistroVendas.AddRange(r1,r2,r3,r4,r5,r6,r7,r8);

            _context.SaveChanges();

        }

    }
}
