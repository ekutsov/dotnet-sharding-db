using System.Collections.Generic;
using Sharding.BusinessObjects.Settings;

namespace Sharding.Data.Context.Provider
{
    public interface IShardProvider
    {
        ShardSettings GetShard();
        List<ShardSettings> Shards { get; }
    }
}