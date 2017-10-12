using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneBanner : MonoBehaviour {

    private GameMgr.SCENE nextScene;

	// Use this for initialization
	void Start () {

	}

    public void activate(GameMgr.SCENE scene)
    {
        nextScene = scene;

        switch (scene)
        {
            case GameMgr.SCENE.ALLY_MAIN:
                // Resources/Bannerフォルダからグラをロード
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Banner/" + "AllyTurn");
                break;

            case GameMgr.SCENE.ENEMY_MAIN:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Banner/" + "EnemyTurn");
                break;

            case GameMgr.SCENE.GAME_RESULT:
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Banner/" + "YouWin");
                break;
        }


        // ユーザ操作不能にし、バナー表示⇒消失⇒シーン変更
        GameObject.Find("Main Camera").GetComponent<GameMgr>().setGameScene(GameMgr.SCENE.GAME_INEFFECT);
        gameObject.SetActive(true);
        StartCoroutine("inactiveAfterTimer");
    }


    //--- 時間後にバナー消失⇒シーン移行 ---//
    IEnumerator inactiveAfterTimer()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<GameMgr>().setGameScene(nextScene);
    }

}
