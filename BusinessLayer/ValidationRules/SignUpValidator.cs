using DataAccessLayer.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class SignUpValidator : AbstractValidator<RegisterViewModel>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Lütfen Şirket Adınızı Yazın");
           

        }
    }
}
