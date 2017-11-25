using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;
using General;


/*
 * SRPGゲーム中のMap生成とMap情報管理用
 */

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
    public GameObject archerPrefab;

    // ブロックのprefab
    public GameObject block_kusaPrefab;
    public GameObject block_woodPrefab;
    public GameObject block_rengaPrefab;
    public GameObject block_michiPrefab;


    public void positioningBlocks()
    {
        // map情報の読み込み
        mapinfo map = JsonUtility.FromJson<mapinfo>(MapStruct2.mapStruct());
        x_mass = (int)System.Math.Sqrt(map.mapstruct.Length)/2;
        y_mass = (int)System.Math.Sqrt(map.mapstruct.Length)/2;

        Debug.Log(map.mapstruct.Length + " " + x_mass + " " + y_mass);


        // map作成
        FieldBlocks = new GameObject[x_mass * 2, y_mass * 2];

        for (int x = 0; x < x_mass * 2; x++)
        {
            for (int y = 0; y < y_mass * 2; y++)
            {                
                Vector3 position = new Vector3((x - y)/2.0f, y_mass - y / 4.0f - x / 4.0f, 0);

                FieldBlocks[x, y] = Instantiate(getBlockTypebyid(map.mapstruct[y*y_mass*2 + x]), position, transform.rotation);
                FieldBlocks[x, y].GetComponent<FieldBlock>().position[0] = x;
                FieldBlocks[x, y].GetComponent<FieldBlock>().position[1] = y;

                FieldBlocks[x, y].name = x + "_" + y + "_block";

                // map上の表示順の設定
                int distance = (abs(x) + abs(y));
                // 障害物の場合、Spriteが上に飛び出るのでSotingをUnitに合わせる
                if (FieldBlocks[x, y].GetComponent<FieldBlock>().blocktype == GROUNDTYPE.UNMOVABLE) distance += 100;
                FieldBlocks[x, y].GetComponent<SpriteRenderer>().sortingOrder = distance;


                //Debug.Log(FieldBlocks[x, y]);
            }
        }
        
    }


    private GameObject getBlockTypebyid(int typeno)
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



    public void positioningAllyUnits(int[] units)
    {
        if(units.Length == 0)
        {
            randomAlly();
            randomAlly();
            randomAlly();
        }
        else
        {
            for (int i=0;i<units.Length; i++)
            {
                setUnitFromId(units[i]);
            }

        }
        
    }

    //--- ランダムに味方ユニットを配置 ---//
    int prerand = -1;
    private void randomAlly()
    {
        int rand = Random.Range(0, 6);
        while(rand == prerand) rand = Random.Range(0, 6);
        prerand = rand;

        switch (rand)
        {
            case 0:
                rand = 2;break;
            case 1:
                rand = 5;break;
            case 2:
                rand = 8;break;
            case 3:
                rand = 11;break;
            case 4:
                rand = 12;break;
            case 5:
                rand = 15;break;
            case 6:
                rand = 4; break;
        }

        setUnitFromId(rand);

    }


    //--- 指定したunitidのユニットを配置 ---//
    private void setUnitFromId(int unitid)
    {

        switch (unitid)
        {
            case 5:
                positioningUnit(5, 2, ninjaPrefab, new Rin_HN(), CAMP.ALLY);
                break;
            case 8:
                positioningUnit(2, 3, singerPrefab, new Hanayo_LB(), CAMP.ALLY);
                break;
            case 11:
                positioningUnit(3, 2, healerPrefab, new Riko_SN(), CAMP.ALLY);
                break;
            case 15:
                positioningUnit(4, 3, arcangelPrefab, new Yohane_JA(), CAMP.ALLY);
                break;
            case 12:
                positioningUnit(3, 3, fighterPrefab, new Kanan_TT(), CAMP.ALLY);
                break;
            case 2:
                positioningUnit(4, 2, piratesPrefab, new Eli_DS(), CAMP.ALLY);
                break;
            case 4:
                positioningUnit(6, 2, archerPrefab, new Umi_DG(), CAMP.ALLY);
                break;
        }

    }


    public void positioningEnemyUnits()
    {
      //  positioningUnit(11, 8, fighterPrefab, new Enemy1_Smile(), GameMgr.CAMP.ENEMY);
        positioningUnit(8, 5, sagePrefab, new Enemy1_Cool(), CAMP.ENEMY);
        positioningUnit(6, 5, fighterPrefab, new Enemy1_Smile(), CAMP.ENEMY);
        positioningUnit(3, 10, sagePrefab, new Enemy1_Cool(), CAMP.ENEMY);

    }




    //--- ユニット配置 ---//
    // x,y:初期位置
    // jobprefab:ジョブのぷれふぁぶ
    // status:ステータス
    // camp:1=味方 -1=敵
    private void positioningUnit(int x, int y, GameObject jobprefab, statusTable status, CAMP camp)
    {
        List<GameObject> unitlist = new List<GameObject>();

        switch (camp)
        {
            case CAMP.ALLY: unitlist = allyUnitList; break;
            case CAMP.ENEMY: unitlist = enemyUnitList; break;
        }

        unitlist.Add(Instantiate(jobprefab, new Vector3(0, 0, 0), transform.rotation));
        unitlist[unitlist.Count - 1].GetComponent<Unit>().init(x, y, camp, status);
    }



    //--- 最も近い位置にいるAllyユニットを検索 ---//
    // selectedunit:検索対象
    // return: 最も近くにいるAllyユニット
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


    //--- ルーム用の設定 ---//
    // ユニットのランダム配置、移動可能範囲の不可視化、歩行スピードの減速
    public void settingforRoom()
    {
        for (int i = 0; i < allyUnitList.Count; i++)
        {
            Unit unit = allyUnitList[i].GetComponent<Unit>();

            unit.movableAreaPrefab.GetComponent<SpriteRenderer>().enabled = false;
            unit.staticMoveVelocity = 1;

            int randx = Random.Range(0, x_mass * 2);
            int randy = Random.Range(0, y_mass * 2);

            while (FieldBlocks[randx, randy].GetComponent<FieldBlock>().blocktype != GROUNDTYPE.NORMAL)
            {
                randx = Random.Range(0, x_mass * 2);
                randy = Random.Range(0, y_mass * 2);
            }

            unit.changePosition(randx, randy, false);
        }

        // カメラを引きに
        gameObject.GetComponent<Camera>().orthographicSize = 3;
    }

    //--- SRPGゲーム用の設定 ---//
    // 移動可能範囲の可視化、歩行スピードの設定
    public void settingforGame()
    {
        for (int i = 0; i < allyUnitList.Count; i++)
        {
            Unit unit = allyUnitList[i].GetComponent<Unit>();

            unit.movableAreaPrefab.GetComponent<SpriteRenderer>().enabled = true;
            unit.staticMoveVelocity = 2;

        }

        // カメラを寄りに
        gameObject.GetComponent<Camera>().orthographicSize = 3;
    }


    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

}
