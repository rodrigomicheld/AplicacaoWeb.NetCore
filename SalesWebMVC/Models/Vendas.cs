using SalesWebMVC.Models.Enums;
using System;


namespace SalesWebMVC.Models
{
    public class Vendas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public StatusVendas Status { get; set; }
        public Vendedor Vendedor { get; set; }


        public Vendas() { }

        public Vendas(int id,DateTime data,double valor,StatusVendas status,Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Valor = valor;
            Status = status;
            Vendedor = vendedor;
        }
    }

}
