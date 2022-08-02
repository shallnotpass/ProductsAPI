using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLParser
{
    public interface IParser
    {
        public Task<IEnumerable<string>> GetDataByNomenclature(string nomenclature, CancellationToken token);
    }
}
