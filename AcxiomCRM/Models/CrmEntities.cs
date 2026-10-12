using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AcxiomCRM.Models;
public abstract class CrmEntity { public int Id { get; set; } public DateTime CreatedDate { get; set; } = DateTime.UtcNow; public string? CreatedBy { get; set; } }
public class Customer : CrmEntity {
 [Required, StringLength(30)] public string CustomerCode {get;set;} = ""; [Required, StringLength(120)] public string CustomerName {get;set;} = "";
 [Required, EmailAddress, StringLength(256)] public string Email {get;set;} = ""; [Required, Phone, StringLength(20)] public string Phone {get;set;} = "";
 [StringLength(160)] public string? CompanyName {get;set;} [StringLength(250)] public string? Address {get;set;} [StringLength(80)] public string? City {get;set;} [StringLength(80)] public string? State {get;set;} [StringLength(30)] public string Status {get;set;} = "Active"; public string? OwnerId {get;set;}
}
public class Lead : CrmEntity {
 [Required, StringLength(30)] public string LeadCode {get;set;} = ""; [Required, StringLength(120)] public string LeadName {get;set;} = ""; [EmailAddress] public string? Email {get;set;} [Phone] public string? Phone {get;set;} [StringLength(160)] public string? CompanyName {get;set;}
 [StringLength(80)] public string Source {get;set;} = "Website"; [StringLength(30)] public string Status {get;set;} = "New"; [StringLength(30)] public string Priority {get;set;} = "Normal"; [Range(0,1000000000)] public decimal ExpectedValue {get;set;} public string? AssignedTo {get;set;} public bool IsConverted {get;set;}
}
public class Opportunity : CrmEntity {
 [Required, StringLength(160)] public string OpportunityName {get;set;} = ""; public int? CustomerId {get;set;} public Customer? Customer {get;set;} public int? LeadId {get;set;} public Lead? Lead {get;set;}
 [Range(typeof(decimal), "0.01", "999999999999")] public decimal Amount {get;set;} [StringLength(30)] public string Stage {get;set;} = "Qualification"; [Range(0,100)] public int Probability {get;set;} [DataType(DataType.Date)] public DateTime ExpectedCloseDate {get;set;} = DateTime.Today.AddDays(30); [StringLength(30)] public string Status {get;set;} = "Open"; public string? AssignedTo {get;set;}
 [NotMapped] public decimal WeightedValue => Amount * Probability / 100m;
}
public class FollowUp : CrmEntity {
 public int? CustomerId {get;set;} public Customer? Customer {get;set;} public int? LeadId {get;set;} public Lead? Lead {get;set;} [Required, DataType(DataType.Date)] public DateTime FollowUpDate {get;set;} = DateTime.Today.AddDays(1); [Required, StringLength(40)] public string FollowUpType {get;set;} = "Call"; [Required, StringLength(160)] public string Subject {get;set;} = ""; [StringLength(1000)] public string? Remarks {get;set;} [StringLength(30)] public string Status {get;set;} = "Planned"; public string? AssignedTo {get;set;}
}
public class Activity : CrmEntity {
 [Required, StringLength(40)] public string ActivityType {get;set;} = "Call"; [Required, StringLength(160)] public string Subject {get;set;} = ""; [StringLength(1000)] public string? Description {get;set;} public DateTime ActivityDate {get;set;} = DateTime.Now; public int? CustomerId {get;set;} public Customer? Customer {get;set;} public int? LeadId {get;set;} public Lead? Lead {get;set;} public string? AssignedTo {get;set;} [StringLength(30)] public string Status {get;set;} = "Planned";
}
public class AuditLog { public long Id {get;set;} public string? UserId {get;set;} [StringLength(120)] public string UserName {get;set;} = "System"; [Required, StringLength(80)] public string Action {get;set;} = ""; [StringLength(80)] public string EntityName {get;set;} = ""; public string? RecordId {get;set;} public string? OldValue {get;set;} public string? NewValue {get;set;} public DateTime CreatedDate {get;set;} = DateTime.UtcNow; [StringLength(64)] public string? IpAddress {get;set;} public string Result {get;set;} = "Success"; }
