using Core.Models.Chats;
using Core.Models.Files;
using Core.Models.Questionnaires;
using Microsoft.AspNetCore.Identity;

namespace Core.Models.Identitiy;

public class User : IdentityUser
{
    public bool IsOnline { get; set; }

    public DateTime? LastOnline { get; set; }

    public int? ProfilePictureId { get; set; }

    public virtual LocalFile? ProfilePicture { get; set; }

    public virtual ICollection<Chat> UserChats { get; set; } = [];

    public virtual ICollection<LocalFile> LocalFiles { get; set; } = [];

    public virtual ICollection<Questionnaire> Questionnaires { get; set; } = [];

}