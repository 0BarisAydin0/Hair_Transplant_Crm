using EntityLayer;
using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPatientService: IGenericService<Patient>
    {
        string CheckCreate(Patient patient );
        bool _Delete(int id);

        bool _Update(Patient patient);

        Task<List<ChronicProblems>> ChronicProblemsSelect();
        Task<List<InfectiousDisease>> InfectiousDiseaseSelect();
        Task<List<Country>> CountrySelect();


    }
}
