using System;
using System.Linq;

namespace CodeWars
{
    class Program
    {
        static void Main(string[] args)
        {
            var ss = "ers";
            ss = Remove_char(ss);
            Console.WriteLine(ss);
            Console.ReadKey();
        }
        public static string Remove_char(string s)
        {
            return s.Substring(1, s.Length-2);
            return new string(s.ToCharArray().Skip(1).ToArray().Reverse().Skip(1).Reverse().ToArray());
            return string.Join("", s.Skip(1).Take(s.Length - 2));
        }
    }
}
