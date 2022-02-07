using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRumosClassLibrary
{
    public class RecipeComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Comment { get; set; }
        [DisplayName("UserId")]
        public User User { get; set; }
        [DisplayName("RecipeId")]
        public Recipe Recipe { get; set; }

    }
}
