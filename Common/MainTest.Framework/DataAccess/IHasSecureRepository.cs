namespace MainTest.Framework.DataAccess
{    
    
    public interface IHasSecureRepository
    {
        public List<string> Roles { get; set; }
        public ICollection<PermittedEntity> GetPermitedEntities(ICollection<string> roles);
    }
}
