using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Data;
using System.Data.SqlClient;
using BornToMove;

namespace Organizer {
    public class Program {
        public static void Main(string[] _) {
            Console.WriteLine("Looks like it's time to start moving!");
            Console.WriteLine("Would you like us to suggest an exercise (0), or would you rather choose (1)?");
            Console.Write("Enter you Preference: ");
            int userInput = HandleInput([0, 1]);
            switch (userInput) {
                case 0: //suggestion
                    List<int> ids = GetAllIds();
                    Random rng = new Random();
                    int selectedMoveId = rng.Next(ids.Count);
                    Move move = GetMove(selectedMoveId);
                    StartExercise(move);
                    break;
                case 1: //list all
                    List<Move> moves = GetAllMoves();
                    DisplayMoves(moves);
                    Console.Write("Please make your selection (Enter the corresponding number): ");
                    int selectedMove = HandleInput(Enumerable.Range(0,moves.Count).ToArray());
                    switch (selectedMove) {
                        case 0: //new move
                            EnterNewMove();
                            break;
                        default: //existing move
                            StartExercise(moves[selectedMove-1]);
                            break;
                    }
                    break;
            }
        }

        //--------------------------------------------
        // Dummy GetFunctions from DB
        public static List<int> GetAllIds() {
            return new List<int>([1,2,3]);
        }
        public static List<Move> GetAllMoves() {
            return new List<Move>();
        }
        public static Move GetMove(int id) {
            return new Move(1,"run", "run fast", 2);
        }
        //--------------------------------------------
        public static void DisplayMoves(List<Move> moves) {
            Console.WriteLine("0 - Make your own move (enter info on a new move)");
            for (int i = 0; i < moves.Count; i++) {
                Console.WriteLine($"{i} - Name: {moves[i].name} Sweat Rate: {moves[i].sweatRate}");
            }
        }

        public static void EnterNewMove() {
            Console.WriteLine("writing to db... (not implemented)");
        }

        public static void StartExercise(Move move) {
            move.ShowMove();
            Console.Write("Give Rating: ");
            int moveRating = HandleInput(Enumerable.Range(1, 5).ToArray());
            Console.Write("Give Intensity: ");
            int moveIntensityRating = HandleInput(Enumerable.Range(1, 5).ToArray());
            Console.WriteLine($"Rating: {moveRating}, Intensity: {moveIntensityRating}");
        }

        public static int HandleInput(int[] acceptableValues) {
            while (true) {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int value)) {
                    if (acceptableValues.Contains(value)) {
                        return value;
                    }
                }
                Console.Write("Invalid input. Please try again: ");
            }
        }
    }
}
