using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WeiboImageBackend.Models
{
    public class WeiboImageRepo
    {
        private static readonly WeiboImageRepo _instance = new WeiboImageRepo();
        private string[] _urlSet = new string[] { };
        private List<int> _selectedSet = new List<int>();
        public static WeiboImageRepo Instance
        {
            get { return _instance; }
        }

        public void Initialize()
        {
            var modelFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/input.txt");
            _urlSet = File.ReadAllLines(modelFileName);
            _urlSet = _urlSet.OrderBy(x => x).ToArray();
        }

        public List<string> ExtractImages(int size)
        {
            if (size > 100 || size <= 0)
            {
                size = 100;
            }

            int sectionCount = _urlSet.Length / 100;
            int index = 0;
            bool added = false;
            for (int i = 0; i < 10; ++i)
            {
                int rand = new Random().Next(0, sectionCount);
                if (_selectedSet.Contains(rand))
                {
                    continue;
                }
                else
                {
                    _selectedSet.Add(rand);
                    added = true;
                    index = rand;
                }
            }

            if (added)
            {
                if (_selectedSet.Count > 100)
                {
                    _selectedSet.RemoveAt(0);
                }
            }

            return _urlSet.Skip(index * 100).Take(size).ToList();
        }
    }
}