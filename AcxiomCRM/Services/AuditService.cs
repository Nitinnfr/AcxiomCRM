using AcxiomCRM.Data; using AcxiomCRM.Models; using Microsoft.AspNetCore.Identity; using System.Security.Claims;
namespace AcxiomCRM.Services;
public class AuditService : IAuditService {
 private readonly ApplicationDbContext _db; private readonly IHttpContextAccessor _http; private readonly UserManager<ApplicationUser> _users;
 public AuditService(ApplicationDbContext db,IHttpContextAccessor http,UserManager<ApplicationUser> users){_db=db;_http=http;_users=users;}
 public async Task WriteAsync(string action,string entityName,string? recordId=null,string? oldValue=null,string? newValue=null,string result="Success") { var ctx=_http.HttpContext; var id=ctx?.User.FindFirstValue(ClaimTypes.NameIdentifier); var user=id==null?null:await _users.FindByIdAsync(id); _db.AuditLogs.Add(new AuditLog{UserId=id,UserName=user?.FullName??ctx?.User.Identity?.Name??"System",Action=action,EntityName=entityName,RecordId=recordId,OldValue=oldValue,NewValue=newValue,IpAddress=ctx?.Connection.RemoteIpAddress?.ToString(),Result=result}); await _db.SaveChangesAsync(); }
}
