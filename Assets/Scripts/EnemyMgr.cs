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
        yield return new WaitForSeconds(1f);

        while (GM.gameTurn == GameMgr.TURN.ENEMY)
        {

            GM.pushA();
            yield return new WaitForSeconds(0.5f);
           // while (GM.gameScene == GameMgr.SCENE.GAME_INEFFECT) { }


            GM.pushA();
            yield return new WaitForSeconds(0.5f);
           // while (GM.gameScene == GameMgr.SCENE.GAME_INEFFECT) { }

            GM.pushArrow(0, 1);
            yield return new WaitForSeconds(0.5f);
            //while (GM.gameScene == GameMgr.SCENE.GAME_INEFFECT) { }

            GM.pushA();
            yield return new WaitForSeconds(0.5f);
            //while (GM.gameScene == GameMgr.SCENE.GAME_INEFFECT) { }

        }



        yield return new WaitForSeconds(1);

       // GM.switchTurn();
    }
    
}
