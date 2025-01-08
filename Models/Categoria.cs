
namespace ProjAspNetMvcDocker.Models;

public class Categoria {

    public int Id { get; set; }
    public string Descricao { get; set; }

    //relacionamento 1 para n produtos
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}