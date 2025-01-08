using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjAspNetMvcDocker.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é obrigatório.")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A unidade é obrigatória.")]
        public string Unidade { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal? Preco { get; set; } = 0;


        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }


        [ValidateNever]
        //Propriedade de navegação
        public Categoria Categoria { get; set; } 
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
