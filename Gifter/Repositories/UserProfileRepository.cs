using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Gifter.Models;
using Gifter.Utils;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {

        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }
        public List<Post> GetAllPostsByUserId(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated,
                       p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId,
                       up.Name, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,
                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
                  FROM Post p
                       LEFT JOIN UserProfile up ON p.UserProfileId = up.id
                       LEFT JOIN Comment c on c.PostId = p.id
              where p.UserProfileId = @userId
              ORDER BY p.DateCreated;";

                    DbUtils.AddParameter(cmd, "@userId", userId);

                    var reader = cmd.ExecuteReader();

                    var posts = new List<Post>();
                    while (reader.Read())
                    {

                        posts.Add(new Post()
                        {
                            Id = DbUtils.GetInt(reader, "PostId"),
                            Title = DbUtils.GetString(reader, "Title"),
                            Caption = DbUtils.GetString(reader, "Caption"),
                            DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
                            UserProfileId = userId,
                            Comments = new List<Comment>()
                        });

                        //if (DbUtils.IsNotDbNull(reader, "CommentId"))
                        //{
                        //    Post.Comments.Add(new Comment()
                        //{
                        //    Id = DbUtils.GetInt(reader, "CommentId"),
                        //    Message = DbUtils.GetString(reader, "Message"),
                        //    PostId = id,
                        //    UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
                        //});
                    }

                    reader.Close();

                    return posts;
                }
            }
        }
        //public UserProfile GetUserByIdWithPosts(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"select up.Id as UserProfileID, up.[Name] as UserProfileName, up.Email as UserProfileEmail, up.ImageUrl as UserProfileImageUrl, up.Bio as UserProfileBio, up.DateCreated as UserProflieDataCreated, 

        //    p.Id AS PostId, p.Title as PostTitle, p.Caption as PostCaption, p.DateCreated AS PostDateCreated, p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId

        //        From UserProfile up
        //        LEFT JOIN Post p On p.UserProfileId = up.Id

        //        WHERE UserProfileID = @Id";

        //            DbUtils.AddParameter(cmd, "@Id", id);

        //    var reader = cmd.ExecuteReader();

        //            UserProfile userProfile = null;

        //            while (reader.Read())
        //            {
        //                if (userProfile == null)
        //                {
        //                    userProfile = new UserProfile()
        //                    {
        //                        Id = id,
        //                        Name = DbUtils.GetString(reader, "UserProfileName"),
        //                        Email = DbUtils.GetString(reader,"UserProfileEmail"),
        //                        ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
        //                        DateCreated = DbUtils.GetDateTime(reader, "UserProflieDataCreated"),
        //                        Post = new Post()
        //                        {

        //                        }
        //                        DateCreated = DbUtils.GetString(reader,"UserProfileBio"),
        //                    }
        //                }
        //            }
        //        }

    }
}
