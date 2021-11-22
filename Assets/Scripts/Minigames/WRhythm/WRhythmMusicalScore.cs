using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRhythmMusicalScore
{
    public float spansec { private set; get; }
    public int[] score { private set; get; }

    public WRhythmMusicalScore(int id)
    {
        switch (id)
        {
            case 1:
                spansec = 0.5f;
                score = new int[] { 0, 0, 1, 2, 1 , 2, 1, 2, 1, 2, 4, 1, 1, 1 };
                break;
            case 2:
                break;
        }
    }

}


