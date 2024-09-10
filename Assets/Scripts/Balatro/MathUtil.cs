using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MathUtil
{
    public bool HasThreeOfOneAndTwoOfAnother<T>(T[] array)
    {
        // Check if the array has exactly 5 elements, as we need 3 of one value and 2 of another
        if (array == null || array.Length != 5)
            return false;

        // Dictionary to store the count of each value
        Dictionary<T, int> valueCounts = new Dictionary<T, int>();

        // Count the occurrences of each value
        foreach (T item in array)
        {
            if (valueCounts.ContainsKey(item))
            {
                valueCounts[item]++;
            }
            else
            {
                valueCounts[item] = 1;
            }
        }

        // Check if there are exactly 2 distinct values, one with count 3 and one with count 2
        bool hasThree = false;
        bool hasTwo = false;

        foreach (int count in valueCounts.Values)
        {
            if (count == 3)
            {
                hasThree = true;
            }
            else if (count == 2)
            {
                hasTwo = true;
            }
        }

        // Return true if both conditions are met
        return hasThree && hasTwo;
    }

    public bool AreAllElementsSame<T>(T[] array)
    {
        if (array == null || array.Length == 0)
            return false;  // Handle empty or null arrays as needed

        HashSet<T> uniqueElements = new HashSet<T>(array);
        return uniqueElements.Count == 1;
    }

    public int HowManyInList(int[] values, int i)
    {
       int count = values.Count(x => x == i);
       return count;
    }

    //public int KeyValuePair<T, T> GetEntryInDictionary<T>(Dictionary<T, T> myDictionary, int i)
    //{
        // foreach (var entry in myDictionary)
        // {
        //     Debug.Log("First entry: " + entry.Key + " -> " + entry.Value);
        //     break;  // Break after the first entry to simulate accessing the "first" item
        // }

        //return myDictionary.ElementAt[i];
    //}
}
