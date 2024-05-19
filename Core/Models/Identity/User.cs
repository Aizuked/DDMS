using Core.Models.Chats;
using Core.Models.Files;
using Core.Models.Questionnaires;
using Microsoft.AspNetCore.Identity;

namespace Core.Models.Identity;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = string.Empty;

    public string? About { get; set; }

    public string? JobTitle { get; set; }

    public bool IsOnline { get; set; }

    public DateTime? LastOnline { get; set; }

    public int? ProfilePictureId { get; set; }

    public virtual ICollection<Chat> UserChats { get; set; } = [];

    public virtual ICollection<LocalFile> LocalFiles { get; set; } = [];

    public virtual ICollection<Questionnaire> Questionnaires { get; set; } = [];

    public virtual ICollection<QuestionnaireResult> QuestionnaireResults { get; set; } = [];
}