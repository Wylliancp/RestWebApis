using RestWebApiPaginacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestWebApiPaginacao.Controllers
{
    public class AulasController : ApiController
    {

        private CursoContext db = new CursoContext();

        public IHttpActionResult GetAulas(int idCurso)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();//404
            }
            var lista = curso.Aulas.OrderBy(a => a.Ordem).ToList();
            return Ok(lista);//200
        }

        public IHttpActionResult GetAula(int idCurso, int ordemAula)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();//404
            }

            var aula = curso.Aulas.FirstOrDefault(x => x.Ordem == ordemAula);

            if (aula == null)
            {
                return NotFound();//404
            }

            return Ok(aula);//200
        }

        public IHttpActionResult PutAula(int idCurso, int ordemAula, Aula aula)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();//404
            }

            var aulaAtual = curso.Aulas.FirstOrDefault(x => x.Ordem == ordemAula);

            if (aulaAtual == null)
            {
                return NotFound();//404
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);//400
            }

            if (aula.Ordem > ordemAula)
            {
                int ultimaAula = curso.Aulas.Max(a => a.Ordem);

                if (aula.Ordem > ultimaAula)
                {
                    aula.Ordem = ultimaAula;
                }

                curso.Aulas.Where(a => a.Ordem > ordemAula && a.Ordem <= aula.Ordem)
                                  .ToList()
                                  .ForEach(a => a.Ordem--);
            }
            else if (aula.Ordem < ordemAula)
            {
                curso.Aulas.Where(a => a.Ordem >= ordemAula && a.Ordem < aula.Ordem)
                                  .ToList()
                                  .ForEach(a => a.Ordem++);
            }

            aulaAtual.Titulo = aula.Titulo;
            aulaAtual.Ordem = aula.Ordem;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);//204 no content
        }

        public IHttpActionResult PostAula(int idCurso, Aula aula)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();//404
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);//400
            }

            if (curso.Aulas.Count != 0)
            {
                int proximaAula = curso.Aulas.Max(a => a.Ordem) + 1;

                if (aula.Ordem > proximaAula)
                {
                    aula.Ordem = proximaAula;
                }
                else if (aula.Ordem < proximaAula)
                {
                    curso.Aulas.Where(a => a.Ordem >= aula.Ordem)
                                     .ToList()
                                     .ForEach(a => a.Ordem++);
                }
            }
            else
            {
                aula.Ordem = 1;
            }
                aula.IdCurso = idCurso;

            db.Aulas.Add(aula);
            db.SaveChanges();

            return CreatedAtRoute("Aulas", new { idCurso = idCurso, ordemAula = aula.Ordem }, aula);//201 created 
        }

        public IHttpActionResult DeleteAula(int idcurso, int ordemAula)
        {
            var curso = db.Cursos.Find(idcurso);

            if (curso == null)
            {
                return NotFound();//404
            }

            var aula = curso.Aulas.FirstOrDefault(a => a.Ordem == ordemAula);

            if (aula == null)
            {
                return NotFound();//404
            }

            db.Entry(aula).State = System.Data.Entity.EntityState.Deleted;

            curso.Aulas.Where(a => a.Ordem > ordemAula)
                             .ToList()
                             .ForEach(a => a.Ordem--);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);//204 no content
        }
    }
}
