using BlogDataLibrary.Models;

namespace BlogDataLibrary.Data
{
    public interface ISqlData
    {
        void AddPost(PostModel post);
        UserModel Authenthicate(string username, string password);
        List<ListPostModel> ListPosts();
        void Register(string username, string firstName, string lastName, string password);
        ListPostModel ShowPostDetails(int id);
    }
}