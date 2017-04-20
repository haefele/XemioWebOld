using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Xemio.Api.Mapping
{
    public interface IMapper<TIn, TOut>
    {
        Task<TOut> MapAsync(TIn input, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<TOut>> MapListAsync(IList<TIn> input, CancellationToken cancellationToken = default(CancellationToken));
    }
}