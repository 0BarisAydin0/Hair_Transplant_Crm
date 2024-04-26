using DataAccessLayer.Concrate;
using EntityLayer;
using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPersonalService:IGenericService<Personal>
    {
         Task<List<Personal>> AsyncGetAll();

        string CheckCreate(Personal personal);

        Task<List<Scope>> ScopeSelect();

        bool _Delete(int id);

        bool _Update(Personal personal);
    }
}
