using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Interfaces.Managers
{
    public interface IConfigurationManager
    {
        string TaskManagerConnectionString { get; }
    }
}
