using RestWebApiVariasEntidades.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace RestWebApiVariasEntidades.Models.Validation
{
    public class EmpresaValidation : AbstractValidator<Empresa>
    {//a classe deve herdar do AbstractValidator e informa de qual tipo, e baixa o FluentValidation. 

        public EmpresaValidation()//o arquivo a ser configurado no construtor da classe
        {
            RuleFor(x => x.Nome)//sempre deverá iniciar por meio de um RuleFor()
                .NotEmpty()//não pode estar em branco
                .WithMessage("O nome da Empresa deverá ser informada!")//mensagem caso o campo esteja em branco
                .Length(10, 100)//tamanho maximo e minimo
                .WithMessage("O nome da empresa deve conter entre {MinLength} e {MaxLength} caracteres.");//msg
        }
    }
}