using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Infraestructure.Shared.Settings
{
    public class BlobStorageSettings
    {
        public const string SettingName = "BlobStorageSettings";

        public string ConnectionString { get; set; }
    }
}
