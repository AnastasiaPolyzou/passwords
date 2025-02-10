using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

class PasswordManager
{
    private static readonly string filePath = "passwords.txt";
    private static readonly string key = "hG7v$9Pn2Xf@YqLzB!wM3dC#KsTpVjN5"; // ισχυρο σταθερο κλειδι

    public static void SavePassword(string service, string password)
    {
        if (!IsValidPassword(password)) // ελεγχος κρητηριων κωδικου
        {
            Console.WriteLine("The saving process failed. The password must be at least 8 characters long, contain at least one uppercase letter, and at least one symbol.");
            return;
        }

        string encryptedPassword = Encrypt(password);
        File.AppendAllText(filePath, $"{service}:{encryptedPassword}\n"); // αν ο κωδικος πληροι τα κρητηρια γινεται η κρυπτογραφηση
        Console.WriteLine("Password saved successfully.");
    }

    public static void RetrievePassword(string service)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                var parts = line.Split(':');
                if (parts[0] == service)
                {
                    string decryptedPassword = Decrypt(parts[1]);
                    Console.WriteLine($"Password for {service}: {decryptedPassword}"); // αποκρυπτογραφηση
                    return;
                }
            }
        }
        Console.WriteLine("Service not found."); // οταν δεν βρισκει το χρηστη 
    }

    private static bool IsValidPassword(string password)
    {
        // Ελέγχουμε αν ο κωδικός έχει τουλάχιστον 8 χαρακτήρες, ένα κεφαλαίο γράμμα και ένα σύμβολο
        return password.Length == 8 &&
               Regex.IsMatch(password, @"[A-Z]") && // Ελέγχουμε αν περιέχει τουλάχιστον ένα κεφαλαίο γράμμα
               Regex.IsMatch(password, @"[\W_]"); // Ελέγχουμε αν περιέχει τουλάχιστον ένα σύμβολο (όχι αλφαριθμητικό)
    }

    private static string Encrypt(string text)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key); // Χρησιμοποιούμε το νέο ισχυρό κλειδί
        aes.IV = new byte[16]; // Σταθερό IV 

        using var encryptor = aes.CreateEncryptor();
        byte[] inputBytes = Encoding.UTF8.GetBytes(text);
        byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }

    private static string Decrypt(string encryptedText)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key); // Χρησιμοποιούμε το ίδιο κλειδί
        aes.IV = new byte[16]; // Σταθερό IV

        using var decryptor = aes.CreateDecryptor();
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    static void Main()
    {
        int choice;
        // Βρόχος για να βεβαιωθούμε ότι ο χρήστης επιλέγει έγκυρη επιλογή
        while (true)
        {
            Console.WriteLine("1. Save Password\n2. Retrieve Password");
            Console.Write("Choose an option (1 or 2): ");

            // Ελέγχουμε αν η είσοδος είναι αριθμός και αν είναι 1 ή 2
            if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2))
            {
                break; // Βγαίνουμε από τον βρόχο αν η επιλογή είναι έγκυρη
            }
            else
            {
                Console.WriteLine("Μη έγκυρη επιλογή. Παρακαλώ επιλέξτε 1 ή 2.");
            }
        }

        Console.Write("Enter service name: ");
        string service = Console.ReadLine();

        if (choice == 1)
        {
            string password;
            // Ζητάμε από τον χρήστη να εισάγει έναν έγκυρο κωδικό με τις προϋποθέσεις
            while (true)
            {
                Console.Write("Enter password (8 characters, at least one uppercase letter, and one symbol): ");
                password = Console.ReadLine();

                // Ελέγχουμε αν ο κωδικός είναι έγκυρος
                if (IsValidPassword(password))
                {
                    break; // Εξοδος από τον βρόχο αν ο κωδικός είναι έγκυρος
                }
                else
                {
                    Console.WriteLine("Ο κωδικός δεν πληροί τις απαιτήσεις. Παρακαλώ δοκιμάστε ξανά.");
                }
            }

            SavePassword(service, password);
        }
        else if (choice == 2)
        {
            RetrievePassword(service);
        }
    }
}


