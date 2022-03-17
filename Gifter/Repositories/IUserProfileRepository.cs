using Gifter.Models;
using System.Collections.Generic;

namespace Gifter.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile user);

        UserProfile GetByEmail(string email);
        List<Post> GetAllPostsByUserId(int userId);
    }
}