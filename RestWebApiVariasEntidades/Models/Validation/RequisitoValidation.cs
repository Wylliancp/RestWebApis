using FluentValidation;
using RestWebApiVariasEntidades.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestWebApiVariasEntidades.Models.Validation
{
    public class RequisitoValidation : AbstractValidator<Requisito>
    {

        public RequisitoValidation()
        {

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Preencha o campo Descricao!")
                .Length(10, 100)
                .WithMessage("O campo descricao deve ter entre {MinLength} e {MaxLength} caracteres!");

        }
    }
}