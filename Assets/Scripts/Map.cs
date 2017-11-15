using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

public class Map : MonoBehaviour
{

    // マップ情報
    public int x_mass, y_mass;
    public GameObject[,] FieldBlocks;
    public GameObject cursor;

    // マップ上のユニット情報
    public GameObject selectedUnit;
    public List<GameObject> allyUnitList = new List<GameObject>();
    public List<GameObject> enemyUnitList = new List<GameObject>();

    public GameObject fighterPrefab;
    public GameObject sagePrefab;
    public GameObject piratesPrefab;
    public GameObject ninjaPrefab;
    public GameObject singerPrefab;
    public GameObject arcangelPrefab;
    public GameObject healerPrefab;

    public GameObject block_kusaPrefab;
    

    public void positioningBlocks()
    {

        FieldBlocks = new GameObject[x_mass * 2, y_mass * 2];

        for (int x = 0; x < x_mass * 2; x++)
        {
            for (int y = 0; y < y_mass * 2; y++)
            {
                GameObject block = block_kusaPrefab;
                Vector3 position = new Vector3(x - y, y_mass - y / 2.0f - x / 2.0f, 0);

                FieldBlocks[x, y] = Instantiate(block, position, transform.rotation);
                FieldBlocks[x, y].GetComponent<FieldBlock>().position[0] = x;
                FieldBlocks[x, y].GetComponent<FieldBlock>().position[1] = y;

                FieldBlocks[x, y].name = x + "_" + y + "_kusa";
                int distance = (abs(x) + abs(y));
                FieldBlocks[x, y].GetComponent<SpriteRenderer>().sortingOrder = distance;

                Debug.Log(FieldBlocks[x, y]);
            }
        }
        
    }


    public void positioningAllyUnits()
    {
        allyUnitList = new List<GameObject>();
        

        allyUnitList.Add(Instantiate(ninjaPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[allyUnitList.Count - 1].GetComponent<Unit>().init(5, 2, 1, new Rin_HN());
        allyUnitList.Add(Instantiate(singerPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[allyUnitList.Count - 1].GetComponent<Unit>().init(2, 3, 1, new Hanayo_LB());
        allyUnitList.Add(Instantiate(healerPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[allyUnitList.Count - 1].GetComponent<Unit>().init(3, 2, 1, new Riko_SN());
        allyUnitList.Add(Instantiate(arcangelPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[allyUnitList.Count - 1].GetComponent<Unit>().init(4, 3, 1, new Yohane_JA());

        allyUnitList.Add(Instantiate(piratesPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[allyUnitList.Count - 1].GetComponent<Unit>().init(4, 2, 1, new Eli_DS());
        allyUnitList.Add(Instantiate(fighterPrefab, new Vector3(0, 0, 0), transform.rotation));
        allyUnitList[allyUnitList.Count - 1].GetComponent<Unit>().init(3, 3, 1, new Kanan_TT());
    }


    public void positioningEnemyUnits()
    {
        enemyUnitList = new List<GameObject>();

        enemyUnitList.Add( Instantiate(fighterPrefab, new Vector3(0, 0, 0), transform.rotation));
        enemyUnitList[0].GetComponent<Unit>().init(5, 5, -1, new Enemy1_Smile());
        enemyUnitList.Add(Instantiate(fighterPrefab, new Vector3(0, 0, 0), transform.rotation));
        enemyUnitList[1].GetComponent<Unit>().init(8, 5, -1, new Enemy1_Smile());
        enemyUnitList.Add(Instantiate(fighterPrefab, new Vector3(0, 0, 0), transform.rotation));
        enemyUnitList[2].GetComponent<Unit>().init(11, 8, -1, new Enemy1_Smile());
        
    }


    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

}
