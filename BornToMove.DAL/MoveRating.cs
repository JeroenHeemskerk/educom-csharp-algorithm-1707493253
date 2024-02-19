using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL {
    public class MoveRating {
        public int id { get; set; }
        public Move Move {  get; set; }
        public double Rating { get; set; }
        public double Vote { get; set; }
        public MoveRating(double rating, double vote) {
            Move = new Move();
            Rating = rating;
            Vote = vote;
        }
    }
}
