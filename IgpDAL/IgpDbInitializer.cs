

namespace IgpDAL
{
    public static class IgpDbInitializer
    {
        public static void Initialize(IgpDbContext ctx)
        {
              ctx.Database.EnsureCreated();  
             if (ctx.Clients.Any())
             {
                
                 return;
             }
            // else  ctx.Database.EnsureCreated();

            // var Songs = new List<Song>()
            // {
            //        new Song(){Title="Hello",Language="Yoruba",ImageUrl="jkkj"},
            //        new Song() {Title="GREETINGS", Language="English",ImageUrl="nonsense"}
            // };
            // ctx.Songs.AddRange(Songs);
            // ctx.SaveChanges();
        }

    }
}