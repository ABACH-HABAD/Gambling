namespace DataBaseClasses.Entity;

public abstract class Entity
{
    public int Id { get; set; }

    public T Clone<T>() where T : Entity
    { 
        return (T)MemberwiseClone();
    }
}
