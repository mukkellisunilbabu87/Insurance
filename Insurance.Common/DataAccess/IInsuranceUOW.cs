using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Common.DataAccess
{
    public interface IInsuranceUOW
    {
        IInsuranceRepository RepositoryInstance { get; }
    }
}
