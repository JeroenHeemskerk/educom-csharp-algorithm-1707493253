using BornToMove.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Security.Cryptography;

namespace BornToMove.Business {
    public class BuMove {
        private readonly MoveCrud moveCrud;

        public BuMove(MoveCrud moveCrud) {
            this.moveCrud = moveCrud;
        }

        public Move GetRandomMove() {
            List<Move> moves = moveCrud.ReadAllMoves();
            Random rng = new Random();
            int selectedMoveId = rng.Next(moves.Count);
            Move move = moves[selectedMoveId];
            return move;
        }

        public List<MoveRating> GetAllMoves() {
            var moveRatings = moveCrud.ReadAllMovesSorted();
            return moveRatings;
            //var moves = moveCrud.ReadAllMoves();
            //return moves;
        }

        public bool AddMove(Move m) {
            if(!doesMoveExist(m.name)) {
                moveCrud.Create(m);
                return true;
            }
            return false;
        }

        public bool doesMoveExist(string name) {
            return moveCrud.ReadMoveByName(name) != null;
        }

        public void PopulateMoveTable() {
            if (moveCrud.ReadAllMoves().Count == 0) {
                var sql = File.ReadAllText("C:\\Users\\thoma\\Source\\Repos\\educom-csharp-algorithm\\sql\\b2m-insert-into-move.sql");
                moveCrud.RunSqlCommand(sql);
            }
        }

        //----------------------------------------------------------
        //-------------------- RATING FUNCTIONS --------------------
        //----------------------------------------------------------

        public void AddRating(MoveRating r) {
            r.Move.Ratings.Add(r);
            moveCrud.AddRating(r);
            moveCrud.Update(r.Move);
        }

        public double GetAverageRating(Move move) {
            move.Ratings = moveCrud.ReadAllRatingsByMove(move);
            return move.Ratings.Average(r => r.Rating);
        }
    }
}
