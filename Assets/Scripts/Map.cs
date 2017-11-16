using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

public class Map : MonoBehaviour
{

    // マップ情報
    public int x_mass, y_mass;
    public GameObject[,] FieldBlocks;
    private int[,] mapstruct;
    public GameObject cursor;

    // マップ上のユニット情報
    public List<GameObject> allyUnitList = new List<GameObject>();
    public List<GameObject> enemyUnitList = new List<GameObject>();

    // ジョブのprefab
    public GameObject fighterPrefab;
    public GameObject sagePrefab;
    public GameObject piratesPrefab;
    public GameObject ninjaPrefab;
    public GameObject singerPrefab;
    public GameObject arcangelPrefab;
    public GameObject healerPrefab;

    // ブロックのprefab
    public GameObject block_kusaPrefab;
    public GameObject block_woodPrefab;
    public GameObject block_rengaPrefab;
    public GameObject block_michiPrefab;


    public void positioningBlocks()
    {
        // map情報の読み込み
        mapinfo map = JsonUtility.FromJson<mapinfo>(MapStruct1.mapStruct());
        x_mass = (int)System.Math.Sqrt(map.mapstruct.Length)/2;
        y_mass = (int)System.Math.Sqrt(map.mapstruct.Length)/2;

        Debug.Log(map.mapstruct.Length + " " + x_mass + " " + y_mass);


        // map作成
        FieldBlocks = new GameObject[x_mass * 2, y_mass * 2];

        for (int x = 0; x < x_mass * 2; x++)
        {
            for (int y = 0; y < y_mass * 2; y++)
            {                
                Vector3 position = new Vector3(x - y, y_mass - y / 2.0f - x / 2.0f, 0);

                FieldBlocks[x, y] = Instantiate(getBlockType(map.mapstruct[y*y_mass*2 + x]), position, transform.rotation);
                FieldBlocks[x, y].GetComponent<FieldBlock>().position[0] = x;
                FieldBlocks[x, y].GetComponent<FieldBlock>().position[1] = y;

                FieldBlocks[x, y].name = x + "_" + y + "_block";
                int distance = (abs(x) + abs(y));
                FieldBlocks[x, y].GetComponent<SpriteRenderer>().sortingOrder = distance;

                Debug.Log(FieldBlocks[x, y]);
            }
        }
        
    }


    private GameObject getBlockType(int typeno)
    {
        switch (typeno)
        {
            case 2:
                return block_woodPrefab;
            case 3:
                return block_rengaPrefab;
            case 4:
                return block_michiPrefab;
            default:
                return block_kusaPrefab;
        }
    }



    public void positioningAllyUnits()
    {
        randomAlly();
        randomAlly();
      //  positioningUnit(5, 2, ninjaPrefab, new Rin_HN(), 1);
      //  positioningUnit(2, 3, singerPrefab, new Hanayo_LB(), 1);
      //  positioningUnit(3, 2, healerPrefab, new Riko_SN(), 1);
      //  positioningUnit(4, 3, arcangelPrefab, new Yohane_JA(), 1);
      //  positioningUnit(4, 2, piratesPrefab, new Eli_DS(), 1);
      //  positioningUnit(3, 3, fighterPrefab, new Kanan_TT(), 1);
    }

    int prerand = -1;
    private void randomAlly()
    {
        int rand = Random.Range(0, 5);
        while(rand == prerand) Random.Range(0, 5);

        switch (rand)
        {
            case 0:
                positioningUnit(5, 2, ninjaPrefab, new Rin_HN(), 1);
                break;
            case 1:
                positioningUnit(2, 3, singerPrefab, new Hanayo_LB(), 1);
                break;
            case 2:
                positioningUnit(3, 2, healerPrefab, new Riko_SN(), 1);
                break;
            case 3:
                positioningUnit(4, 3, arcangelPrefab, new Yohane_JA(), 1);
                break;
            case 4:
                positioningUnit(3, 3, fighterPrefab, new Kanan_TT(), 1);
                break;
            case 5:
                positioningUnit(4, 2, piratesPrefab, new Eli_DS(), 1);
                break;
        }

        prerand = rand;
    }


    public void positioningEnemyUnits()
    {
        positioningUnit(11, 8, fighterPrefab, new Enemy1_Smile(), -1);
        positioningUnit(8, 5, sagePrefab, new Enemy1_Cool(), -1);
        positioningUnit(5, 5, fighterPrefab, new Enemy1_Smile(), -1);
        positioningUnit(3, 10, sagePrefab, new Enemy1_Cool(), -1);

    }




    //--- ユニット配置 ---//
    // x,y:初期位置
    // jobprefab:ジョブのぷれふぁぶ
    // status:ステータス
    // camp:1=味方 -1=敵
    private void positioningUnit(int x, int y, GameObject jobprefab, statusTable status, int camp)
    {
        List<GameObject> unitlist = new List<GameObject>();

        switch (camp)
        {
            case 1: unitlist = allyUnitList; break;
            case -1: unitlist = enemyUnitList; break;
        }

        unitlist.Add(Instantiate(jobprefab, new Vector3(0, 0, 0), transform.rotation));
        unitlist[unitlist.Count - 1].GetComponent<Unit>().init(x, y, camp, status);
    }


    public GameObject getNearAllyUnit(GameObject selectedunit)
    {
        GameObject nearestunit = allyUnitList[0];

        for(int i=1; i<allyUnitList.Count; i++)
        {
            int distanceA = abs(selectedunit.GetComponent<Unit>().nowPosition[0] - allyUnitList[i].GetComponent<Unit>().nowPosition[0])
                                + abs(selectedunit.GetComponent<Unit>().nowPosition[1] - allyUnitList[i].GetComponent<Unit>().nowPosition[1]);
            int distanceB = abs(selectedunit.GetComponent<Unit>().nowPosition[0] - nearestunit.GetComponent<Unit>().nowPosition[0])
                                + abs(selectedunit.GetComponent<Unit>().nowPosition[1] - nearestunit.GetComponent<Unit>().nowPosition[1]);

            if (distanceA < distanceB) nearestunit = allyUnitList[i];
        }

        return nearestunit;
    }




    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

}
