using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.DataAccess
{
    public interface IDbContext
    {
        Task<string> Add(string nomenclature, CancellationToken token);
        Task<string> AddMany(IEnumerable<string> nomenclatures, CancellationToken token);
    }
}
