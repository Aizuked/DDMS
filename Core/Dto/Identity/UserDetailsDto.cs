using Core.Dto.Questionnaires;

namespace Core.Dto.Identity;

public class UserDetailsDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = string.Empty;

    public string? About { get; set; }

    public string? JobTitle { get; set; }

    public bool IsOnline { get; set; }

    public DateTime? LastOnline { get; set; }

    public int? ProfilePictureId { get; set; }

    public ICollection<QuestionnaireDetailsDto> Questionnaires { get; set; } = [];
}