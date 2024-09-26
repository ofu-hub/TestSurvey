using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Survey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("a5d3b9e0-95a8-4a92-8b4d-d95b285ef123"), "Опрос для оценки вовлечённости сотрудников.", "Опрос вовлечённости сотрудников" },
                    { new Guid("c1a4e4bb-97a5-41d9-ae6d-96f51d123abc"), "Опрос для сбора отзывов клиентов.", "Опрос удовлетворённости клиентов" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "SurveyId", "Text" },
                values: new object[,]
                {
                    { new Guid("3b4a3749-53f4-4076-b519-b70a0df50f11"), new Guid("c1a4e4bb-97a5-41d9-ae6d-96f51d123abc"), "Насколько вы довольны нашим сервисом?" },
                    { new Guid("a1fa8e7f-8b19-4897-b46d-991ff5178b52"), new Guid("c1a4e4bb-97a5-41d9-ae6d-96f51d123abc"), "Насколько вероятно, что вы нас порекомендуете?" },
                    { new Guid("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5"), new Guid("a5d3b9e0-95a8-4a92-8b4d-d95b285ef123"), "Как вы относитесь к своей текущей роли?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "Text" },
                values: new object[,]
                {
                    { new Guid("27a7d9e5-66b6-4d2d-8e55-abc123456791"), new Guid("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5"), "Очень доволен" },
                    { new Guid("2b6cf581-987e-4f4e-b827-abc123456790"), new Guid("a1fa8e7f-8b19-4897-b46d-991ff5178b52"), "Совсем не вероятно" },
                    { new Guid("6c3f4314-944d-4f77-b63b-abc123456789"), new Guid("3b4a3749-53f4-4076-b519-b70a0df50f11"), "Очень доволен" },
                    { new Guid("7c2d7d14-2e32-49c9-b76d-abc12345678b"), new Guid("3b4a3749-53f4-4076-b519-b70a0df50f11"), "Не доволен" },
                    { new Guid("81b7e1d9-ec9f-41bb-8c91-abc12345678c"), new Guid("a1fa8e7f-8b19-4897-b46d-991ff5178b52"), "Очень вероятно" },
                    { new Guid("81e273bc-3bd7-4730-991c-abc12345678a"), new Guid("3b4a3749-53f4-4076-b519-b70a0df50f11"), "Вполне доволен" },
                    { new Guid("998cbb6c-9244-4f84-bc37-abc123456792"), new Guid("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5"), "Доволен" },
                    { new Guid("9d4d6f1b-bd94-40d2-988a-abc12345678f"), new Guid("a1fa8e7f-8b19-4897-b46d-991ff5178b52"), "Маловероятно" },
                    { new Guid("a4c1e354-6475-4873-b5f7-abc123456794"), new Guid("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5"), "Очень недоволен" },
                    { new Guid("a9c1d284-9142-48f1-a9f2-abc12345678e"), new Guid("3b4a3749-53f4-4076-b519-b70a0df50f11"), "Совсем не доволен" },
                    { new Guid("d8349e1d-86f6-4e82-b57f-abc123456793"), new Guid("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5"), "Не доволен" },
                    { new Guid("f7f9d140-9e88-45a2-bb3f-abc12345678d"), new Guid("a1fa8e7f-8b19-4897-b46d-991ff5178b52"), "Скорее всего" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("27a7d9e5-66b6-4d2d-8e55-abc123456791"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("2b6cf581-987e-4f4e-b827-abc123456790"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("6c3f4314-944d-4f77-b63b-abc123456789"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("7c2d7d14-2e32-49c9-b76d-abc12345678b"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("81b7e1d9-ec9f-41bb-8c91-abc12345678c"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("81e273bc-3bd7-4730-991c-abc12345678a"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("998cbb6c-9244-4f84-bc37-abc123456792"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("9d4d6f1b-bd94-40d2-988a-abc12345678f"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("a4c1e354-6475-4873-b5f7-abc123456794"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("a9c1d284-9142-48f1-a9f2-abc12345678e"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("d8349e1d-86f6-4e82-b57f-abc123456793"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("f7f9d140-9e88-45a2-bb3f-abc12345678d"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("3b4a3749-53f4-4076-b519-b70a0df50f11"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("a1fa8e7f-8b19-4897-b46d-991ff5178b52"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("f8d6cb60-90f4-489f-83ff-2146eaf7c7a5"));

            migrationBuilder.DeleteData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("a5d3b9e0-95a8-4a92-8b4d-d95b285ef123"));

            migrationBuilder.DeleteData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("c1a4e4bb-97a5-41d9-ae6d-96f51d123abc"));
        }
    }
}
