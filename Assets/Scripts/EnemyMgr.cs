﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;


/*
 * CPUの挙動
 * GameMgrからEnemyターンに呼び出し
 */

public class EnemyMgr : MonoBehaviour {

    GameMgr GM;
    Map map;

	// Use this for initialization
	void Start () {

        GM = gameObject.GetComponent<GameMgr>();
        map = gameObject.GetComponent<Map>();
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
        while (GM.gameScene == SCENE.GAME_INEFFECT) yield return new WaitForSeconds(0.5f);

        // 全ユニットが行動完了してGameMgrがターンを遷移するまで続く
        // ユニットごとにループするイメージ
        while (GM.gameTurn == CAMP.ENEMY)
        {

            // カーソルはユニットに置かれるからとりあえず動くためにAボタン押す
            GM.pushA();
            yield return new WaitForSeconds(0.5f);
            while (GM.gameScene == SCENE.GAME_INEFFECT) yield return new WaitForSeconds(0.5f);

            // とりあえず攻撃したいので敵ユニットに近づく
            GameObject nearestAlly = map.getNearAllyUnit(GM.selectedUnit);
            FieldBlock moveto = GM.selectedUnit.GetComponent<Unit>().getBlockToApproach(nearestAlly);
            GM.pushBlock(moveto.position[0], moveto.position[1]);
            yield return new WaitForSeconds(1f);
            while (GM.gameScene == SCENE.GAME_INEFFECT) yield return new WaitForSeconds(0.5f);


            // 敵ユニットに攻撃が届くか確認
            int distanceToTargetUnit =
                abs(nearestAlly.GetComponent<Unit>().nowPosition[0] - GM.selectedUnit.GetComponent<Unit>().nowPosition[0]) +
                abs(nearestAlly.GetComponent<Unit>().nowPosition[1] - GM.selectedUnit.GetComponent<Unit>().nowPosition[1]);
            

            // 攻撃が届けば殴る
            if (distanceToTargetUnit <= GM.selectedUnit.GetComponent<Unit>().unitInfo.reach[1]) {

                // Menuカーソルをこうげき(0)に移動
                if (GM.unitMenu.GetComponent<UnitMenu>().getSelectedAction() != 0)
                {
                    GM.pushArrow(0, 1);
                    yield return new WaitForSeconds(0.5f);
                }

                // こうげきを選択
                GM.pushA();
                yield return new WaitForSeconds(0.5f);
                while (GM.gameScene == SCENE.GAME_INEFFECT) yield return new WaitForSeconds(0.5f);

                // 敵ユニットを選択
                GM.pushBlock(nearestAlly.GetComponent<Unit>().nowPosition[0], nearestAlly.GetComponent<Unit>().nowPosition[1]);
                yield return new WaitForSeconds(0.5f);
                while (GM.gameScene == SCENE.GAME_INEFFECT) yield return new WaitForSeconds(0.5f);

                // 攻撃指定
                GM.pushA();
                yield return new WaitForSeconds(0.5f);
                while (GM.gameScene == SCENE.GAME_INEFFECT) yield return new WaitForSeconds(0.5f);
            }
            // 攻撃が届かない場合
            else
            {
                // たいきにMenuカーソルを移動
                if (GM.unitMenu.GetComponent<UnitMenu>().getSelectedAction() == 0)
                {
                    GM.pushArrow(0, 1);
                    yield return new WaitForSeconds(0.5f);
                }

                // 待機に決定
                GM.pushA();
                yield return new WaitForSeconds(0.5f);
                while (GM.gameScene == SCENE.GAME_INEFFECT) yield return new WaitForSeconds(0.5f);

            }
            
        }


        yield return new WaitForSeconds(0.5f);
        while (GM.gameScene == SCENE.GAME_INEFFECT) yield return new WaitForSeconds(0.5f);


    }


    //--- 移動先の特定  ---//
    // 現状最も近い敵ユニットを見つけ、自分の移動範囲内で近づくようにしてるよ
    // selectedunit: 移動するUnit
    // return: 移動先のBlock
    private FieldBlock detectMoveTo(GameObject selectedunit)
    {
        GameObject nearestAlly = map.getNearAllyUnit(selectedunit);

        return selectedunit.GetComponent<Unit>().getBlockToApproach(nearestAlly);
        
    }





    //--- 絶対値取得 ---//
    private int abs(int a)
    {
        if (a < 0) a = a * (-1);
        return a;
    }
}