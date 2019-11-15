using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivingMgr : MonoBehaviour
{
    PlayerData playerdata;


    void Start()
    {
        // プレイヤーデータ新規作成
        playerdata = PlayerData.getinstance();

    }


    void Update()
    {
        
    }

    public void openItemListMenu()
    {
        GameObject ItemList = Instantiate(Resources.Load<GameObject>("Prefab/Nogyo/ItemMenuPanel"), GameObject.Find("LivingCanvas").transform);
        ItemList.GetComponent<ItemMenu>().Activate();
    }

    public void startNogyo()
    {
        SceneManager.LoadScene("Nogyo");
    }

    public void onClickHelp()
    {
        GameObject[] helps = GameObject.FindGameObjectsWithTag("HelpWindow");
        foreach(GameObject help in helps)
        {
            switch (help.GetComponent<HelpWindow>().active)
            {
                case true:
                    help.GetComponent<HelpWindow>().Activate(false); break;
                case false:
                    help.GetComponent<HelpWindow>().Activate(true); break;
            }
        }
    }
}
