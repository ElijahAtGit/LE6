using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace BlogTestUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlData db = GetConnection();
            Register(db);

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        /* Returns user */
        private static UserModel GetCurrentUser(SqlData db)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            UserModel user = db.Authenthicate(username, password);

            return user;
        }

        public static void Authenticate(SqlData db)
        {
            UserModel user = GetCurrentUser(db);

            if(user == null)
            {
                Console.WriteLine("Invalid credentials");
            }
            else
            {
                Console.WriteLine($"Welcome, {user.UserName}");
            }
        }

        public static void Register(SqlData db)
        {
            Console.Write("Enter a new username: ");
            var username = Console.ReadLine();

            Console.Write("Enter a new password: ");
            var password = Console.ReadLine();

            Console.Write("Enter a new first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter a new last name: ");
            var lastName = Console.ReadLine();

            db.Register(username, firstName, lastName, password);
        }

        private static void AddPost(SqlData db)
        {
            UserModel user = GetCurrentUser(db);

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Write body: ");
            string body = Console.ReadLine();

            PostModel post = new PostModel
            {
                Title = title,
                Body = body,
                DateCreated = DateTime.Now,
                UserId = user.Id
            };

            db.AddPost(post);
        }

        private static void ListPosts(SqlData db)
        {
            List<ListPostModel> posts = db.ListPosts();

            foreach(ListPostModel post in posts)
            {
                Console.WriteLine($"{post.Id}. Title: {post.Title} by {post.UserName} [{post.DateCreated.ToString("yyyy-MM-dd")}]");
                Console.WriteLine($"{post.Body.Substring(0, 20)}...");
                Console.WriteLine();
            }
        }

        private static void ShowPostDetails(SqlData db)
        {
            Console.WriteLine("Enter a post ID: ");
            int id = Int32.Parse(Console.ReadLine());

            ListPostModel post = db.ShowPostDetails(id);
            Console.WriteLine(post.Title);
            Console.WriteLine($"by {{post.FirstName}} {{postLastName}} [{{post.UserName}}]");
            
            Console.WriteLine();

            Console.WriteLine(post.Body);

            Console.WriteLine(post.DateCreated.ToString("MMM d yyyy"));
        }

        static SqlData GetConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();
            ISqlDataAccess dbAccess = new SqlDataAccess(config);
            SqlData db = new SqlData(dbAccess);

            return db;
        }
    }
}