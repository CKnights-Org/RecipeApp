using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Models
{
    public class Review : BaseEntity
    {
        public string ReviewerName { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }

        public int RecipeID { get; set; }

        [ForeignKey(nameof(RecipeID))]
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
