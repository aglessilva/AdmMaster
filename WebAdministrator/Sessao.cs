using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdministrator.Models;

namespace WebAdministrator
{
    public static class Sessao
    {
        private static RepositorioEmpresa empresas = new RepositorioEmpresa();

        public static RepositorioEmpresa Empresas { get => empresas; set => empresas = value; }
    }
}
