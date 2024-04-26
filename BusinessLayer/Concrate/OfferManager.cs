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
    public class OfferManager : IOfferService
    {

        private IOfferDAL _offerdal;

        public OfferManager(IOfferDAL offerdal)
        {
            _offerdal = offerdal;
        }

        public void Create(Offer entity)
        {
            _offerdal.Create(entity);
        }

        public void Delete(Offer entity)
        {
            throw new NotImplementedException();
        }

        public List<Offer> GetAll(Expression<Func<Offer, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Offer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Offer GetOne(Expression<Func<Offer, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Offer entity)
        {
            throw new NotImplementedException();
        }
    }
}
