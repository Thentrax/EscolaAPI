using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscolaAPI.Models;
using EscolaAPI.Context;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly EscolaContext _context;
        public MatriculaController(EscolaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(int alunoId, int turmaId)
        {
            Aluno aluno = new Aluno { Id = alunoId };
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            _context.Aluno.Add(aluno);
            _context.Aluno.Attach(aluno);

            Turma turma = new Turma { Id = turmaId };
            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }
            _context.Turma.Add(turma);
            _context.Turma.Attach(turma);

            turma.Alunos.Add(aluno);
            _context.SaveChanges();

            return Ok("Matrícula realizada com sucesso");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var matriculas = (
                from a in _context.Aluno
                join t in _context.Turma on a.Id equals t.Id
                select new
                {
                    Matricula = a.Id,
                    Aluno = a.Nome,
                    Turma = t.Nome
                }
            ).ToList();
            if (matriculas == null)
            {
                return NotFound("Nenhuma matrícula encontrada");
            }
            else{
                return Ok(matriculas);
            }
        }
    }
}