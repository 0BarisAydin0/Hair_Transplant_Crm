using EntityLayer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class PersonalAddValidation : AbstractValidator<Personal>
    {
        public PersonalAddValidation()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen Adınızı Yazın");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Lütfen Soyadınızı Yazın");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Cinsiyet Alanını Boş Bırakmayınız");
        }
    }
}


