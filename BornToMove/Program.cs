using System.Data;
using System.Data.SqlClient;
using BornToMove.Business;

namespace BornToMove {
    public class Program {
        public static void Main(string[] _) {
            Console.WriteLine("Looks like it's time to start moving!");
            Console.WriteLine("Would you like us to suggest an exercise (0), or would you rather choose (1)?");
            Console.Write("Enter you Preference: ");
            int userInput = HandleInput([0, 1]);
            switch (userInput) {
                case 0: //suggestion
                    Move move = BuMove.GetRandomMove();
                    StartExercise(move);
                    break;
                case 1: //list all
                    List<Move> moves = BuMove.GetAllMoves();
                    DisplayMoves(moves);
                    Console.Write("Please make your selection (Enter the corresponding number): ");
                    int selectedMove = HandleInput(Enumerable.Range(0,moves.Count+1).ToArray());
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

        public static void DisplayMoves(List<Move> moves) {
            Console.WriteLine("0 - Make your own move (enter info on a new move)");
            for (int i = 0; i < moves.Count; i++) {
                Console.WriteLine($"{i+1} - Name: {moves[i].name, -15} Sweat Rate: {moves[i].sweatRate}");
            }
        }

        public static void EnterNewMove() {
            string? moveName;
            Console.Write("Input your new move's name: ");
            do {
                moveName = Console.ReadLine();
                if (moveName != null) {
                    if (BuMove.doesMoveExist(moveName)) {
                        Console.Write("This name already exists. Try a different one: ");
                    } else { break; }
                }
            } while (true);

            Console.WriteLine("Enter a description for your new move: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Enter a sweatRate for your new move (1-5): ");
            int sweatRate = HandleInput(Enumerable.Range(1, 5).ToArray());

            Move m = new Move(moveName, description, sweatRate);
            if (BuMove.AddMove(m)) {
                Console.WriteLine("Move added to DB.");
            } else {
                Console.WriteLine("Move could not be added.");
            }
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
