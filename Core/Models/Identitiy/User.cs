using Core.Models.Chats;
using Core.Models.Files;
using Core.Models.Questionnaires;
using Microsoft.AspNetCore.Identity;

namespace Core.Models.Identitiy;

public class User : IdentityUser
{
    public int ProfilePictureId { get; set; }

    public virtual ICollection<Chat> UserChats { get; set; } = null!;

    public virtual ICollection<LocalFile> LocalFiles { get; set; } = null!;

    public virtual ICollection<Questionnaire> Questionnaires { get; set; } = null!;
}