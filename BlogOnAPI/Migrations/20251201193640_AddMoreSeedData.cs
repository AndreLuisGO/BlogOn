using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogOnAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Content", "CreatedAt", "Title" },
                values: new object[,]
                {
                    { 2, "In this post, we explore how MediatR enables the CQRS pattern by separating read and write operations. This separation allows us to optimize each operation independently and maintain cleaner code architecture.", new DateTime(2024, 1, 5, 10, 30, 0, 0, DateTimeKind.Utc), "Understanding MediatR and CQRS Pattern" },
                    { 3, "Learn how to implement efficient caching strategies using Redis in ASP.NET Core applications. We'll cover cache invalidation, distributed caching, and performance optimization techniques.", new DateTime(2024, 1, 10, 14, 15, 0, 0, DateTimeKind.Utc), "Redis Caching Strategies for ASP.NET Core" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "BlogPostId", "CreatedAt", "Text" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2024, 1, 1, 13, 15, 0, 0, DateTimeKind.Utc), "I've been looking for a good example of vertical slice architecture. Thanks for sharing!" },
                    { 3, 1, new DateTime(2024, 1, 1, 15, 45, 0, 0, DateTimeKind.Utc), "How does this compare to traditional layered architecture?" },
                    { 4, 1, new DateTime(2024, 1, 2, 9, 20, 0, 0, DateTimeKind.Utc), "Clean and simple implementation. Would love to see more posts like this!" },
                    { 5, 2, new DateTime(2024, 1, 5, 11, 10, 0, 0, DateTimeKind.Utc), "MediatR has really simplified my projects. Great explanation!" },
                    { 6, 2, new DateTime(2024, 1, 5, 14, 25, 0, 0, DateTimeKind.Utc), "Could you elaborate more on the performance implications of using MediatR?" },
                    { 7, 2, new DateTime(2024, 1, 6, 8, 50, 0, 0, DateTimeKind.Utc), "The separation of concerns is fantastic. Makes unit testing so much easier." },
                    { 8, 2, new DateTime(2024, 1, 6, 16, 30, 0, 0, DateTimeKind.Utc), "Do you recommend using the pipeline behaviors for cross-cutting concerns?" },
                    { 9, 3, new DateTime(2024, 1, 10, 15, 5, 0, 0, DateTimeKind.Utc), "Redis has been a game-changer for our application performance!" },
                    { 10, 3, new DateTime(2024, 1, 10, 17, 40, 0, 0, DateTimeKind.Utc), "What's your strategy for cache invalidation in a microservices environment?" },
                    { 11, 3, new DateTime(2024, 1, 11, 10, 15, 0, 0, DateTimeKind.Utc), "Very helpful! I was struggling with distributed caching before reading this." },
                    { 12, 3, new DateTime(2024, 1, 11, 13, 55, 0, 0, DateTimeKind.Utc), "Would be great to see a follow-up on Redis clustering and high availability." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
