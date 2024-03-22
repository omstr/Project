using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q1Fib : MonoBehaviour
{
    public static int Fib(int n)
    {
        if (n <= 1)
            return n;
        else
            return Fib(n - 1) + Fib(n - 2);
    }

    static void Main()
    {
        
        Console.Write("Enter the value of n: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int result = Fib(n);
        Console.WriteLine($"The {n}-th Fibonacci number is: {result}");
    }
}
