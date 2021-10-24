using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Pinterest.Models
{
    [DataContract]
    public class Board
    {
        [DataMember]
        public int BoardId { get; set; }
        
        [DataMember, Required]
        public string Name { get; set; }
        
        [DataMember]
        public string Description { get; set; }
        
        public ICollection<Pin> Pins { get; set; }
        
        public User Creator { get; set; }
        [DataMember]
        public int CreatorId { get; set; }
        
        public ICollection<Following> Followings { get; set; }
    }
}