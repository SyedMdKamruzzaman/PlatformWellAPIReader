using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DTO
{
    public class Well
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public Int32 id { get; set; }
        public Int32 platformId { get; set; }
        [NotMapped]
        public string PlatformName { get; set; }
        public string uniqueName { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

    }
}
