using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using EntityLayer;
using EntityLayer.Definition;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class PersonalManager : IPersonalService
    {

        private readonly Context _context;
        private IPersonalDAL _personalDAL;

        public PersonalManager(IPersonalDAL personalDAL)
        {
            _personalDAL = personalDAL;
        }



        public async Task<List<Personal>> AsyncGetAll()
        {
            return await _context.Personals.ToListAsync();
        }

        public string CheckCreate(Personal personal)
        {

                try
                {
                    Personal p = _context.Personals.FirstOrDefault(c => c.Mail.ToLower() == personal.Mail.ToLower());

                    if (p == null)
                    {
                        personal.Mail.ToLower();
                        _context.Add(personal);
                        _context.SaveChanges();
                        return "success";

                    }
                    else
                    {
                        return "dublicate";
                    }

                }
                catch (Exception)
                {

                    return "error";
                }

        }

        public void Create(Personal entity)
        {
            _personalDAL.Create(entity);
        }

        public void Delete(Personal entity)
        {
            _personalDAL.Delete(entity);
        }

        public List<Personal> GetAll(Expression<Func<Personal, bool>> filter = null)
        {
            return _personalDAL.GetAll(filter).Where(x => x.IsActive == true).OrderByDescending(x => x.PersonalID).ToList();
        }

        public Personal GetById(int id)
        {
            return _personalDAL.GetById(id);
        }

        public Personal GetOne(Expression<Func<Personal, bool>> filter)
        {
            return (_personalDAL.GetOne(filter));
        }

        public bool _Delete(int id)
        {
            try
            {
                var personal = _personalDAL.GetById(id);
                personal.IsActive = false;
                _personalDAL.Update(personal);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Scope>> ScopeSelect()
        {

            List<Scope> scopes = await _context.Scopes.ToListAsync();
            return scopes;
        }

        public void Update(Personal entity)
        {
            _personalDAL.Update(entity);
        }

        public bool _Update(Personal personal)
        {
            try
            {
                _personalDAL.Update(personal);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
