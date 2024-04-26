using EntityLayer;
using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class OfferDTO
    {
        public Patient Patient { get; set; }
        public Offer Offer { get; set; }

        public List<Patient> patients { get; set; }
        public List<Offer> Offers { get; set; }
        public List<Currency> currencies { get; set; }
        public List<Technique> Techniques { get; set; }
    }
}
