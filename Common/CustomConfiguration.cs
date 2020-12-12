using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CustomConfiguration : ICustomConfiguration
    {
        public CustomConfiguration()
        {

        }
        public string DefaultConnectionString { get; set; }
        public string TokenAuthority { get; set; }
        public string ClientId { get; set; }
    }

    public interface ICustomConfiguration
    {
    }
}
