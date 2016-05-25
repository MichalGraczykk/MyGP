using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGraduationProject.Models.DatabaseEntities.Interfaces
    {
    public interface IDatabaseEntity
    {
        T GetInstance<T>()
             where T : class;
    }
}
