using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;
using Information;

public class Garden : MonoBehaviour
{


    // マップ情報
    public int x_mass, y_mass;
    public GameObject[,] FieldBlocks;
    private int[,] mapstruct;
    public GameObject cursor;
    public GameObject mapframe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void positioningPlants()
    {
        // map情報の読み込み 

        x_mass = 3;
        y_mass = 1;

       // mapframe.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/mapframe/" + mapinformation.frame);
       

        // map作成
        FieldBlocks = new GameObject[x_mass, y_mass];

        for (int x = 0; x < x_mass; x++)
        {
            for (int y = 0; y < y_mass; y++)
            {
                setPlant(GameObject.Find("Balcony_kadan_Position").transform.position, x, y);
            }
        }

    }

    /*
     * baseP : x=0,y=0の基準位置
     * x,y : 設置する位置
     */
    public void setPlant(Vector3 baseP, int x, int y)
    {
        // 配置位置の指定
        Vector3 position = baseP + new Vector3((x - y) / 2.0f,  - y / 4.0f - x / 4.0f, 0);

        // ブロックの生成
        FieldBlocks[x, y] = Instantiate(Resources.Load<GameObject>("Prefab/Nogyo/gardenBlock"), position, transform.rotation);
        GameObject child = FieldBlocks[x, y].transform.GetChild(0).gameObject;
        FieldBlocks[x, y].name = x + "_" + y + "_block";

        // ブロックの相対位置をScript上に記録
        FieldBlocks[x, y].GetComponent<GardenBlock>().id = x + y * x;


        // Spriteの変更
        FieldBlocks[x, y].GetComponent<SpriteRenderer>().sprite = GardenInfoUtil.getGardenBlockType(x_mass, y_mass, x, y);
        //child.GetComponent<SpriteRenderer>().sprite = GardenInfoUtil.getGardenProduceSpriteByType(Random.Range(0, 2)); // int引数のrandom.rangeはmaxを含まない

        // map上の表示順の設定
        int distance = (abs(x) + abs(y));
        distance += 100;
        FieldBlocks[x, y].GetComponent<SpriteRenderer>().sortingOrder = distance;
        child.GetComponent<SpriteRenderer>().sortingOrder = distance + 1;
    }


    /*
     * 作物のSprite変更
     */
    public void renewProduce(int[] pos, Produce.PRODUCE_TYPE type, Produce.PRODUCE_STATE state)
    {
        if(state != Produce.PRODUCE_STATE.Vanish)
        {
            FieldBlocks[pos[0], pos[1]].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite
                = GardenInfoUtil.getGardenProduceSpriteByType(type, state);
        }
        else
        {
            FieldBlocks[pos[0], pos[1]].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite
            =null;
        }

    }


    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

}
