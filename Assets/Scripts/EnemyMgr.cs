using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : MonoBehaviour {

    GameMgr GM;

	// Use this for initialization
	void Start () {

        GM = gameObject.GetComponent<GameMgr>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startEnemyTurn()
    {
        StartCoroutine("enemyTurn");
    }

    IEnumerator enemyTurn()
    {
        yield return new WaitForSeconds(1);

        GM.switchTurn();
    }
}
