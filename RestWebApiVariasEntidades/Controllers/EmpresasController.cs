using FluentValidation;
using RestWebApiVariasEntidades.Filters;
using RestWebApiVariasEntidades.Models.Context;
using RestWebApiVariasEntidades.Models.Entities;
using RestWebApiVariasEntidades.Models.Validation;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace RestWebApiVariasEntidades.Controllers
{
    public class EmpresasController : ApiController
    {

        private VagaContext db = new VagaContext();
        private EmpresaValidation validation = new EmpresaValidation();


        //Get api/Empresas  
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.InlineCount, MaxTop = 10, PageSize = 10)]
        public IQueryable<Empresa> GetEmpresa()
        {
            return db.Empresas;
        }

        //Get api/empresas/5
        public IHttpActionResult GetEmpresa(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero!");

            var empresa = db.Empresas.Find(id);

            if (empresa == null)
                return NotFound();

            return Ok(empresa);
        }

        //Get api/empresas/{id}/vagas
        [Route("api/empresa/{id}/vagas")]
        public IHttpActionResult GetVagas(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero!");

            var empresa = db.Empresas.Find(id);

            if (empresa == null)
                return NotFound();

            return Ok(empresa.Vagas);
        }
        //[BasicAuhtentication]
        // Put api/empresas/4
        public IHttpActionResult PutEmpresa(int id, Empresa empresa)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero!");

            if (id == empresa.Id)
                return BadRequest("o id da URL deve ser igual ao do corpo da requisição.");

            if (db.Empresas.Count(e => e.Id == id) == 0)
                return NotFound();

            validation.ValidateAndThrow(empresa);

            db.Entry(empresa).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST api/empresas/
        //[BasicAuhtentication]
        public IHttpActionResult PostEmpresa(Empresa empresa)
        {
            validation.ValidateAndThrow(empresa);

            db.Empresas.Add(empresa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = empresa.Id }, empresa);
        }

        //DELETE api/empresas/id
        //[BasicAuhtentication]
        public IHttpActionResult DeleteEmpresa(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL da requisição deve ser maior que zero!");
            var empresa = db.Empresas.Find(id);

            if (empresa == null)
                return NotFound();

            if (empresa.Vagas.Count(x => x.Ativo) > 0)
                return Content(HttpStatusCode.Forbidden, "Essa empresa não pode ser excluida pois a vagas ativa relacionadas");

            db.Entry(empresa).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            //ou
            //db.Empresas.Remove(empresa);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
