using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Logic.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<string>> Add(ulong nomenclature);
    }
}
