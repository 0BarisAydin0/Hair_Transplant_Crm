﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Repository;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfOfferDAL : GenericRepository<Offer, Context>, IOfferDAL
    {
        public EfOfferDAL(Context context) : base(context)
        {
        }
    }
}
