using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNasciemnto { get; set; }
        public double Salario { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Vendas> RegistroVendas { get; set; } = new List<Vendas>();

        public Vendedor() { }

        public Vendedor(int id,string nome,string email,DateTime dataNasciemnto,double salario,Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNasciemnto = dataNasciemnto;
            Salario = salario;
            Departamento = departamento;
        }

        public void AddVenda(Vendas vendas)
        {
            RegistroVendas.Add(vendas);
        }

        public void RemoverVenda(Vendas vendas)
        {
            RegistroVendas.Remove(vendas);
        }

        public double ToatalVendas(DateTime inicio, DateTime final)
        {
            return RegistroVendas.Where(vendas => vendas.Data >= inicio && vendas.Data <= final).Sum(vendas => vendas.Valor);
        }
    }
}
