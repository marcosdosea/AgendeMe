#nullable disable

namespace Core
{
    public partial class Cargo
    {
        public Cargo()
        {
            Profissionalcargos = new HashSet<Profissionalcargo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Profissionalcargo> Profissionalcargos { get; set; }
    }
}
