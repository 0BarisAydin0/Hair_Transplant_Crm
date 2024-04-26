using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using EntityLayer.Definition;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class PatientManager : IPatientService
    {

        Context context = new Context();
        private IPatientDAL _patientDAL;


        public PatientManager(IPatientDAL patientDAL)
        {

            _patientDAL = patientDAL;
        }

        public string CheckCreate(Patient patient)
        {
            using (var context = new Context())
            {
                try
                {
                    Patient p = context.Patients
                        .FirstOrDefault(
                        c => c.Mail.ToLower() == patient.Mail.ToLower() &&
                        c.Name.ToLower() == patient.Name.ToLower() &&
                        c.Surname.ToLower() == patient.Surname.ToLower() &&
                        c.PhoneNumber == patient.PhoneNumber
                        );



                    if (p == null)
                    {
                        patient.Mail.ToLower();
                        context.Add(patient);
                        context.SaveChanges();

                        return "success";

                    }
                    else
                    {
                        string patid = p.PatientID.ToString();
                        return patid;

                    }

                }
                catch (Exception)
                {

                    return "error";
                }

            }

        }

        public async Task<List<ChronicProblems>> ChronicProblemsSelect()
        {
            List<ChronicProblems> cps = await context.ChronicProblems.ToListAsync();
            return cps;
        }

        public async Task<List<Country>> CountrySelect()
        {
            List<Country> countries = await context.Countries.ToListAsync();
            return countries;
        }
        public async Task<List<InfectiousDisease>> InfectiousDiseaseSelect()
        {
            List<InfectiousDisease> ifd = await context.InfectiousDiseases.ToListAsync();
            return ifd;
        }
        public void Create(Patient entity)
        {
            _patientDAL.Create(entity);
        }

        public void Delete(Patient entity)
        {
            _patientDAL.Delete(entity);
        }

        public List<Patient> GetAll(Expression<Func<Patient, bool>> filter = null)
        {
            return _patientDAL.GetAll(filter).Where(x => x.IsActive == true).OrderByDescending(x => x.PatientID).ToList(); 
        }

        public Patient GetById(int id)
        {
            return _patientDAL.GetById(id);
        }

        public Patient GetOne(Expression<Func<Patient, bool>> filter)
        {
            return (_patientDAL.GetOne(filter));
        }



        public void Update(Patient entity)
        {
            _patientDAL.Update(entity);
        }

        public bool _Delete(int id)
        {
            try
            {
                var patient = _patientDAL.GetById(id);
                patient.IsActive = false;
                _patientDAL.Update(patient);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool _Update(Patient patient)
        {
            try
            {
                _patientDAL.Update(patient);
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

