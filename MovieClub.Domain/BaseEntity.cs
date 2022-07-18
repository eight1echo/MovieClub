namespace MovieClub.Domain;
public abstract class BaseEntity
{
    public int Id { get; set; }

    public string? CreatedBy { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public string? ModifiedBy { get;set; }
    public DateTime LastModified { get; set; }
}
