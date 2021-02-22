using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Insurance.Container
{
    public class InsuranceContainer
    {
        private static UnityContainer container;

        public static UnityContainer Instance
        {
            get => container = container ?? new UnityContainer();
        }

        private InsuranceContainer()
        {
        }
    }
}
