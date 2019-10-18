using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NogyoGeneral;

/*
 * 農園の状況管理クラス
 */
public class BalconyState
{
    public enum BALCONY{Balcony1, Balcony2, Proof }

    public List<int[]> plantpos { get; private set; }
    public Dictionary<int, Produce> produces { get; private set; }

    public BalconyState(List<int[]> plantpos)
    {
        produces = new Dictionary<int, Produce>();
        
        this.plantpos = plantpos;
    }

    /* 作物を植える */
    public void plantProduce(int position, Produce.PRODUCE_TYPE type, NogyoItem.NogyoItemGroup group)
    {
        produces.Add(position, new Produce(type, group));
    }

    /* 作物の状態を進める */
    public void proceedProduceState(int position)
    {
        if (produces.ContainsKey(position))
        {
            // proceedstate実行、Vanishなら作物を消す
            if (produces[position].endDay() == Produce.PRODUCE_STATE.Vanish)
            {
                removeProduce(position);
            }
        }

    }

    /* 作物を取り除く */
    public void removeProduce(int position)
    {
        produces.Remove(position);
        Debug.Log("remove:" + position);
    }

    /* 作物の収穫 */
    public Produce harvestProduce(int position)
    {
        if(produces[position].status == Produce.PRODUCE_STATE.Harvest)
        {
            Produce prod = produces[position];
            removeProduce(position);
            return prod; 
        }
        else
        {
            return null;
        }

    }
        


}
