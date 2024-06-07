using DataAccessLayer.Abstract;
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
    public class EfPersonalDAL : GenericRepository<Personal, Context>, IPersonalDAL
    {
        private readonly Context _context;

        public EfPersonalDAL(Context context) : base(context)
        {
            _context = context;
        }

        public string PersonalInsert(Personal personal)
        {
            try
            {
                Personal p = _context.Personals.FirstOrDefault(c => c.Mail.ToLower() == personal.Mail.ToLower());

                if (p == null)
                {
                    personal.Mail = personal.Mail.ToLower();
                    _context.Add(personal);
                    _context.SaveChanges();
                    return "Başarılı.";
                }
                else
                {
                    return "Zaten var.";
                }
            }
            catch (Exception)
            {
                return "Başarısız.";
            }
        }
    }
}
