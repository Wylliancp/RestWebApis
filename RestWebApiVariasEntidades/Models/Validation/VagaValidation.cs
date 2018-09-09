using FluentValidation;
using RestWebApiVariasEntidades.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestWebApiVariasEntidades.Models.Validation
{
    public class VagaValidation : AbstractValidator<Vaga>
    {
        
        public VagaValidation()
        {

            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("O campo titulo deve ser preenchido.")
                .Length(5, 100)
                .WithMessage("O titulo da vaga deve ter entre {MinLength} e {MaxLenght} caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("O campo descrição deve ser preenchido.");

            RuleFor(x => x.Salario)
                .GreaterThan(0)
                .WithMessage("O salario deve ser maior que zero!");

            RuleFor(x => x.LocalTrabalho)
                .Length(10, 100)
                .WithMessage("O local de trabalho da vaga deve ter entre {MinLength} e {MaxLength} numeros");
                
        }
    }
}