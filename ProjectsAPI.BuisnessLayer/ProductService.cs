
using ProductsAPI.DataAccess;
using ProductsAPI.Logic.Contracts;
using HTMLParser;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace ProductsAPI.Logic
{
    public class ProductService : IProductService
    {
        private readonly IDbContext _dbContext;
        private readonly IParser _parser;

        public ProductService(IDbContext dbContext, IParser parser)
        {
            _dbContext = dbContext;
            _parser = parser;
        }

        public async Task<IEnumerable<string>> Add(ulong nomenclature)
        {
            var shortNomenclature = nomenclature.ToString().Substring(0, 4);
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var nomencaltures = await _parser.GetDataByNomenclature(shortNomenclature, token);
            await _dbContext.AddMany(nomencaltures, token);
            return nomencaltures;
        }
    }
}