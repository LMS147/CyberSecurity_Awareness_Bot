using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Threading;


namespace CyberSecurity_Awareness_Bot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Play a voice greeting in a separate thread to allow simultaneous display of ASCII art.
            TypingEffect("Initializing Cybersecurity Awareness Bot...", 30);
            Thread greetingThread = new Thread(PlayVoiceGreeting);
            greetingThread.Start();
            // Display visually appealing ASCII art with a slow reveal effect.
            DisplayASCIIArt();
            greetingThread.Join();

            // Greet the user and start the chatbot session.
            GreetUser();
        }

        // Plays a pre-recorded voice greeting if the audio file exists.
        private static void PlayVoiceGreeting()
        {
            // Construct the file path for the greeting audio file.
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

            // Check if the audio file exists.
            if (!File.Exists(filePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypingEffect($"Error: The file '{filePath}' was not found.", 30);
                Console.ResetColor();
                return;
            }

            try
            {
                SoundPlayer player = new SoundPlayer(filePath);
                player.PlaySync(); // Plays the audio file synchronously
            }
            catch (Exception ex)
            {
                // Handle any exceptions during audio playback.
                Console.ForegroundColor = ConsoleColor.Red;
                TypingEffect($"Error playing voice greeting: {ex.Message}", 30);
                Console.ResetColor();
            }
        }

        // Displays ASCII art with a slow reveal effect to enhance the visual appeal of the chatbot.
        private static void DisplayASCIIArt()
        {
            // Define the ASCII art string
            string asciiArt = @"
 ██████╗██╗   ██╗██████╗ ███████╗██████╗ ███████╗███████╗ ██████╗██╗   ██╗██████╗ ██╗████████╗██╗   ██╗   
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝██╔════╝██║   ██║██╔══██╗██║╚══██╔══╝╚██╗ ██╔╝   
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗█████╗  ██║     ██║   ██║██████╔╝██║   ██║    ╚████╔╝    
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══╝  ██║     ██║   ██║██╔══██╗██║   ██║     ╚██╔╝     
╚██████╗   ██║   ██████╔╝███████╗██║  ██║███████║███████╗╚██████╗╚██████╔╝██║  ██║██║   ██║      ██║      
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝      ╚═╝      
 █████╗ ██╗    ██╗ █████╗ ██████╗ ███████╗███╗   ██╗███████╗███████╗███████╗    ██████╗  ██████╗ ████████╗
██╔══██╗██║    ██║██╔══██╗██╔══██╗██╔════╝████╗  ██║██╔════╝██╔════╝██╔════╝    ██╔══██╗██╔═══██╗╚══██╔══╝
███████║██║ █╗ ██║███████║██████╔╝█████╗  ██╔██╗ ██║█████╗  ███████╗███████╗    ██████╔╝██║   ██║   ██║   
██╔══██║██║███╗██║██╔══██║██╔══██╗██╔══╝  ██║╚██╗██║██╔══╝  ╚════██║╚════██║    ██╔══██╗██║   ██║   ██║   
██║  ██║╚███╔███╔╝██║  ██║██║  ██║███████╗██║ ╚████║███████╗███████║███████║    ██████╔╝╚██████╔╝   ██║   
╚═╝  ╚═╝ ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝╚══════╝╚══════╝╚══════╝    ╚═════╝  ╚═════╝    ╚═╝   
    ";
            // Display the ASCII art with a slow reveal effect.
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (char c in asciiArt)
            {
                Console.Write(c);
                Thread.Sleep(5); // Slow reveal effect
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        // Greets the user by asking for their name and personalizing the interaction.
        private static void GreetUser()
        {
            // Introduce the chatbot and ask for the user's name.
            TypingEffect("\nHello! Welcome to the Cybersecurity Awareness Bot, your virtual assistant for digital safety and best practices.", 30);
            TypingEffect("\nMay I have your name, please? ", 30);
            string userName = Console.ReadLine();

            // Read the user's input and handle empty or invalid names
            if (string.IsNullOrWhiteSpace(userName))
            {
                TypingEffect("\nIt seems you did not provide a name. I will refer to you as 'Friend'.", 30);
                userName = "Friend";
            }

            // Log the user's name for debugging purposes.
            LogMessage($"User identified as: {userName}");
            // Personalize the greeting based on the user's name.
            TypingEffect($"\nPleased to meet you, {userName}. How can I assist you with cybersecurity concerns today?", 30);
        }

        // Starts the chatbot session, allowing the user to ask questions until they type "exit".

        /// Adds a visual divider to the console output for better readability.
        private static void AddDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            TypingEffect(new string('=', Console.WindowWidth - 1), 10);
            Console.ResetColor();
        }

        // Simulates a typing effect for text output to make the interaction more engaging.
        private static void TypingEffect(string text, int delay = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);// Simulate typing delay.
            }
            Console.WriteLine();
        }

        // Logs a message to a file for debugging purposes.
        // Handles logging errors gracefully.
        private static void LogMessage(string message)
        {
            // Append the message to a log file with a timestamp
            try
            {
                File.AppendAllText("chatbot_log.txt", $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            // Handle any errors during logging.
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error logging message: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
