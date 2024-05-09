using Core.Models.Chats;
using Core.Models.Facets;
using Core.Models.Files;
using Core.Models.Projects;
using Core.Models.Questionnaires;
using Core.Models.Themes;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class DdmsDbContext(DbContextOptions<DdmsDbContext> options) : DbContext(options)
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Facet> Facets { get; set; }
    public DbSet<FacetItem> FacetItems { get; set; }
    public DbSet<LocalFile> LocalFiles { get; set; }
    public DbSet<LocalFileGroup> LocalFileGroups { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<QuestionnaireResult> QuestionnaireResults { get; set; }
    public DbSet<KeyWord> KeyWords { get; set; }
    public DbSet<SuggestedTheme> SuggestedThemes { get; set; }
    public DbSet<Theme> Themes { get; set; }
}