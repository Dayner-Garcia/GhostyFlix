namespace Data.Common
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}