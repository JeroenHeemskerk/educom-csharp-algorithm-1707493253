using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL {
    public class MoveCrud {
        private readonly MoveContext context;

        public MoveCrud(MoveContext context) {
            this.context = context;
        }

        public void Create(Move move) {
            context.Move.Add(move);
            context.SaveChanges();
        }

        public List<Move> ReadAllMoves() {
            List<Move> moves = context.Move.Include("Ratings").ToList<Move>();
            return moves;
        }
        public Move? ReadMoveById(int id) {
            return context.Move.Include("Ratings").Where(move=>move.id==id).SingleOrDefault();
        }
        public Move? ReadMoveByName(string name) {
            return context.Move.Include("Ratings").Where(move => move.name == name).SingleOrDefault();
        }
        public void Update(Move updatedMove) {
            context.Move.Update(updatedMove);
            context.SaveChanges();
        }
        public void Delete(int id) {
            Move? m = ReadMoveById(id);
            if (m != null) {
                context.Move.Remove(m);
            }
            context.SaveChanges();
        }
        public void RunSqlCommand(string sql) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw(sql);
        }

        public void AddRating(MoveRating r) {
            context.MoveRating.Add(r);
            context.SaveChanges();
        }

        public List<MoveRating> ReadAllRatingsByMove(Move move) {
            context.Database.EnsureCreated();
            List<MoveRating> ratings = context.MoveRating.Where(rating => rating.Move == move).ToList();
            return ratings;
        }
    }
}
