using BornToMove.DAL;
using Microsoft.Data.SqlClient;
using Organizer;
using System;
using System.Data;
using System.Security.Cryptography;

namespace BornToMove.Business {
    public class BuMove {
        private readonly MoveCrud moveCrud;

        public BuMove(MoveCrud moveCrud) {
            this.moveCrud = moveCrud;
        }

        public MoveRating GetRandomMove() {
            List<MoveRating> moves = moveCrud.ReadAllMoves();
            Random rng = new Random();
            int selectedMoveId = rng.Next(moves.Count);
            MoveRating move = moves[selectedMoveId];
            return move;
        }

        public List<MoveRating> GetAllMoves() {
            var moveRatings = moveCrud.ReadAllMoves();
            var sorter = new RotateSort<MoveRating>();
            moveRatings = sorter.Sort(moveRatings, new RatingsComparer());
            return moveRatings;
            //var moves = moveCrud.ReadAllMoves();
            //return moves;
        }

        public MoveRating? GetMoveById(int id) {
            MoveRating? move = moveCrud.ReadMoveById(id);

            return move;
        }

        public Move? GetMoveByName(string name) {
            Move? m = moveCrud.ReadMoveByName(name);
            return m;
        }

        public bool AddMove(Move m) {
            if(!doesMoveExist(m.name)) {
                moveCrud.Create(m);
                return true;
            }
            return false;
        }

        public void DeleteMove(Move m) {
            moveCrud.Delete(m.id);
        }

        public void EditMove(Move updatedMove) {
            moveCrud.Update(updatedMove);
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
