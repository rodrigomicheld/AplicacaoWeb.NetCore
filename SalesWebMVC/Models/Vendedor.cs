using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Display(Name = "Nome do Vendedor")]
        public string Nome { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNasciemnto { get; set; }
        [Display(Name = "Salário")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Salario { get; set; }
        public Departamento Departamento { get; set; }
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }
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
