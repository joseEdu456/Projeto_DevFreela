using DevFreela.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreatedUserInputModel>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                    .WithMessage("Email inválido.");

            RuleFor(u => u.DtNascimento)
                .Must(d => d > DateTime.Now.AddMonths(-18))
                    .WithMessage("Usuário deve ser maior de 18 anos.");
        }
    }
}
