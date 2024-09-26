using Microsoft.EntityFrameworkCore;
using Survey.Domain.Entities;

namespace Survey.Infrastructure;

/// <summary>
/// Контекст базы данных
/// </summary>
public class DatabaseContext : DbContext
{
    /// <summary>
    /// Таблица опросов
    /// </summary>
    public DbSet<Domain.Entities.Survey> Surveys { get; set; } = null!;
    
    /// <summary>
    /// Таблица вопросов
    /// </summary>
    public DbSet<Question> Questions { get; set; } = null!;
    
    /// <summary>
    /// Таблица ответов
    /// </summary>
    public DbSet<Answer> Answers { get; set; } = null!;
    
    /// <summary>
    /// Таблица интервью
    /// </summary>
    public DbSet<Interview> Interviews { get; set; } = null!;
    
    /// <summary>
    /// Таблица результатов
    /// </summary>
    public DbSet<Result> Results { get; set; } = null!;
    
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public DatabaseContext() { }

    /// <summary>
    /// Конструктор с авто-миграцией
    /// </summary>
    /// <param name="options">Опции базы данных</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        if (Database.GetPendingMigrations().Any())
            Database.Migrate();
    }

    /// <summary>
    /// Создание сущностей в базе данных на основе моделей
    /// </summary>
    /// <param name="modelBuilder">Конструктор сущностей</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Survey>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            
            entity.HasMany(s => s.Questions)
                .WithOne(q => q.Survey)
                .HasForeignKey(q => q.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Text).IsRequired();
            
            entity.HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Text).IsRequired();
            
            entity.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired(false);
            
            entity.HasMany(i => i.Results)
                .WithOne(r => r.Interview)
                .HasForeignKey(r => r.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(i => i.Survey)
                .WithMany(s => s.Interviews)
                .HasForeignKey(i => i.SurveyId);
        });
        
        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.InterviewId).IsRequired();
            entity.Property(e => e.QuestionId).IsRequired();
            entity.Property(e => e.AnswerId).IsRequired();
            
            entity.HasOne(r => r.Interview)
                .WithMany(i => i.Results)
                .HasForeignKey(e => e.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Question>()
            .HasIndex(q => q.SurveyId);

        modelBuilder.Entity<Answer>()
            .HasIndex(a => a.QuestionId);

        modelBuilder.Entity<Result>()
            .HasIndex(r => r.InterviewId);
        
        // Вставка начальных данных
        SeedData(modelBuilder);
    }
    
    private void SeedData(ModelBuilder modelBuilder)
    {
        // Вставка данных в таблицу Surveys (Опросы)
        modelBuilder.Entity<Domain.Entities.Survey>().HasData(
            new Domain.Entities.Survey
            {
                Id = Guid.Parse("c1a4e4bb-97a5-41d9-ae6d-96f51d123abc"),
                Title = "Опрос удовлетворённости клиентов",
                Description = "Опрос для сбора отзывов клиентов."
            },
            new Domain.Entities.Survey
            {
                Id = Guid.Parse("a5d3b9e0-95a8-4a92-8b4d-d95b285ef123"),
                Title = "Опрос вовлечённости сотрудников",
                Description = "Опрос для оценки вовлечённости сотрудников."
            }
        );

        // Вставка данных в таблицу Questions (Вопросы)
        modelBuilder.Entity<Question>().HasData(
            new Question
            {
                Id = Guid.Parse("3b4a3749-53f4-4076-b519-b70a0df50f11"),
                Text = "Насколько вы довольны нашим сервисом?",
                SurveyId = Guid.Parse("c1a4e4bb-97a5-41d9-ae6d-96f51d123abc")
            },
            new Question
            {
                Id = Guid.Parse("a1fa8e7f-8b19-4897-b46d-991ff5178b52"),
                Text = "Насколько вероятно, что вы нас порекомендуете?",
                SurveyId = Guid.Parse("c1a4e4bb-97a5-41d9-ae6d-96f51d123abc")
            },
            new Question
            {
                Id = Guid.Parse("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5"),
                Text = "Как вы относитесь к своей текущей роли?",
                SurveyId = Guid.Parse("a5d3b9e0-95a8-4a92-8b4d-d95b285ef123")
            }
        );

        // Вставка данных в таблицу Answers (Ответы)
        modelBuilder.Entity<Answer>().HasData(
            // Ответы для вопроса "Насколько вы довольны нашим сервисом?"
            new Answer
            {
                Id = Guid.Parse("6c3f4314-944d-4f77-b63b-abc123456789"),
                Text = "Очень доволен",
                QuestionId = Guid.Parse("3b4a3749-53f4-4076-b519-b70a0df50f11")
            },
            new Answer
            {
                Id = Guid.Parse("81e273bc-3bd7-4730-991c-abc12345678a"),
                Text = "Вполне доволен",
                QuestionId = Guid.Parse("3b4a3749-53f4-4076-b519-b70a0df50f11")
            },
            new Answer
            {
                Id = Guid.Parse("7c2d7d14-2e32-49c9-b76d-abc12345678b"),
                Text = "Не доволен",
                QuestionId = Guid.Parse("3b4a3749-53f4-4076-b519-b70a0df50f11")
            },
            new Answer
            {
                Id = Guid.Parse("a9c1d284-9142-48f1-a9f2-abc12345678e"),
                Text = "Совсем не доволен",
                QuestionId = Guid.Parse("3b4a3749-53f4-4076-b519-b70a0df50f11")
            },
            // Ответы для вопроса "Насколько вероятно, что вы нас порекомендуете?"
            new Answer
            {
                Id = Guid.Parse("81b7e1d9-ec9f-41bb-8c91-abc12345678c"),
                Text = "Очень вероятно",
                QuestionId = Guid.Parse("a1fa8e7f-8b19-4897-b46d-991ff5178b52")
            },
            new Answer
            {
                Id = Guid.Parse("f7f9d140-9e88-45a2-bb3f-abc12345678d"),
                Text = "Скорее всего",
                QuestionId = Guid.Parse("a1fa8e7f-8b19-4897-b46d-991ff5178b52")
            },
            new Answer
            {
                Id = Guid.Parse("9d4d6f1b-bd94-40d2-988a-abc12345678f"),
                Text = "Маловероятно",
                QuestionId = Guid.Parse("a1fa8e7f-8b19-4897-b46d-991ff5178b52")
            },
            new Answer
            {
                Id = Guid.Parse("2b6cf581-987e-4f4e-b827-abc123456790"),
                Text = "Совсем не вероятно",
                QuestionId = Guid.Parse("a1fa8e7f-8b19-4897-b46d-991ff5178b52")
            },
            // Ответы для вопроса "Как вы относитесь к своей текущей роли?"
            new Answer
            {
                Id = Guid.Parse("27a7d9e5-66b6-4d2d-8e55-abc123456791"),
                Text = "Очень доволен",
                QuestionId = Guid.Parse("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5")
            },
            new Answer
            {
                Id = Guid.Parse("998cbb6c-9244-4f84-bc37-abc123456792"),
                Text = "Доволен",
                QuestionId = Guid.Parse("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5")
            },
            new Answer
            {
                Id = Guid.Parse("d8349e1d-86f6-4e82-b57f-abc123456793"),
                Text = "Не доволен",
                QuestionId = Guid.Parse("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5")
            },
            new Answer
            {
                Id = Guid.Parse("a4c1e354-6475-4873-b5f7-abc123456794"),
                Text = "Очень недоволен",
                QuestionId = Guid.Parse("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5")
            }
        );
    }
}