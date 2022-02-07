using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRumosClassLibrary
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsAdmin { get; set; }
        public string PhotoPath { get; set; }
        //Guardar a pass do user como um valor hash (pass encriptada)
        public byte[] PasswordHash { get; set; }
        //camada extra de proteção da password atribuindo um codigo aleatorio à password do utilizador
        public byte[] PasswordSalt { get; set; }
        public bool Active { get; set; } = true;

        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<UserRecipeLike> UserRecipeLikes { get; set; }
        public ICollection<UserRecipeFavourite> UserRecipeFavourites { get; set; }
        public ICollection<RecipeComment> RecipeComments { get; set; }


    }
}
