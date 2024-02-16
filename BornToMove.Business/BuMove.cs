using BornToMove.DAL;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BornToMove.Business {
    public static class BuMove {

        public static Move GetRandomMove() {
            List<Move> moves = MoveCrud.ReadAllMoves();
            Random rng = new Random();
            int selectedMoveId = rng.Next(moves.Count);
            Move move = moves[selectedMoveId];
            return move;
        }

        public static List<Move> GetAllMoves() {
            var x = MoveCrud.ReadAllMoves();
            Console.WriteLine(x);
            return x;
        }

        public static bool AddMove(Move m) {
            if(!doesMoveExist(m.name)) {
                MoveCrud.Create(m);
                return true;
            }
            return false;
        }

        public static bool doesMoveExist(string name) {
            return MoveCrud.ReadMoveByName(name) != null;
        }
    }
}
