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
    public enum PRODUCE_TYPE { Not, GMary, WClover, Carrot, Mikan, SBerry }

    public PRODUCE_TYPE type { get; private set; }
    public NogyoItem.NogyoItemGroup group { get; private set; }
    public PRODUCE_STATE status { get; private set; }
    public NogyoItem water { get; private set; }
    public NogyoItem chemi { get; private set; }

    public Produce(PRODUCE_TYPE type, NogyoItem.NogyoItemGroup group)
    {
        this.type = type;
        this.group = group;
        status = PRODUCE_STATE.Seed;
    }
    
    /*
     * 状態の進行
     */
    public PRODUCE_STATE proceedState()
    {
        if(chemi != null && status < PRODUCE_STATE.Dead)
        {
            status = status + 2;
        }
        else
        {
            status++;
        }
        return status;
    }

    /*
     * 散水
     */
    public void watering(NogyoItem water)
    {
        this.water = water;
    }

    /*
     * 施肥
     */
    public void fertilize(NogyoItem chemi)
    {
        this.chemi = chemi;
    }

    /*
     * 一日の終り
     */
    public PRODUCE_STATE endDay()
    {
        proceedState();

        if(status != PRODUCE_STATE.Vanish && water == null)
        {
            status = PRODUCE_STATE.Dead;
        }

        water = null;
        chemi = null;

        return status;
    }

}
