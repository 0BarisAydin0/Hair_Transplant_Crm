using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class OperationManager : IOperationService
    {
        private IOperationDAL _operationDAL;

        public OperationManager(IOperationDAL operationDAL)
        {
            _operationDAL = operationDAL;
        }

        public void Create(Operation entity)
        {
           _operationDAL.Create(entity);
        }

        public void Delete(Operation entity)
        {
           _operationDAL.Delete(entity);
        }

        public List<Operation> GetAll(Expression<Func<Operation, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Operation GetById(int id)
        {
          return _operationDAL.GetById(id);
        }

        public Operation GetOne(Expression<Func<Operation, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Operation entity)
        {
            _operationDAL.Update(entity);
        }
    }
}
