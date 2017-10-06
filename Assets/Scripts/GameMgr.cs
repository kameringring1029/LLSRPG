using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {

    public int x_mass, y_mass;
    public GameObject block_kusa;
    public GameObject unit;

    public GameObject[,] FieldBlocks { get; set; }

    private GameObject cursor;

	// Use this for initialization
	void Start () {

        FieldBlocks = new GameObject[x_mass*2,y_mass*2];
        createMap();
        positioningUnit();

        cursor = GameObject.Find("cursor");
        cursor.GetComponent<cursor>().moveCursorToAbs(4, 4);

    }

    // Update is called once per frame
    void Update () {
		
	}



    private void createMap()
    {
        for(int x=0; x<x_mass*2; x++)
        {
            for(int y=0; y<y_mass*2; y++)
            {
                GameObject block = block_kusa;
                Vector3 position = new Vector3(x - y, y_mass - y/2.0f - x/2.0f, 0);
                                
                FieldBlocks[x,y] = Instantiate(block, position, transform.rotation);

                FieldBlocks[x, y].name = x + "_" + y + "_kusa";
                int distance = (abs(x) + abs(y));
                FieldBlocks[x, y].GetComponent<SpriteRenderer>().sortingOrder = distance;
            }

        }
        
    }


    private void positioningUnit()
    {
        GameObject unit1 = Instantiate(unit, new Vector3(0,0,0), transform.rotation);
        unit1.GetComponent<Unit>().init(5, 4, 0);
        //GameObject unit2 = Instantiate(unit, new Vector3(0, 0, 0), transform.rotation);
        //unit2.GetComponent<Unit>().init(3, 5, 1);
    }


    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }

    private int vect(int a, int b)
    {
        int vecter = 1;
        if(a < x_mass || b < y_mass)
        {
            vecter = -1;
        }
        return vecter;
    }
}
