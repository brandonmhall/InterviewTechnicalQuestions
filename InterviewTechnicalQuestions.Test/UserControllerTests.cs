namespace InterviewTechnicalQuestions.Test
{
    using InterviewTechnicalQuestions.Controllers;
    using Microsoft.EntityFrameworkCore;
    using FluentAssertions;

    public class UserControllerTests
    {
        private readonly UsersController usersController;
        private readonly DatabaseContext databaseContext;

        public UserControllerTests()
        {
            var dbBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            dbBuilder.UseInMemoryDatabase(databaseName: "InMemoryTests");
            databaseContext = new(dbBuilder.Options);

            this.usersController = new(databaseContext);
        }

        [Fact]
        public void GetUsers_ReturnsAllAgentsAndAdmins()
        {
            this.usersController
                .Get()
                .Should()
                .BeEquivalentTo(
                    new List<User>()
                    {
                        new()
                        {
                            ID = 1,
                            FirstName = "James",
                            LastName = "Wilson",
                            IsAdmin = false,
                            Password = "&^%bhdjbd43456hjHGDgvg!!v",
                            Username = "JamesTheAgent",
                        },
                        new()
                        {
                            ID = 2,
                            FirstName = "Brandon",
                            LastName = "Hall",
                            IsAdmin = false,
                            Password = "p@ssw0rd",
                            Username = "branhall",
                        },
                        new()
                        {
                            ID = 3,
                            FirstName = "Joshua",
                            LastName = "Phillips",
                            Password = "password",
                            Username = "josh01",
                            IsAdmin = true,
                        },
                        new()
                        {
                            ID = 4,
                            FirstName = "Phil",
                            LastName = "Thornton",
                            Password = "password",
                            Username = "HYVTVJGDJKvbdhabdcjbnWAOGD",
                            IsAdmin = true,
                        }
                    }
                );
        }
    }
}
