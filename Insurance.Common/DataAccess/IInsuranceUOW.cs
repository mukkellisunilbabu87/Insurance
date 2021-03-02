using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Common.DataAccess
{
    /// <summary>
    /// Interface for unit of work for insurance .
    /// </summary>
    public interface IInsuranceUOW
    {
        IInsuranceRepository RepositoryInstance { get; }
    }
}
