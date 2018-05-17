using System.Collections.Generic;

namespace FileSystem
{
    public class UserPermissions
    {
        public UserPermissions()
        {
            Roles = new List<Role>();
        }

        public string UserId { get; set; }

        public List<Role> Roles { get; }
    }
}
