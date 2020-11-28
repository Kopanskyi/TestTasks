using System;

namespace Rail_Fence_Cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "WEAREDISCOVEREDFLEEATONCE";
            var encodedText = RailFenceCipher.Encode(text, 5);
            var decodedText = RailFenceCipher.Decode(encodedText, 5);

            Console.WriteLine(text);
            Console.WriteLine(encodedText);
            Console.WriteLine(decodedText);
            Console.ReadKey();
        }
    }
}
