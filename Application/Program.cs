using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HashTableLib;

namespace console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = new StreamReader("prestuplenie-i-nakazanie.txt").ReadToEnd();

            Regex regex = new Regex("[А-Яа-яA-Za-z]+");
            MatchCollection matchCollection = regex.Matches(text);
            string[] words = new string[matchCollection.Count];
            for (int i = 0; i < matchCollection.Count; i++)
            {
                words[i] = matchCollection[i].Value;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            CommonDict(words);
            sw.Stop();
            Console.WriteLine($"Время: {sw.ElapsedMilliseconds}");

            sw.Restart();
            MineDict(words);
            sw.Stop();
            Console.WriteLine($"Время: {sw.ElapsedMilliseconds}");

            Console.ReadLine();
        }
        public static void CommonDict(string[] words)
        {
            System.Collections.Generic.Dictionary<string, int> dict = [];
            for (int i = 0; i < words.Length; i++)
            {
                if (dict.ContainsKey(words[i]))
                {
                    dict[words[i]]++;
                }
                else dict.Add(words[i], 1);
            }

            var smallDict = new System.Collections.Generic.Dictionary<string, int>();
            foreach (var item in dict)
            {
                if (item.Value > 27)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");
                    smallDict.Add(item.Key, item.Value);
                }
            }
            foreach (var item in smallDict)
            {
                dict.Remove(item.Key);
            }
        }
        public static void MineDict(string[] words)
        {

        }
    }
}
