using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedDb
{
    public static void Seed(this ModelBuilder modelbuilder)
    {
      SeedRoles(modelbuilder);
     // SeedUsers(modelbuilder);
    }

    public static void SeedRoles(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<IdentityRole>().HasData(
            new IdentityRole{
                Id="Admin",
                Name="Admin",
                NormalizedName="admin".ToUpper()
            },
                new IdentityRole{
                Id="Client",
                Name="Client",
                NormalizedName="Client".ToUpper()
            },
                new IdentityRole{
                Id="ServiceProvider",
                Name="ServiceProvider",
                NormalizedName="ServiceProvider".ToUpper()
            }

        );
    }
 public static void SeedUsers(ModelBuilder modelbuilder)
   {
       var user1= new IdentityUser{ 
           Id="1",
           UserName="Admin",
           NormalizedUserName="Admin".ToUpper(),
           Email= "Akomspatrick@gmail.com",
           EmailConfirmed=true

       };
         modelbuilder.Entity<IdentityRole>().HasData(user1
         

        );
   }
}