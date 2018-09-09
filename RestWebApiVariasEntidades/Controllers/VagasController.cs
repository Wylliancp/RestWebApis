using FluentValidation;
using RestWebApiVariasEntidades.Filters;
using RestWebApiVariasEntidades.Models.Context;
using RestWebApiVariasEntidades.Models.Entities;
using RestWebApiVariasEntidades.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace RestWebApiVariasEntidades.Controllers
{
    public class VagasController : ApiController
    {
        private VagaContext db = new VagaContext();
        private VagaValidation validation = new VagaValidation();


        //Get api/vagas/
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select | AllowedQueryOptions.Skip | AllowedQueryOptions.Top,
            MaxTop = 10,
            PageSize = 10)]
        public IQueryable<Vaga> GetVagas()
        {
            return db.Vagas.Where(x => x.Ativo);
        }

        //Get api/vagas/5
        public IHttpActionResult GetVaga(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado deve ser maior que zero! ");

            var vaga = db.Vagas.Find(id);

            if (vaga == null)
                return NotFound();

            return Ok(vaga);
        }

        //Put api/vagas/3
        //[BasicAuhtentication]
        public IHttpActionResult PutVaga(int id, Vaga vaga)
        {
            if (id <= 0)
            {
                return BadRequest("o id informado deve ser maior que zero!");
            }

            if (id != vaga.Id)
                return BadRequest("o id informado no cabeçalho da requisição deve ser igual ao do corpo!");

            if (db.Vagas.Count(x => x.Id == id) == 0)
                return NotFound();

            validation.ValidateAndThrow(vaga);

            var idsRequisitosEditados = vaga.Requisitos.Where(x => x.Id > 0).Select(r => r.Id);

            var requisitosExcluidos = db.Requisito.Where(x => x.Vaga.Id == id && !idsRequisitosEditados.Contains(x.Id));

            db.Requisito.RemoveRange(requisitosExcluidos);

            foreach (var requisito in vaga.Requisitos)
            {
                if (requisito.Id > 0)
                {
                    db.Entry(requisito).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Entry(requisito).State = System.Data.Entity.EntityState.Added;
                }
            }
            db.Entry(vaga).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST api/vagas
        //[BasicAuhtentication]
        public IHttpActionResult PostVaga(Vaga vaga)
        {

            validation.ValidateAndThrow(vaga);

            vaga.Ativo = true;

            db.Vagas.Add(vaga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vaga.Id}, vaga);
        }

        //Delete api/vagas/4
        //[BasicAuhtentication]
        public IHttpActionResult DeleteVaga(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado deve ser maior que zero!");

            Vaga vaga = db.Vagas.Find(id);

            if (vaga == null)
                return NotFound();

            db.Vagas.Remove(vaga);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
