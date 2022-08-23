using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdministrator.DAL
{
    public static class GenericRepositoryValidation
    {
        public enum GenericRepositoryExceptionStatus
        {
            Success = 0,
            ArgumentNullException = 1,
            SqlException = 2
        }

    }
}
