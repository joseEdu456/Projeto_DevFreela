using DevFreela.Application.Commands.InsertProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                    .WithMessage("Titulo não pode ser vazio")
                .MaximumLength(50)
                    .WithMessage("Titulo do projeto deve ter no máximo 50 caracteres.");

            RuleFor(p => p.TotalCost)
                .GreaterThan(0)
                    .WithMessage("Custo do projeto deve ser maior que $0.");
                
        }
    }
}
