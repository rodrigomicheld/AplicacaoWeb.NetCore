using System;


namespace SalesWebMVC.Services.Exceptions
{
    public class DbconcurrencyException :ApplicationException
    {
        public DbconcurrencyException(string mensagem): base(mensagem)
        {

        }
    }
}
