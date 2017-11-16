using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Information;

[RequireComponent(typeof(Rigidbody))]

public class FieldBlock : MonoBehaviour {

    public GameObject GroundedUnit { get; set; }
    public int[] position = new int[2];
    
    public Unit.GROUNDTYPE blocktype;
    public BlockInfo blockInfo;
    

	// Use this for initialization
	void Start () {
        switch (blocktype)
        {
            case Unit.GROUNDTYPE.NORMAL:
                blockInfo = new Kusa();
                break;
            case Unit.GROUNDTYPE.UNMOVABLE:
                blockInfo = new Unmovable();
                break;
            case Unit.GROUNDTYPE.SEA:
                blockInfo = new Sea();
                break;
            case Unit.GROUNDTYPE.HIGH:
                blockInfo = new High();
                break;

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void onClick()
    {
        GameObject.Find("Main Camera").GetComponent<GameMgr>().pushBlock(position[0], position[1]);
    }


    public string outputInfo()
    {
        string outinfo =
            position[0] + "-" + position[1] +"\n" +
            "ブロック(" + blockInfo.type() + ")" + "\n" +
            "特殊効果；" + blockInfo.groundtype() + "\n";

        Debug.Log(outinfo);

        return outinfo;
    }
}
