using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MiLibNetCore.Threading
{
    public interface ICancellationTokenProvider
    {
        CancellationToken Token { get; }
    }
}
