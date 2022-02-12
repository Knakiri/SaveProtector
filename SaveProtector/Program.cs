using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SaveProtector
{
    internal class Program
    {
       
        public static string SpliceText(string text, int lineLength)
        {
            return Regex.Replace(text, "(.{" + lineLength + "})", "$1" + Environment.NewLine);
        }
        
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "SaveProtector 1.0";
            Console.Write(@"
█▀▀ ▄▀▄ █ █ █▀▀ █▀█ █▀█ █▀█ ▀█▀ █▀▀ █▀▀ ▀█▀ █▀█ █▀█ 
▄██ █▀█ ▀▄▀ ██▄ █▀▀ █▀▄ █▄█  █  ██▄ █▄▄  █  █▄█ █▀▄ 
");
            bool flag = args.Length != 0;
            if (flag)
            {
                byte[] rawbytes = File.ReadAllBytes(args[0]);
                string str = Encoding.Default.GetString(rawbytes);
                var xdfaskd = SpliceText(str, 72);
                if (xdfaskd.Contains("save.dat"))
                {
                    Console.WriteLine("\nFound save.dat strings\n");
                    string random = RandomString(8);
                 
                   
                    Console.WriteLine("Generating random keys\n");
                    Console.WriteLine("Key generated:" + random + "\n");
                    string a = str.Replace("save.dat", random);
                    byte[] b = Encoding.Default.GetBytes(a);
                    string c = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Growtopia.exe";
                    string d = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\save.dat";
                    string e = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + random;

                    if (File.Exists(d))
                    {
                        File.Copy(d, e);
                    }

                    File.WriteAllBytes(c, b);
                    Console.WriteLine("Done!");
                }
                else
                {
                    Console.WriteLine("not found!");
                   
                }
               
            }
            Console.ReadLine();
        }
    }
}
