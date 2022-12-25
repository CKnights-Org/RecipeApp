using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecipeAppMVC.Models.Recipe
{
    public class ReviewModel
    {
        public string ReviewerName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
