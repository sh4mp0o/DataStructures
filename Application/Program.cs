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
        static void Main()
        {
            int n = 5;
            var oaTable = new OpenAddressHashTable<int, int>();
            for (int i = 0; i < n; i++)
            {
                oaTable.Add(new KeyValuePair<int, int>(i * 11, 0));
            }

            oaTable.Add(new KeyValuePair<int, int>(23, 0));

            oaTable.Remove(11);

            oaTable.Remove(22);

            oaTable.Remove(33);



            string text = new StreamReader("WarAndWorld.txt").ReadToEnd();     //prestuplenie-i-nakazanie.txt      annakarenina.txt        worldandwar

            Regex regex = new Regex("[А-Яа-яA-Za-z]+");
            MatchCollection matchCollection = regex.Matches(text);
            string[] words = new string[matchCollection.Count];

            for (int i = 0; i < matchCollection.Count; i++)
            {
                words[i] = matchCollection[i].Value;
            }

            long commonTime = 0;
            long mineTime = 0;
            for (int i = 0; i < 50; i++)
            {
                mineTime += OneTryMine(words);
                commonTime += OneTryCommon(words);
            }
            double k = (double)mineTime / (double)commonTime;

            Console.WriteLine($"common: {commonTime/50} mine: {mineTime/50} coefficient: {k}");
            Console.ReadLine();
            Main();
        }
        public static long OneTryCommon(string[] words)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            CommonDict(words);
            sw.Stop();

            long time = sw.ElapsedMilliseconds;

            return time;
        }
        public static long OneTryMine(string[] words)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            MineDict(words);
            sw.Stop();

            long time = sw.ElapsedMilliseconds;

            return time;
        }
        public static void MineDict(string[] words)
        {
            OpenAddressHashTable<string, int> maDict = new OpenAddressHashTable<string, int>();
            for (int i = 0; i < words.Length; i++)
            {
                if (maDict.ContainsKey(words[i]))
                {
                    maDict[words[i]]++;
                }
                else maDict.Add(words[i], 1);
            }

            var smallDict = new OpenAddressHashTable<string, int>();
            foreach (var item in maDict)
            {
                if (item.Value > 27)
                {
                    smallDict.Add(item.Key, item.Value);
                }
            }
            foreach (var item in smallDict)
            {
                maDict.Remove(item.Key);
            }

        }
        public static void CommonDict(string[] words)
        {
            System.Collections.Generic.Dictionary<string, int> dict = new System.Collections.Generic.Dictionary<string, int>();
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
                    smallDict.Add(item.Key, item.Value);
                }
            }
            foreach (var item in smallDict)
            {
                dict.Remove(item.Key);
            }
        }
    }
}
