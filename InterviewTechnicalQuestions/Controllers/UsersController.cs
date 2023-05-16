namespace InterviewTechnicalQuestions.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext dbContext;

        public UsersController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;

            this.dbContext.AddRange(
                new List<Agent>()
                {
                    new()
                    {
                        ID = 1,
                        FirstName = "James",
                        LastName = "Wilson",
                        IsAdmin = false,
                        Password = "&^%bhdjbd43456hjHGDgvg!!v",
                        Username = "JamesTheAgent",
                        Extension = "0132"
                    },
                    new()
                    {
                        ID = 2,
                        FirstName = "Brandon",
                        LastName = "Hall",
                        IsAdmin = false,
                        Password = "p@ssw0rd",
                        Username = "branhall",
                        Extension = "2125",
                    },
                }
            );

            this.dbContext.Admins.AddRange(
                new List<Admin>()
                {
                    new()
                    {
                        ID = 3,
                        FirstName = "Joshua",
                        LastName = "Phillips",
                        Password = "password",
                        Username = "josh01",
                    },
                    new()
                    {
                        ID = 4,
                        FirstName = "Phil",
                        LastName = "Thornton",
                        Password = "password",
                        Username = "HYVTVJGDJKvbdhabdcjbnWAOGD",
                    }
                }
            );

            dbContext.SaveChanges();
        }

        [HttpGet]
        public List<User> Get()
        {
            return this.dbContext.Agents
                .Select(
                    agent =>
                        new User()
                        {
                            ID = agent.ID,
                            FirstName = agent.FirstName,
                            LastName = agent.LastName,
                            IsAdmin = false,
                            Password = agent.Password,
                            Username = agent.Username,
                        }
                )
                .Union(
                    this.dbContext.Admins.Select(
                        admin =>
                            new User()
                            {
                                ID = admin.ID,
                                FirstName = admin.FirstName,
                                LastName = admin.LastName,
                                IsAdmin = true,
                                Password = admin.Password,
                                Username = admin.Username,
                            }
                    )
                )
                .ToList();
        }

        public void AddAgent()
        {
            // TODO: Add agent
        }
    }

    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class User : Person
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual bool IsAdmin { get; set; } = false;
    }

    public class Agent : User
    {
        public string Extension { get; set; }
    }

    public class Admin : User
    {
        public override bool IsAdmin => true;
    }

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Agent> Agents { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }
    }
}
