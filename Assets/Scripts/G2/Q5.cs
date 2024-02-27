using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class Q5 : MonoBehaviour
{
    public static string Algorithm(int n) 
    {
        List<string> stringArr = new List<string>(); // HIDE
        Console.WriteLine();
        while(n > 0)
        {
            if(n % 2 == 0)
            {
                stringArr.Add("0"); // HIDE
                Console.WriteLine("0");
            }
            else
            {
                stringArr.Add("1"); // HIDE
                Console.WriteLine("1");
            }
            n = n / 2;
        }
        Console.WriteLine("Read upwards");
        Console.ReadLine();
        
        //ignore return
        string numberString = string.Join(",", stringArr); // HIDE
        return numberString; // HIDE
    }
}
