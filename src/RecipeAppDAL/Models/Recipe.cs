using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Models
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual List<IngredientRecipe> IngredientRecipe { get; set; } = null!;

        public virtual List<Review> Reviews { get; set; } = null!;
    }
}
