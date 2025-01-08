using Microsoft.EntityFrameworkCore;

namespace ProjAspNetMvcDocker.Models
{
    public static class DbInitializer
    {
        public static void adicionadaDados(AppDbContext context)
        {

            if (context.Categorias.Any())
            {
                return; // DB já contém dados
            }

            var categorias = new Categoria[]
           {
                new Categoria { Descricao = "Eletrodomésticos" },
                new Categoria { Descricao = "Eletronicos" },
                new Categoria { Descricao = "Mesa e banho" },
                new Categoria { Descricao = "Outros" }
           };

             context.Categorias.AddRange(categorias);
             context.SaveChanges();


            // if (context.Produtos.Any())
            // {
            //     return; // DB já contém dados
            // }

            // var produtos = new Produto[]
            // {
            //     new Produto {  Descricao = "Produto A", Unidade = "KG" },
            //     new Produto {  Descricao = "Produto B",  Unidade = "PC"  },
            //     new Produto {  Descricao = "Produto C",  Unidade = "unid"  }
            // };

            // context.Produtos.AddRange(produtos);
            // context.SaveChanges();
        }

        public static void IniciaMigracao(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
            }
        }

    }
}
