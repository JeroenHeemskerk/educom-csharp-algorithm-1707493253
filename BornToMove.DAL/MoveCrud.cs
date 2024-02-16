using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL {
    public static class MoveCrud {

        public static void Create(Move move) {
            using (var context = new MoveContext()) {
                context.Database.EnsureCreated();
                context.Move.Add(move);
                context.SaveChanges();
            }
        }

        public static List<Move> ReadAllMoves() {
            using (var context = new MoveContext()) {
                context.Database.EnsureCreated();
                List<Move> moves = context.Move.ToList<Move>();
                return moves;
            }
        }
        public static Move? ReadMoveById(int id) {
            using (var context = new MoveContext()) {
                context.Database.EnsureCreated();
                return context.Move.Where(move=>move.id==id).SingleOrDefault();
            }
        }
        public static Move? ReadMoveByName(string name) {
            using (var context = new MoveContext()) {
                context.Database.EnsureCreated();
                return context.Move.Where(move => move.name == name).SingleOrDefault();
            }
        }
        public static void Update(Move updatedMove) {
            using (var context = new MoveContext()) {
                context.Database.EnsureCreated();
                context.Move.Update(updatedMove);
            }
        }
        public static void Delete(int id) {
            using (var context = new MoveContext()) {
                context.Database.EnsureCreated();
                Move? m = ReadMoveById(id);
                if (m != null) {
                    context.Move.Remove(m);
                }
                context.SaveChanges();
            }
        }
    }
}
