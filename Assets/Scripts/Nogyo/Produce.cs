using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NogyoGeneral;

/*
 * 植えられた作物のクラス
 */
[System.Serializable]
public class Produce
{
    public enum PRODUCE_STATE { Seed, Growth, Harvest, Dead, Vanish }
    public enum PRODUCE_TYPE { Not, GMary, WClover, Carrot, Mikan, SBerry }

    public PRODUCE_TYPE type;
    public NogyoItem.NogyoItemGroup group;
    public PRODUCE_STATE status;
    public NogyoItem water;
    public NogyoItem chemi;

    public Produce(PRODUCE_TYPE type, NogyoItem.NogyoItemGroup group)
    {
        this.type = type;
        this.group = group;
        status = PRODUCE_STATE.Seed;

        this.chemi = new NogyoItem();
        this.water = new NogyoItem();
    }
    public Produce()
    {
        type = PRODUCE_TYPE.Not;
        group = NogyoItem.NogyoItemGroup.Null;
        status = PRODUCE_STATE.Vanish;
        chemi = new NogyoItem();
        water = new NogyoItem();
    }
    
    /*
     * 状態の進行
     */
    public PRODUCE_STATE proceedState()
    {
        if(chemi.id != "" && status < PRODUCE_STATE.Dead)
        {
            status = status + 3;
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

        if(status != PRODUCE_STATE.Vanish && water.id == "")
        {
            status = PRODUCE_STATE.Dead;
        }

        water = new NogyoItem();
        chemi = new NogyoItem();

        return status;
    }

}
