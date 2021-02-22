using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Common.DataAccess;

namespace Insurance.DataAccess
{
    /// <summary>
    /// Unit of work class
    /// </summary>
    public class InsuranceUOW : IInsuranceUOW
    {
        private readonly IInsuranceRepository repository;

        public InsuranceUOW(IInsuranceRepository repository)
        {
            this.repository = repository;
        }

        public IInsuranceRepository RepositoryInstance => this.repository;
    }
}
