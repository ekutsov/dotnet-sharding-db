using System.Collections.Generic;

namespace Sharding.BusinessObjects.Settings
{
    public class ShardSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString => $"{nameof(this.Host)}={this.Host};" + 
                                          $"{nameof(this.Port)}={this.Port};" + 
                                          $"{nameof(this.Database)}={this.Database};" + 
                                          $"{nameof(this.Username)}={this.Username};" + 
                                          $"{nameof(this.Password)}={this.Password};";
    }
}