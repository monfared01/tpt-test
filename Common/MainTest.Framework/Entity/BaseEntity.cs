using System.ComponentModel.DataAnnotations;

namespace MainTest.Framework.Entity;

public abstract class BaseEntity : IBaseEntity
{
    public virtual Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
}
