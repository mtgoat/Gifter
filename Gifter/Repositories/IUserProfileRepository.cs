using Gifter.Models;
using System.Collections.Generic;

namespace Gifter.Repositories
{
    public interface IUserProfileRepository
    {
        List<Post> GetAllPostsByUserId(int userId);
    }
}