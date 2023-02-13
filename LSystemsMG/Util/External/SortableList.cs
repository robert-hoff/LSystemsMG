using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LSystemsMG.Util.External
{
    class SortableItem : IComparable<SortableItem>
    {
        private int sortValue;
        private string name;

        public SortableItem(int sortValue, string name)
        {
            this.sortValue = sortValue;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{name}";
        }

        public int CompareTo(SortableItem other)
        {
            return sortValue - other.sortValue;
        }

        public static void TrialRun()
        {
            List<SortableItem> myList = new();
            myList.Add(new SortableItem(5, "I am 5"));
            myList.Add(new SortableItem(1, "I am 1"));
            myList.Add(new SortableItem(9, "I am 9"));
            myList.Sort();
            foreach (SortableItem item in myList)
            {
                Debug.WriteLine($"{item}");
            }
        }
    }
}

