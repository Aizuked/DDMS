using System.Collections.Immutable;
using Core;
using Core.Models.Facets;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Miscellaneous;

public static class Migrator
{
    public static async void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        await using var context = scope.ServiceProvider.GetService<T>();

        if (context == null)
            return;

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
            await context.Database.MigrateAsync();

        switch (context)
        {
            case DdmsDbContext ctx:
                if (ctx.Facets.Any(i => i.Code == "project_status"))
                    return;

                await SeedMainContext(ctx);
                break;

            case IdentityContext ctx:
                if (ctx.Roles.Any(i => i.Name == ROLES_ADMIN))
                    return;

                await SeedIdentityRoles(ctx);
                break;
        }
    }

    private async static Task SeedMainContext(DdmsDbContext context)
    {
        var test =
            await
                context
                    .Facets
                    .AnyAsync(i => i.Code == "project_status");

        if (test)
            return;

        await SeedFacets(context);
    }

    private async static Task SeedFacets(DdmsDbContext context)
    {
        #region Facets
        var facets = new List<Facet>
        {
            new()
            {
                IsSystem =    true,
                Code =        "project_status",
                DisplayName = "Статус в котором находится проект.",
                Description = ""
            },
            new()
            {
                IsSystem =    true,
                Code =        "task_status",
                DisplayName = "Статус в котором находится задача по проекту.",
                Description = ""
            },
            new()
            {
                IsSystem =    true,
                Code =        "question_type",
                DisplayName = "Тип ответа для вопроса.",
                Description = ""
            },
            new()
            {
                IsSystem =    false,
                Code =        "questionnaire_type",
                DisplayName = "Тип анкеты.",
                Description = ""
            }
        };
        context.AddRange(facets);
        await context.SaveChangesAsync();

        var facetIds =
            facets
                .ToImmutableDictionary(k => k.Code, v => v.Id);
        #endregion

        #region project_status
        var facetItems_project_status = new List<FacetItem>
        {
            new()
            {
                Code =        "Discussion",
                DisplayName = "Обсуждение.",
                Description = "",
                FacetId =     facetIds["project_status"]
            },
            new()
            {
                Code =        "WorkInProhgressProgramme",
                DisplayName = "Программный продукт в разработке.",
                Description = "",
                FacetId =     facetIds["project_status"]
            },
            new()
            {
                Code =        "WorkInProgressThesis",
                DisplayName = "Сопутсвтующая записка в написании.",
                Description = "",
                FacetId =     facetIds["project_status"]
            },
            new()
            {
                Code =        "Finished",
                DisplayName = "Завершён.",
                Description = "",
                FacetId =     facetIds["project_status"]
            },
        };
        context.AddRange(facetItems_project_status);
        await context.SaveChangesAsync();
        #endregion

        #region task_status
        var facetItems_task_status = new List<FacetItem>
        {
            new()
            {
                Code =        "Analysis",
                DisplayName = "На анализе.",
                Description = "",
                FacetId =     facetIds["task_status"]
            },
            new()
            {
                Code =        "WorkInProgress",
                DisplayName = "В разработке.",
                Description = "",
                FacetId =     facetIds["task_status"]
            },
            new()
            {
                Code =        "Question",
                DisplayName = "Есть вопросы.",
                Description = "",
                FacetId =     facetIds["task_status"]
            },
            new()
            {
                Code =        "Review",
                DisplayName = "Ожидает отзыва.",
                Description = "",
                FacetId =     facetIds["task_status"]
            },
            new()
            {
                Code =        "Rework",
                DisplayName = "К доработке.",
                Description = "",
                FacetId =     facetIds["task_status"]
            },
            new()
            {
                Code =        "Finished",
                DisplayName = "Завершена.",
                Description = "",
                FacetId =     facetIds["task_status"]
            },
        };
        context.AddRange(facetItems_task_status);
        await context.SaveChangesAsync();
        #endregion

        #region question_type
        var facetItems_question_type = new List<FacetItem>
        {
            new()
            {
                Code =        "Text",
                DisplayName = "Текстовое поле.",
                Description = "",
                FacetId =     facetIds["question_type"]
            },
            new()
            {
                Code =        "Date",
                DisplayName = "Дата.",
                Description = "",
                FacetId =     facetIds["question_type"]
            },
            new()
            {
                Code =        "DateTime",
                DisplayName = "Дата и время.",
                Description = "",
                FacetId =     facetIds["question_type"]
            },
            new()
            {
                Code =        "TimeSpan",
                DisplayName = "Временной промежуток.",
                Description = "",
                FacetId =     facetIds["question_type"]
            },
            new()
            {
                Code =        "Numeric",
                DisplayName = "Число с плавающей точкой.",
                Description = "",
                FacetId =     facetIds["question_type"]
            },
            new()
            {
                Code =        "CheckBox",
                DisplayName = "Флаг.",
                Description = "",
                FacetId =     facetIds["question_type"]
            },
            new()
            {
                Code =        "SelectList",
                DisplayName = "Выпадающий список.",
                Description = "",
                FacetId =     facetIds["question_type"]
            },
            new()
            {
                Code =        "MultiSelect",
                DisplayName = "Выпадающий список с множественным выбором.",
                Description = "",
                FacetId =     facetIds["question_type"]
            },
        };
        context.AddRange(facetItems_question_type);
        await context.SaveChangesAsync();
        #endregion

        #region questionnaire_type
        var facetItems_questionnaire_type = new List<FacetItem>
        {
            new()
            {
                Code =        "Base",
                DisplayName = "Базовый.",
                Description = "",
                FacetId =     facetIds["questionnaire_type"]
            },
            new()
            {
                Code =        "Additional",
                DisplayName = "Дополнительный.",
                Description = "",
                FacetId =     facetIds["questionnaire_type"]
            },
        };
        context.AddRange(facetItems_questionnaire_type);
        await context.SaveChangesAsync();
        #endregion
    }

    private async static Task SeedIdentityRoles(IdentityContext context)
    {
        var test =
            await
                context
                    .Roles
                    .AnyAsync(i => i.Name == ROLES_ADMIN);

        if (test)
            return;

        var userRoles = new List<IdentityRole<int>>
        {
            new()
            {
                Name           = ROLES_ADMIN,
                NormalizedName = ROLES_ADMIN.ToLower()
            },
            new()
            {
                Name           = ROLES_TEACHER,
                NormalizedName = ROLES_TEACHER.ToLower()
            },
            new()
            {
                Name           = ROLES_STUDENT,
                NormalizedName = ROLES_STUDENT.ToLower()
            }
        };
        context.AddRange(userRoles);
        await context.SaveChangesAsync();

        var userRoleIds =
            userRoles
                .ToImmutableDictionary(k => k.Name!, v => v.Id);

        var passwordHash = new PasswordHasher<object>().HashPassword(null!, "zxczxc");

        var users = new List<User>
        {
            new()
            {
                UserName           = "admin",
                NormalizedUserName = "admin",
                Email              = "abc@qwe.ru",
                NormalizedEmail    = "abc@qwe.ru",
                PasswordHash       = passwordHash,
                SecurityStamp      = Guid.NewGuid().ToString(),
                FirstName          = "Админ",
                MiddleName         = "Админович",
                LastName           = "Админов",
            },
            new()
            {
                UserName           = "teacher",
                NormalizedUserName = "teacher",
                Email              = "qwe@abc.ru",
                NormalizedEmail    = "qwe@abc.ru",
                PasswordHash       = passwordHash,
                SecurityStamp      = Guid.NewGuid().ToString(),
                FirstName          = "Преподаватель",
                MiddleName         = "Преподавателевич",
                LastName           = "Преподаватель",
                JobTitle           = "Старший по крутости преподаватель",
            },
            new()
            {
                UserName           = "student",
                NormalizedUserName = "student",
                Email              = "zxc@qwe.ru",
                NormalizedEmail    = "zxc@qwe.ru",
                PasswordHash       = passwordHash,
                SecurityStamp      = Guid.NewGuid().ToString(),
                FirstName          = "Студент",
                MiddleName         = "Студентович",
                LastName           = "Студентов",
            },
        };

        context.AddRange(users);
        await context.SaveChangesAsync();

        var userIds =
            users
                .ToImmutableDictionary(k => k.UserName!, v => v.Id);

        var identityUserRoles = new List<IdentityUserRole<int>>
        {
            new()
            {
                UserId = userIds["admin"],
                RoleId = userRoleIds[ROLES_ADMIN],
            },
            new()
            {
                UserId = userIds["teacher"],
                RoleId = userRoleIds[ROLES_TEACHER],
            },
            new()
            {
                UserId = userIds["student"],
                RoleId = userRoleIds[ROLES_STUDENT],
            },
        };

        context.AddRange(identityUserRoles);
        await context.SaveChangesAsync();
    }
}
