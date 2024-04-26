using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class PatientOperationImgManager : IPatientOperationImgService
    {
        Context context = new Context();
        private IPatientOperationImgDAL _POImgdal;

        public PatientOperationImgManager(IPatientOperationImgDAL pOImgdal)
        {
            _POImgdal = pOImgdal;
        }

        public void Create(PatientOperationImg entity)
        {
           _POImgdal.Create(entity);
        }

        public void Delete(PatientOperationImg entity)
        {
            throw new NotImplementedException();
        }

        public List<PatientOperationImg> GetAll(Expression<Func<PatientOperationImg, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public PatientOperationImg GetById(int id)
        {
            return _POImgdal.GetById(id);
        }

        public PatientOperationImg GetOne(Expression<Func<PatientOperationImg, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(PatientOperationImg entity)
        {
            throw new NotImplementedException();
        }
    }
}
