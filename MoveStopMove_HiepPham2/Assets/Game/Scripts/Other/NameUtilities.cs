using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NameUtilities 
{
    private static List<string> names = new List<string>()
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P"
    };

    public static List<string> GetNames(int amount)
    {
        var list = names.OrderBy(d => System.Guid.NewGuid());
        return list.Take(amount).ToList();
    }

    public static string GetRandomName()
    {
        return names[Random.Range(0, names.Count)];
    }
}
