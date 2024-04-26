using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonalService, PersonalManager>();
            services.AddScoped<IPersonalDAL, EfPersonalDAL>();

            services.AddScoped<IPatientService, PatientManager>();
            services.AddScoped<IPatientDAL, EfPatientDAL>();

            services.AddScoped<IOfferService, OfferManager>();
            services.AddScoped<IOfferDAL, EfOfferDAL>();

            services.AddScoped<IOperationService, OperationManager>();
            services.AddScoped<IOperationDAL, EfOperationDAL>();

            services.AddScoped<IPatientOperationImgService, PatientOperationImgManager>();
            services.AddScoped<IPatientOperationImgDAL, EfPatientOperationImgDAL>();

            return services;
        }
    }
}
