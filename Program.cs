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
        // Dictionary for storing predefined responses to user questions
        private static readonly Dictionary<string, string> Responses = new Dictionary<string, string>()
        {
            { "how are you?", "As an AI-powered program, I do not experience emotions. However, I am fully operational and ready to assist you with cybersecurity inquiries." },
            { "what is your purpose?", "My primary objective is to provide guidance on cybersecurity best practices, ensuring users stay informed about digital threats and safety measures." },
            { "what topics can i ask you about?", "You may ask me about password security, phishing scams, safe browsing practices, malware prevention, and other cybersecurity-related topics." },
            { "how can i create a strong password?", "A strong password should be at least 12-16 characters long, include a mix of uppercase and lowercase letters, numbers, and special symbols. Avoid using easily guessed words or personal information. Consider using a password manager to generate and store secure passwords." },
            { "what is phishing and how can i avoid it?", "Phishing is a cyberattack where malicious actors impersonate legitimate entities to steal sensitive information, such as passwords or credit card details. To avoid phishing, never click on suspicious links, verify email senders, and enable two-factor authentication (2FA) for added security." },
            { "how can i browse the internet safely?", "To browse safely, always keep your browser and security software updated, avoid clicking on unverified links, use strong passwords for online accounts, and be cautious when sharing personal information on unfamiliar websites." },
            { "how can i prevent malware infections?", "To prevent malware infections, install and regularly update reputable antivirus software, avoid downloading files from untrusted sources, enable automatic system updates, and exercise caution when opening email attachments or clicking on links from unknown senders." }
        };
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
            StartChatbot();
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
        private static void StartChatbot()
        {
            while (true)
            {
                // Add a visual divider between interactions for clarity.
                AddDivider();
                // Prompt the user for input.
                TypingEffect("\nPlease enter your cybersecurity-related question (type 'exit' to terminate the session): ", 30);
                string userInput = Console.ReadLine().Trim().ToLower();

                // Exit the chatbot session if the user types "exit".
                if (userInput == "exit")
                {
                    TypingEffect("Thank you for using the Cybersecurity Awareness Bot. Stay informed and practice safe online habits!", 30);
                    break;
                }

                // Retrieve and display the bot's response
                LogMessage($"User input: {userInput}");
                string response = GetResponse(userInput);
                TypingEffect(response, 30);
            }
        }

        // Retrieves a response based on the user's input.
        private static string GetResponse(string input)
        {
            return Responses.ContainsKey(input)
                ? Responses[input]
                : "I'm sorry, but I do not have information on that specific topic. Could you rephrase your question or ask about cybersecurity-related topics?";
        }

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
