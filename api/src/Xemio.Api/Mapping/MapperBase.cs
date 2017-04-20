using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Xemio.Api.Mapping
{
    public abstract class MapperBase<TIn, TOut> : IMapper<TIn, TOut>
    {
        public abstract Task<TOut> MapAsync(TIn input, CancellationToken cancellationToken = default(CancellationToken));

        public virtual async Task<IList<TOut>> MapListAsync(IList<TIn> input, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = new List<TOut>();
            foreach (var i in input)
            {
                result.Add(await this.MapAsync(i, cancellationToken));
            }
            return result;
        }
    }
}