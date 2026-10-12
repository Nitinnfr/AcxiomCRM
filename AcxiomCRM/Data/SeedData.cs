using AcxiomCRM.Models; using Microsoft.AspNetCore.Identity;
namespace AcxiomCRM.Data;
public static class SeedData {
 public static async Task InitializeAsync(IServiceProvider sp) {
  var roles=sp.GetRequiredService<RoleManager<IdentityRole>>(); var users=sp.GetRequiredService<UserManager<ApplicationUser>>();
  foreach(var role in new[]{"Admin","Manager","SalesExecutive"}) if(!await roles.RoleExistsAsync(role)) await roles.CreateAsync(new IdentityRole(role));
  if(Environment.GetEnvironmentVariable("ACXIOM_SEED_DEMO")!="1") return;
  foreach(var item in new[]{("admin@acxiom.local","CRM Admin","Admin"),("manager@acxiom.local","CRM Manager","Manager"),("sales@acxiom.local","Sales Executive","SalesExecutive")}) {
   var user=await users.FindByEmailAsync(item.Item1); if(user==null) { user=new ApplicationUser{UserName=item.Item1,Email=item.Item1,FullName=item.Item2,EmailConfirmed=true,IsActive=true}; var result=await users.CreateAsync(user,"ChangeMe123!"); if(result.Succeeded) await users.AddToRoleAsync(user,item.Item3); }
  }
 }
}
