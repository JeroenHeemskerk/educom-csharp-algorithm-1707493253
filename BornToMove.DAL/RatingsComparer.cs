using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL {
    public class RatingsComparer : IComparer<MoveRating> {
        public int Compare(MoveRating? x, MoveRating? y) {
            return Comparer<double?>.Default.Compare(y?.Rating, x?.Rating);
        }
    }
}
