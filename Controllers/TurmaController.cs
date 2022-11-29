using Microsoft.AspNetCore.Mvc;
using EscolaAPI.Models;
using EscolaAPI.Context;

namespace EscolaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly EscolaContext _context;
        public TurmaController(EscolaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var turmas = _context.Turma.ToList();
            if (turmas == null)
            {
                return NotFound("Nenhuma turma encontrada");
            }
            else{
                return Ok(turmas);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Turma turma)
        {
            if (turma == null)
            {
                return BadRequest("Turma inválida");
            }
            else{
                _context.Turma.Add(turma);
                _context.SaveChanges();
                return Ok("Turma cadastrada com sucesso");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Turma turma)
        {
            var turmaUpdate = _context.Turma.FirstOrDefault(t => t.Id == id);
            if (turmaUpdate == null)
            {
                return NotFound("Turma não encontrada");
            }
            else{
                turmaUpdate.AnoLetivo = turma.AnoLetivo;
                _context.SaveChanges();
                return Ok("Turma atualizada com sucesso");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var turmaDelete = _context.Turma.FirstOrDefault(t => t.Id == id);
            if (turmaDelete == null)
            {
                return NotFound("Turma não encontrada");
            }
            else{
                _context.Turma.Remove(turmaDelete);
                _context.SaveChanges();
                return Ok("Turma excluída com sucesso");
            }
        }
    }
}