using BornToMove.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove {
    public class Move {

        [Key]
        public int id {  get; set; }

        [StringLength(255, MinimumLength = 1)]
        [Required]
        public string name { get; set; }

        [StringLength(255, MinimumLength = 1)]
        [Required]
        public string description { get; set; }

        [Range(1,5)]
        [Required]
        public int sweatRate { get; set; }
        virtual public ICollection<MoveRating> Ratings { get; set; }
        public Move(int id, string name, string description, int sweatRate) {
            this.id = id;
            this.name = name;
            this.description = description;
            this.sweatRate = sweatRate;
            this.Ratings = new List<MoveRating>();
        }

        public Move(string name, string description, int sweatRate) {
            this.name = name;
            this.description = description;
            this.sweatRate = sweatRate;
            this.Ratings = new List<MoveRating>();
        }

        public Move() {
            name = "";
            description = "";
            Ratings = new List<MoveRating>();
        }

        public void ShowMove() {
            Console.WriteLine("Name:        " + name);
            Console.WriteLine("Sweat Rate:  " + sweatRate);
            Console.WriteLine("Rating:      " + Math.Round(Ratings.Select(r => r.Rating).DefaultIfEmpty().Average(), 2) + "/5");
            Console.WriteLine("Description: " + description);
        }
    }
}
