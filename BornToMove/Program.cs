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
                    Move move = GetMove(ids[selectedMoveId]);
                    StartExercise(move);
                    break;
                case 1: //list all
                    List<Move> moves = GetAllMoves();
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

        //--------------------------------------------
        // GetFunctions from DB
        public static List<int> GetAllIds() {
            string connectionString = "Data Source=(localdb)\\born2move;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string queryString = "SELECT id FROM move ORDER BY id;";

            List<int> ids = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        ids.Add((int)reader["id"]);
                    }
                }
            }
            return ids;
        }
        public static List<Move> GetAllMoves() {
            string connectionString = "Data Source=(localdb)\\born2move;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string queryString = "SELECT * FROM move;";

            List<Move> moves = new List<Move>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Move m = new Move((int)reader["id"], (string)reader["name"], (string)reader["description"], (int)reader["sweatRate"]);
                        moves.Add(m);
                    }
                }
            }
            return moves;
        }
        public static Move GetMove(int id) {
            string connectionString = "Data Source=(localdb)\\born2move;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string queryString = $"SELECT * FROM move WHERE id={id};";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Move m = new Move((int)reader["id"], (string)reader["name"], (string)reader["description"], (int)reader["sweatRate"]);
                        return m;
                    }
                }
            }
            throw new Exception("Move not found.");
        }
        public static bool CreateMove(string name, string description, int sweatRate) {
            string connectionString = "Data Source=(localdb)\\born2move;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string queryString = "INSERT INTO move (name, description, sweatRate)" +
                                 "VALUES (@name, @description, @sweatRate)";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                //add name parameter (to prevent sql injection)
                command.Parameters.Add("@name", SqlDbType.VarChar);
                command.Parameters["@name"].Value = name;

                //add description parameter (to prevent sql injection)
                command.Parameters.Add("@description", SqlDbType.VarChar);
                command.Parameters["@description"].Value = description;

                //add sweatRate parameter (to prevent sql injection, shouldn't matter for this one)
                command.Parameters.Add("@sweatRate", SqlDbType.Int);
                command.Parameters["@sweatRate"].Value = sweatRate;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
        }
        //--------------------------------------------
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
                    if (doesMoveExist(moveName)) {
                        Console.Write("This name already exists. Try a different one: ");
                    } else { break; }
                }
            } while (true);

            Console.WriteLine("Enter a description for your new move: ");
            var description = Console.ReadLine();

            Console.Write("Enter a sweatRate for your new move (1-5): ");
            int sweatRate = HandleInput(Enumerable.Range(1, 5).ToArray());

            if (CreateMove(moveName, description, sweatRate)) {
                Console.WriteLine("New move created successfully.");
            } else {
                Console.WriteLine("Move creation failed. Please try again.");
            }
        }

        public static bool doesMoveExist(string moveName) {
            string connectionString = "Data Source=(localdb)\\born2move;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string queryString = "SELECT id FROM move WHERE name=@name;";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@name", SqlDbType.VarChar);
                command.Parameters["@name"].Value = moveName;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) {
                    return reader.HasRows;
                }
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
