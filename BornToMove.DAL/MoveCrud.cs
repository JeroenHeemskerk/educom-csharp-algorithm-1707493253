using Microsoft.EntityFrameworkCore;
using Organizer;
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
        public List<MoveRating> ReadAllMovesSorted() {
            var mr = new List<MoveRating>();
            //var moves = context.MoveRating.GroupBy(r => r.Move);
            var moves = context.Move.GroupJoin(context.MoveRating, m=>m, r=>r.Move, (m, Ratings)=>new {move = m, Ratings});
            foreach (var moveInfo in moves) {
                var move = new MoveRating(moveInfo.Ratings.Select(r => r.Rating).DefaultIfEmpty().Average(), 0) {
                    Move = moveInfo.move
                };
                mr.Add(move);
            }
            var sorter = new RotateSort<MoveRating>();
            mr = sorter.Sort(mr, new RatingsComparer());
            return mr;
        }
        public MoveRating? ReadMoveById(int id) {
            var move = context.Move.Include("Ratings").Where(move=>move.id==id);
            var moveRating = move.GroupJoin(context.MoveRating, m => m, r => r.Move, (m, Ratings) => new { move = m, Ratings }).SingleOrDefault();
            if (moveRating != null) {
                MoveRating m = new MoveRating(moveRating.Ratings.Select(r => r.Rating).DefaultIfEmpty().Average(), 0) {
                    Move = moveRating.move
                };
                return m;
            }
            return null;
        }
        public Move? ReadMoveByName(string name) {
            return context.Move.Include("Ratings").Where(move => move.name == name).SingleOrDefault();
        }
        public void Update(Move updatedMove) {
            context.Move.Update(updatedMove);
            context.SaveChanges();
        }
        public void Delete(int id) {
            Move? m = ReadMoveById(id)?.Move;
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
