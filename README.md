Password Manager Console App (.NET)

A simple C# console application for storing and retrieving passwords securely using AES encryption.

The app demonstrates:

Password validation (length, uppercase letter, symbol)

Symmetric encryption/decryption using AES

File-based storage for password records

Console-based user interaction

This project is intended for learning and backend practice.

 Tech Stack

Language: C#

Framework: .NET

Encryption: AES (System.Security.Cryptography)

Storage: Local file (passwords.txt)

Application Type: Console Application (no UI)

 Features
 Save Password

Validates password meets the following criteria:

Exactly 8 characters

At least one uppercase letter

At least one symbol

Encrypts password using AES

Saves password to local file in format:

serviceName:encryptedPassword

 Retrieve Password

Reads passwords from file

Decrypts password for a specific service

Displays it in console

 Technical Concepts Demonstrated

Input Validation

Regex-based checks for uppercase letters and symbols

Looping until valid input is provided

Encryption & Security

AES symmetric encryption with static key

Base64 encoding for storage

Decryption with same key

File Handling

Read/write to text file

Append mode for multiple services

Service-based retrieval

Console Application Best Practices

User menu with valid choice enforcement

Clear prompts and feedback

Continuous input validation

 How It Works

User runs the application.

Menu options:

1. Save Password
2. Retrieve Password


User selects option.

For saving:

Enters service name and password

Password is validated, encrypted, and stored

For retrieval:

Enters service name

Password is decrypted and displayed

 Example Usage
1. Save Password
2. Retrieve Password
Choose an option (1 or 2): 1
Enter service name: Gmail
Enter password (8 characters, at least one uppercase letter, and one symbol): Abc!1234
Password saved successfully.

Choose an option (1 or 2): 2
Enter service name: Gmail
Password for Gmail: Abc!1234


Limitations

Uses a fixed encryption key (for learning purposes)

No multi-user authentication

No secure storage (file is plaintext aside from AES-encrypted password)

Password length is fixed at 8 characters

No password update/delete functionality

 Possible Improvements

Move encryption key to environment variable or secure storage

Implement stronger password rules and variable length

Use JSON or database for storage instead of plain text

Add password update and delete options

Implement hashing for additional security

Add unit tests for validation and encryption functions

Why This Project Matters for Portfolio

Demonstrates practical encryption with AES

Shows secure file handling and input validation

Illustrates console application flow and user interaction

Serves as a backend-focused, standalone project suitable for learning and portfolio display
