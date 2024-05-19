namespace Core.Dto.Identity;

public class UserListDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = string.Empty;

    public bool IsOnline { get; set; }

    public DateTime? LastOnline { get; set; }

    public int? ProfilePictureId { get; set; }
}