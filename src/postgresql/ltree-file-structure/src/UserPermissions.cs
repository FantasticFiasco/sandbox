using System.Collections.Generic;

namespace FileSystem
{
    public class UserPermissions
    {
        public UserPermissions(string userId)
        {
            UserId = userId;
            Roles = new List<Role>();
        }

        public string UserId { get; }

        public List<Role> Roles { get; }
    }
}
