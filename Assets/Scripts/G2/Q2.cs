using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Q2 : MonoBehaviour
{ 
    public static int CalculateGCD(int a, int b)
    {
        
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    static void Main()
    {

        
        int num1 = Random.Range(1, 20);
        int num2 = Random.Range(1, 20);
        int result = CalculateGCD(num1, num2);
        
        
    }

}