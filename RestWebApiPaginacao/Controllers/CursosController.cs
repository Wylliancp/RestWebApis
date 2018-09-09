using RestWebApiPaginacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestWebApiPaginacao.Controllers
{
    public class CursosController : ApiController
    {
        private CursoContext db = new CursoContext();

        public IHttpActionResult PostCurso(Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//400
                //bad request retorna um estado(ex: 100,200,300,400,500) com a validação do modelstate ex(as mensagens de validação)

            db.Cursos.Add(curso);
            db.SaveChanges();//retorno volta a url configurada no arquivo app star WebApiconfig, e o corpo do curso inclusive o id criado.
            return CreatedAtRoute("DefaultApi", new { id = curso.Id }, curso);//201 created
        }

        public IHttpActionResult GetCurso(int id)
        {
            if (id <= 0)
                return BadRequest("O id deve ser um numero maior que 0");

            var curso = db.Cursos.Find(id);
            if (curso == null)
                return NotFound();

            return Ok(curso);//200
        }

        public IHttpActionResult PutCurso(int id, Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//400

            if (id != curso.Id)
                return BadRequest("o id informado na URL e diferente do id informado no corpo da requisição");

            if (db.Cursos.Count(x => x.Id == id) == 0)
                return NotFound();//404

            db.Entry(curso).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);//204 no content
        }

        public IHttpActionResult DeleteCurso(int id)
        {
            if (id <= 0)
                return BadRequest("insira um id valido, acima do valor 0");//400 badrequest

            var curso = db.Cursos.Find(id);

            if (curso == null)
                return NotFound();//404

            db.Cursos.Remove(curso);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);//204
        }

        public IHttpActionResult GetCurso(int pagina = 1, int tamanhoPagina = 10 )//qual pagina e quanto registro vou querer da pagina
        {
            if (pagina <= 0 && tamanhoPagina <= 0)
                return BadRequest("A pagina e o tamanho da pagina deve ser maior que 0 ");

            if (tamanhoPagina >= 10)
                return BadRequest("O tamanho maximo da pagina permitido e de 10.");

            int totalPaginas = (int)Math.Ceiling((db.Cursos.Count() / Convert.ToDecimal(tamanhoPagina)));//Nesta linha estamos calculando o total de páginas.
                                                                                                         //Como se trata de um valor inteiro, foi necessário 
                                                                                                         //fazer as devidas conversões entre os tipos de dados
                                                                                                         //e usar a função Math.Ceiling para arredondar o resultado da divisão para cima;
            if (pagina > totalPaginas)
                return BadRequest("A pagina solicitada não existe!");

            System.Web.HttpContext.Current.Response.AddHeader("X-pagina-TotaldePagina", totalPaginas.ToString());
            
            
            //Este bloco enviara informações relevantes para quem consumira o nosso serviço( Facilita a vida do Froent-End) irá para o Healder! ao inves do Body
            if (pagina > 1)
                System.Web.HttpContext.Current.Response.AddHeader("X-pagina-paginaAnterior",
                                                            Url.Link("DefaultApi", new { pagina = pagina - 1, tamanhoPagina = tamanhoPagina }));

            if (pagina < totalPaginas)
                System.Web.HttpContext.Current.Response.AddHeader("X-pagina-paginaPosterior",
                                                             Url.Link("DefaultApi", new { pagina = pagina + 1, tamanhoPagina = tamanhoPagina }));

            //
            var curso = db.Cursos.OrderBy(x => x.DataPublicacao)
                                          .Skip(tamanhoPagina * (pagina - 1))
                                          .Take(tamanhoPagina);//Linha 3: A função Skip é utilizada para avançar um determinado número de itens da lista. Isso permitirá escolher apenas 
                                                               //um conjunto de registros definido pelo número de páginas. Já a função Take indica quantos itens serão recuperados da lista.
                                                               //Neste caso queremos retornar apenas a quantidade especificada no parâmetro tamanhoPagina.
            return Ok(curso);//OK 200
        }
    }
}
