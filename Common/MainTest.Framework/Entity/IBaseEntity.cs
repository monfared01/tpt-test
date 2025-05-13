namespace MainTest.Framework.Entity;

public interface IBaseEntity :  IEntity
{
    Guid Id { get; set; }
    DateTime CreatedOn { get; set; }
    Guid? CreatedBy { get; set; }
    DateTime? LastModifiedOn { get; set; }
    Guid? LastModifiedBy { get; set; }
}
