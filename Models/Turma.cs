using System.ComponentModel.DataAnnotations;

namespace EscolaAPI.Models
{
    public class Turma
    {
        public Turma(){
            this.Alunos = new HashSet<Aluno>();
        }
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public int AnoLetivo { get; set; }
        public virtual ICollection<Aluno> Alunos { get; set; }
    }
}