using EfLearning.Core.EntitiesHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EfLearning.Core.Users
{
    public class RefreshToken:ICreationTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public int UserId { get; set; }

        public virtual AppUser AppUser { get; set; }
        
    }
}
