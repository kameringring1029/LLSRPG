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
    mapinfo mapinformation;
    public int x_mass, y_mass;
    public GameObject[,] FieldBlocks;
    private int[,] mapstruct;
    public GameObject cursor;
    public GameObject mapframe;

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
        mapinformation = JsonUtility.FromJson<mapinfo>(MapStruct3.mapStruct());
        x_mass = (int)System.Math.Sqrt(mapinformation.mapstruct.Length)/2;
        y_mass = (int)System.Math.Sqrt(mapinformation.mapstruct.Length)/2;

        mapframe.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/mapframe/" + mapinformation.frame);

        Debug.Log(mapinformation.mapstruct.Length + " " + x_mass + " " + y_mass);


        // map作成
        FieldBlocks = new GameObject[x_mass * 2, y_mass * 2];

        for (int x = 0; x < x_mass * 2; x++)
        {
            for (int y = 0; y < y_mass * 2; y++)
            {                
                Vector3 position = new Vector3((x - y)/2.0f, y_mass - y / 4.0f - x / 4.0f, 0);

                FieldBlocks[x, y] = Instantiate(getBlockTypebyid(mapinformation.mapstruct[y*y_mass*2 + x]), position, transform.rotation);
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


    public void positioningEnemyUnits()
    {
        //  positioningUnit(11, 8, fighterPrefab, new Enemy1_Smile(), GameMgr.CAMP.ENEMY);
        positioningUnit(getNextUnitInitPosition(CAMP.ENEMY)[0], getNextUnitInitPosition(CAMP.ENEMY)[1], sagePrefab, new Enemy1_Cool(), CAMP.ENEMY);
        positioningUnit(getNextUnitInitPosition(CAMP.ENEMY)[0], getNextUnitInitPosition(CAMP.ENEMY)[1], fighterPrefab, new Enemy1_Smile(), CAMP.ENEMY);
        positioningUnit(getNextUnitInitPosition(CAMP.ENEMY)[0], getNextUnitInitPosition(CAMP.ENEMY)[1], sagePrefab, new Enemy1_Cool(), CAMP.ENEMY);

    }




    //--- ランダムに味方ユニットを配置 ---//
    List<int> prerandlist = new List<int>();
    private void randomAlly()
    {
        int rand = Random.Range(0, 6);
        bool only = false;
        

        // すでに設置されているユニットと被らない乱数を生成
        while (only == false && prerandlist.Count != 0)
        {
            rand = Random.Range(0, 6);
            only = true;

            foreach (int prerand in prerandlist)
            {
                if (prerand == rand) only = false;
            }
        }

        // 配置したユニットは再設置しないようにListで管理
        prerandlist.Add(rand);


        // 乱数からユニットIDを生成
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

        // ユニットIDからユニットを設置
        setUnitFromId(rand);

    }


    //--- 指定したunitidのユニットを配置 ---//
    private void setUnitFromId(int unitid)
    {
        int[] position = getNextUnitInitPosition(CAMP.ALLY);

        switch (unitid)
        {
            case 5:
                positioningUnit(position[0], position[1], ninjaPrefab, new Rin_HN(), CAMP.ALLY);
                break;
            case 8:
                positioningUnit(position[0], position[1], singerPrefab, new Hanayo_LB(), CAMP.ALLY);
                break;
            case 11:
                positioningUnit(position[0], position[1], healerPrefab, new Riko_SN(), CAMP.ALLY);
                break;
            case 15:
                positioningUnit(position[0], position[1], arcangelPrefab, new Yohane_JA(), CAMP.ALLY);
                break;
            case 12:
                positioningUnit(position[0], position[1], fighterPrefab, new Kanan_TT(), CAMP.ALLY);
                break;
            case 2:
                positioningUnit(position[0], position[1], piratesPrefab, new Eli_DS(), CAMP.ALLY);
                break;
            case 4:
                positioningUnit(position[0], position[1], archerPrefab, new Umi_DG(), CAMP.ALLY);
                break;
        }

    }


    //--- 次に配置するユニットの初期位置を取得 ---//
    // jsonから読み込んだmap情報に既定の初期位置が含まれる
    // camp: ユニットの陣営
    // return: 初期位置（X,Y）
    // 
    private int[] getNextUnitInitPosition(CAMP camp)
    {

        switch (camp)
        {
            case CAMP.ALLY:
                switch (allyUnitList.Count)
                {
                    case 0:
                        return mapinformation.ally1;
                    case 1:
                        return mapinformation.ally2;
                    case 2:
                        return mapinformation.ally3;
                }
                break;
            case CAMP.ENEMY:
                switch (enemyUnitList.Count)
                {
                    case 0:
                        return mapinformation.enemy1;
                    case 1:
                        return mapinformation.enemy2;
                    case 2:
                        return mapinformation.enemy3;
                }
                break;
        }

        return null;
    }




    //--- ユニット配置 ---//
    // x,y:初期位置
    // jobprefab:ジョブのぷれふぁぶ
    // status:ステータス
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

            int randx = Random.Range(0, x_mass * 2 - 1);
            int randy = Random.Range(0, y_mass * 2 - 1);

            while (FieldBlocks[randx, randy].GetComponent<FieldBlock>().blocktype != GROUNDTYPE.NORMAL)
            {
                randx = Random.Range(0, x_mass * 2 - 1);
                randy = Random.Range(0, y_mass * 2 - 1);
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
        gameObject.GetComponent<Camera>().orthographicSize = 1.5f;
    }


    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

}
