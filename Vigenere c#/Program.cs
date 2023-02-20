
#define INCLUDE_UPPER
#define INCLUDE_NUMBERS 
#define INCLUDE_SYMBOLS
#define IGNORE_INVALID_CHARACTERS 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace VigenereCipher
{
    public class Program
    {

        public static void Main(string[] args)
        {

            Console.WriteLine("Pick one\n1.Encryption.\n2.Decryption");
           int pic = int.Parse(Console.ReadLine());
            
            switch (pic)
            {
                case 1:
                    Console.WriteLine("Write a message");
                    string message = Console.ReadLine();
                    Console.WriteLine("Write a key");
                    string key = Console.ReadLine();
                    string ciphertext = EncryptVigenere(message, key);
                    Console.WriteLine($"Cyphertext: {ciphertext}");
                    break;
                case 2:
                    Console.WriteLine("Write a message");
                    string msg = Console.ReadLine();
                    Console.WriteLine("Write a key");
                    string key2 = Console.ReadLine();

                    string decrypted = DecryptVigenere(msg, key2);
                    Console.WriteLine($"Decrypted: {decrypted}");

                    break;
                
            }


        }

        #region Algorithm
        static readonly int AlphabetLength;
        static Dictionary<char, byte> Encoder = new Dictionary<char, byte>();
        static Dictionary<byte, char> Decoder = new Dictionary<byte, char>();

        static Program()
        {


            byte code = 0;
           
            //mazosios raides
            Encoder.Add(' ', code);
            Decoder.Add(code++, ' ');
            for (char c = 'a'; c <= 'z'; ++c)
            {
                Encoder.Add(c, code);
                Decoder.Add(code++, c);
            }
            //iterpia didziasias raides

            for (char c = 'A'; c <= 'Z'; ++c)
            {
                Encoder.Add(c, code);
                Decoder.Add(code++, c);
            }

            //iterpia skaicius

            for (char c = '0'; c <= '9'; ++c)
            {
                Encoder.Add(c, code);
                Decoder.Add(code++, c);
            }

            // iterpia ASCII koduote

            for (char c = '!'; c <= '/'; ++c)
            {
                Encoder.Add(c, code);
                Decoder.Add(code++, c);
            }
            for (char c = ':'; c <= '@'; ++c)
            {
                Encoder.Add(c, code);
                Decoder.Add(code++, c);
            }
            for (char c = '['; c <= '`'; ++c)
            {
                Encoder.Add(c, code);
                Decoder.Add(code++, c);
            }
            for (char c = '{'; c <= '~'; ++c)
            {
                Encoder.Add(c, code);
                Decoder.Add(code++, c);
            }


            AlphabetLength = Encoder.Count();


        }

        public static string EncryptVigenere(string message, string key)
        {

                Console.WriteLine("Encrypting (Vigenere):");
                Console.WriteLine($"\tMessage: {message}");
                Console.WriteLine($"\tKey: {key}");


            message = string.Concat(message.Where(c => Encoder.ContainsKey(c)));
            if (message.Length == 0) return "";
            key = string.Concat(key.Where(c => Encoder.ContainsKey(c)));
            if (key.Length == 0) return "Invalid key!";

                if (message.Any(c => !Encoder.ContainsKey(c))) return "Invalid character in message!";
                if (key.Any(c => !Encoder.ContainsKey(c))) return "Invalid character in key!";




            StringBuilder ciphertext = new StringBuilder(message.Length);
            int k = 0;
            foreach (var m in message)
            {

                ciphertext.Append(Decoder[(byte)((Encoder[m] + Encoder[key[k++]]) % AlphabetLength)]);
                if (k == key.Length) k = 0;
            }



            return ciphertext.ToString();
        }

        public static string DecryptVigenere(string ciphertext, string key)
        {

            ciphertext = string.Concat(ciphertext.Where(c => Encoder.ContainsKey(c)));
            if (ciphertext.Length == 0) return "";
            key = string.Concat(key.Where(c => Encoder.ContainsKey(c)));
            if (key.Length == 0) return "Invalid key!";

                if (ciphertext.Any(c => !Encoder.ContainsKey(c))) return "Invalid character in ciphertext!";
                if (key.Any(c => !Encoder.ContainsKey(c))) return "Invalid character in key!";



            //desifravimas
            StringBuilder decrypted = new StringBuilder(ciphertext.Length);
            int k = 0;
            foreach (var c in ciphertext)
            {

                decrypted.Append(Decoder[(byte)((AlphabetLength + Encoder[c] - Encoder[key[k++]]) % AlphabetLength)]);
                if (k == key.Length) k = 0;
            }

            return decrypted.ToString();
        }
        #endregion
    }
}