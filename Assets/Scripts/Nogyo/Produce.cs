using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NogyoGeneral;

/*
 * 植えられた作物のクラス
 */
public class Produce
{
    public enum PRODUCE_STATE { Seed, Growth, Harvest, Dead, Vanish }
    public enum PRODUCE_TYPE { Not, GMary, WClover, Carrot }

    public PRODUCE_TYPE type { get; set; }


    public PRODUCE_STATE status { get; set; }


    public Produce(PRODUCE_TYPE type)
    {
        this.type = type;
        status = PRODUCE_STATE.Seed;
    }


    public PRODUCE_STATE proceedState()
    {
        status++;
        return status;
    }

}
