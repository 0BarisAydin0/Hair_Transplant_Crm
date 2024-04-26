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
        public string PersonalInsert(Personal personal)
        {
            using (var context = new Context())
            {
                try
                {
                    Personal p = context.Personals.FirstOrDefault(c => c.Mail.ToLower() == personal.Mail.ToLower());

                    if (p == null)
                    {
                        personal.Mail.ToLower();
                        context.Add(personal);
                        context.SaveChanges();
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
}
