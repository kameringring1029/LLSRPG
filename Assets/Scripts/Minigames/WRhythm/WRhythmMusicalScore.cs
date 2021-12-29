using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRhythmMusicalScore
{
    public int number { private set; get; }
    public float spansec { private set; get; }
    public int[] score { private set; get; }

    public WRhythmMusicalScore(int id)
    {
        switch (id)
        {
            case 1:
                number = 1;
                spansec = 0.5f;
                //score = new int[] { 0, 0, 1, 2, 1 , 2, 1, 2, 1, 2, 4, 1, 1, 1 };
                score = new int[] { 0, 8, 8, 10, 8, 10, 8, 0, 15, 8, 8, 8, 0};
                break;
            case 2:
                number = 2;
                spansec = 0.5f;
                score = new int[] { 0, 2, 2, 0, 8, 2, 8, 2 ,0};
                break;
            case 3:
                number = 3;
                spansec = 0.5f;
                score = new int[] { 0, 2, 2, 0, 8, 2, 8, 2 ,0};
                break;
        }
    }

}


