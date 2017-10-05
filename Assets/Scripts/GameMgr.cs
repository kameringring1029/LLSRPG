using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {

    public int x_mass, y_mass;
    public GameObject block_kusa;

	// Use this for initialization
	void Start () {
        createMap();
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

                int distance = (abs(x) + abs(y));
                block.GetComponent<SpriteRenderer>().sortingOrder = distance;

                block.name = "kusa_" + x + "_" + y;

                Instantiate(block, position, transform.rotation);

            }

        }
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
