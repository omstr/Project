using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Q7 : MonoBehaviour
{
    public int randNum;
    public static int A, B, C;
    public static List<char> listOfOs = new List<char>(); // HIDE
    
    public static void InitialiseCAndB(int input)
    {
        Console.Write("How many times? ");
        A = input;
        C = A / 2;
        B = 1;
    }
    public static void Output1()
    {
        //int count;
        for(int count = 1; count <= C; count++)
        {
            Console.Write(" ");
            //listOfOs.Add(" "); // HIDE
        }
    }
    public static void Output2()
    {
        
        for (int count = 1; count <= B; count++)
        {
            Console.Write("O");
            char o = 'O'; // HIDE
            listOfOs.Add('O'); // HIDE
        }
        Console.WriteLine();
    }
    public static void Adjust()
    {
        C = C - 1;
        B = B + 2;
    }

    public void Main()
    {
        randNum = Random.Range(3, 8);
        InitialiseCAndB(randNum);
        do
        {
            Output1();
            Output2();
            Adjust();

        } while (B < A);
        Console.ReadLine();

    }
}
