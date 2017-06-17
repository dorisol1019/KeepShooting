using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public interface IShareSNS
    {
        void Post(string text, System.IO.MemoryStream imageStream);
    }
}
