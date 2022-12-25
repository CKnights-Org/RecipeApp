using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Models
{
    public class IngredientRecipe : BaseEntity
    {
        public int IngredientID { get; set; }

        [ForeignKey(nameof(IngredientID))]
        public virtual Ingredient Ingredient { get; set; } = null!;

        public int RecipeID { get; set; }

        [ForeignKey(nameof(RecipeID))]
        public virtual Recipe Recipe { get; set; } = null!;

        public int Amount { get; set; }

        public string TypeOfAmount { get; set; } = null!;
    }
}
