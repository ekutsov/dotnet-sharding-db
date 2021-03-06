using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sharding.BusinessObjects.Settings;

namespace Sharding.Data.Context.Provider
{
    public class ShardProvider : IShardProvider
    {
        private ShardSettings _shard;
        private IConfiguration _configuration;
        private readonly IOptions<List<ShardSettings>> _shardsSettings;
        public List<ShardSettings> Shards { get => _shardsSettings.Value; }
        public ShardProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _shard = _shardsSettings.Value[0];
        }

        public ShardSettings GetShard() => _shard;
    }
}