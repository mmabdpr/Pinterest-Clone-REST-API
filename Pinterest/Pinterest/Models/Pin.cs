using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Runtime.Serialization;

namespace Pinterest.Models
{
    [DataContract]
    public class Pin
    {
        [DataMember]
        public int PinId { get; set; }
        
        [DataMember]
        public string Link { get; set; }
        
        [DataMember, Required]
        public int BoardId { get; set; }
        public Board Board { get; set; }
        
        [Required, DataMember]
        public string Note { get; set; }
        
        [Required, DataMember]
        public Image Image { get; set; }
    }
}