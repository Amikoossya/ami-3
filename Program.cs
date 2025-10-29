using System;
using System.IO;
using System.Text;

class PolybiusCipher
{
    // Таблица Полибия (I и J считаются одной буквой)
    static char[,] square = {
        { 'A', 'B', 'C', 'D', 'E' },
        { 'F', 'G', 'H', 'I', 'K' },
        { 'L', 'M', 'N', 'O', 'P' },
        { 'Q', 'R', 'S', 'T', 'U' },
        { 'V', 'W', 'X', 'Y', 'Z' }
    };

    // === Шифрование ===
    static string Encrypt(string text)
    {
        text = text.ToUpper().Replace(\"J\", \"I\");
        StringBuilder result = new StringBuilder();

        foreach (char ch in text)
        {
            if (char.IsLetter(ch))
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (square[i, j] == ch)
                        {
                            result.Append($\"{i + 1}{j + 1} \");
                        }
                    }
                }
            }
            else if (ch == ' ')
            {
                result.Append(\"  \"); // двойной пробел между словами
            }
        }
        return result.ToString().Trim();
    }

    // === Расшифровка ===
    static string Decrypt(string cipher)
    {
        string[] parts = cipher.Split(' ');
        StringBuilder result = new StringBuilder();

        foreach (string p in parts)
        {
            if (string.IsNullOrEmpty(p))
            {
                result.Append(' ');
            }
            else if (p.Length == 2 && char.IsDigit(p[0]) && char.IsDigit(p[1]))
            {
                int row = int.Parse(p[0].ToString()) - 1;
                int col = int.Parse(p[1].ToString()) - 1;
                result.Append(square[row, col]);
            }
        }

        return result.ToString();
    }

    // === Основная программа ===
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write(\"Введите текст для шифрования: \");
        string input = Console.ReadLine();

        string encrypted = Encrypt(input);

        // Сохранение в файл
        File.WriteAllText(\"encrypted.txt\", encrypted);
        Console.WriteLine(\"\\nЗашифрованный текст сохранён в файл encrypted.txt\");

        // Чтение из файла и расшифровка
        string cipherText = File.ReadAllText(\"encrypted.txt\");
        string decrypted = Decrypt(cipherText);

        Console.WriteLine($\"\\nЗашифрованный текст: {encrypted}\");
        Console.WriteLine($\"Расшифрованный текст: {decrypted}\");    }
}
