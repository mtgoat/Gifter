using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

//This allows other repos to access the SQLconnection by inheritance

namespace Gifter.Repositories
{
    //NOTE: The use of the keyword abstract in the class definition is something we haven't seen before. It indicates that our BaseRepository class cannot be directly instantiated, but can ONLY be used by inheritance.

    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //NOTE: We mark the Connection property as protected to make it available to child classes, but inaccessible to any other code.
        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
