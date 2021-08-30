using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Splunk
{
    public class EventsBag
    {
        internal ConcurrentBag<object> Bag { get; } = new ConcurrentBag<object>();

        public Task EnsureBagEmpty()
            => EnsureBagEmpty(new CancellationToken());

        public async Task EnsureBagEmpty(CancellationToken cancellationToken)
        {
            while(Bag.Count > 0 && !cancellationToken.IsCancellationRequested)
            {
                Debug.WriteLine($"Waiting for {Bag.Count} messages to be sent...");
                await Task.Delay(250);
            }
        }
    }
}
