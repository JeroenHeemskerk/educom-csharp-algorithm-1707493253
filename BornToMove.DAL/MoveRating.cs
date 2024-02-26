using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL {
    public class MoveRating {

        [Key]
        public int id { get; set; }

        [Required]
        public Move Move {  get; set; }

        [Range(1,5)]
        [Required]
        public double Rating { get; set; }

        [Range (1,5)]
        [Required]
        public double Vote { get; set; }
        public MoveRating(double rating, double vote) {
            Move = new Move();
            Rating = rating;
            Vote = vote;
        }

        public MoveRating() {
            Move = new Move();
        }
    }
}
