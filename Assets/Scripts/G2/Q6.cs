using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q6 : MonoBehaviour
{
    public static int Algorithm(string input1, string input2)
    {
        int x, y, total, counter;
        //input1 = Console.ReadLine(); // HIDE 
        //input2 = Console.ReadLine(); // HIDE

        x = int.Parse(input1);
        y = int.Parse(input2);
        total = x;

        for(counter = 1; counter <= y - 1; counter++)
        {
            total = total * x;
        }
        Console.WriteLine(total);
        Console.ReadLine();
        return total; 
    }
}
