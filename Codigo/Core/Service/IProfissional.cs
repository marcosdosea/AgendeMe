using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProfissional
    {
        int Create(Cidadao profissional);
        void Edit(Cidadao profissional);
        void Delete(int idProfissional);
        Cidadao Get(int idProfissional);
        IEnumerable<Cidadao> GetAll();
    }
}
