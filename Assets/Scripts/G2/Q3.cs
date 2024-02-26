using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q3 : MonoBehaviour
{
    public static int Algorithm(int a, int b)
    {
        while(a > 2)
        {
            b = b + 2;
            a = a - 1;
        }
        
        return b;

    }
  
}
