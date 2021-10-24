using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pinterest.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public ICollection<Board> Boards { get; set; }
        
        public ICollection<Following> Followings { get; set; }
    }
}