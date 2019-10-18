﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NogyoMgr : MonoBehaviour
{
    PlayerData playerdata;
    Garden garden;

    public int selectedBlockId;

    public enum NOWMODE { Main, OpeningMenu}
    NOWMODE mode;

    GameObject openingItemMenu;

    // Start is called before the first frame update
    void Start()
    {
        mode = NOWMODE.Main;
        openingItemMenu = null;

        // プレイヤーデータ新規作成
        playerdata = PlayerData.getinstance();

        // プラントSpriteを配置
        garden = new Garden();
        garden.positioningPlants(3, 1);

        // Sprite更新
        renewBalcorySprites(BalconyState.BALCONY.Balcony1);
        selectPlant(0);
    }

    void Update()
    {
        // itemメニュー画面が開いているとき
        if (openingItemMenu)
        {
            //itemが指定されたら
            if (openingItemMenu.GetComponent<CareMenu>().selected != "")
            {
                // キャンセルじゃなかったら
                if (openingItemMenu.GetComponent<CareMenu>().selected != "END") 
                {
                    // アイテムIDからアイテム情報を取得
                    NogyoItem selecteditem = null; 
                   foreach (KeyValuePair<NogyoItem, int> pair in playerdata.itembox.items)
                   {
                      if (pair.Key.id == openingItemMenu.GetComponent<CareMenu>().selected) selecteditem = pair.Key;
                   }

                   // アイテム数を減少
                    playerdata.itembox.changeItemNum(selecteditem, -1);

                    // care種別で処理分岐
                    switch (openingItemMenu.GetComponent<CareMenu>().care)
                    {
                       case NogyoItem.NogyoItemGroup.Seed: // 種植え
                            playerdata.balconies[BalconyState.BALCONY.Balcony1].plantProduce(selectedBlockId, selecteditem.producetype, selecteditem.group);
                            renewBalcorySprites(BalconyState.BALCONY.Balcony1);
 
                            break;

                        case NogyoItem.NogyoItemGroup.Water: // 散水
                            // 作物に散水処理
                            if(playerdata.balconies[BalconyState.BALCONY.Balcony1].produces.ContainsKey(selectedBlockId))
                                playerdata.balconies[BalconyState.BALCONY.Balcony1].produces[selectedBlockId].watering(selecteditem);

                            // Spriteを更新
                            int[] pos = playerdata.balconies[BalconyState.BALCONY.Balcony1].plantpos[selectedBlockId];
                            garden.wateringProduce(pos, true);

                            break;

                        case NogyoItem.NogyoItemGroup.Chemi: // 肥料まき
                            // 作物に施肥処理
                            if (playerdata.balconies[BalconyState.BALCONY.Balcony1].produces.ContainsKey(selectedBlockId))
                                playerdata.balconies[BalconyState.BALCONY.Balcony1].produces[selectedBlockId].watering(selecteditem);
                            break;
                    }
               }

                // メニューを削除
                Destroy(openingItemMenu.transform.parent.gameObject);
                openingItemMenu = null;

                mode = NOWMODE.Main;
            }
        }
    }



    /*対象のBalconyすべての作物のSriteをユーザデータをもとに更新 */
    void renewBalcorySprites(BalconyState.BALCONY balconystr)
    {
        for (int i = 0; i < playerdata.balconies[balconystr].plantpos.Count; i++)
        {
            BalconyState balcony = playerdata.balconies[balconystr];
            if (balcony.produces.ContainsKey(i))
            {
                garden.renewProduce(balcony.plantpos[i], balcony.produces[i].type, balcony.produces[i].status);
            }
            else
            {
                garden.renewProduce(balcony.plantpos[i], Produce.PRODUCE_TYPE.Not, Produce.PRODUCE_STATE.Vanish);
            }
        }

        renewViewInfo();
    }

    /* プラント選択 */
    public void selectPlant(int selected)
    {
        selectedBlockId = selected;

        garden.renewCursor(playerdata.balconies[BalconyState.BALCONY.Balcony1].plantpos[selectedBlockId]);
        renewViewInfo();
    }


    /* 一日の終りだよ */
    void endDay()
    {
        // バルコニー内の全作物を成長、乾かす
        for (int i = 0; i < playerdata.balconies[BalconyState.BALCONY.Balcony1].plantpos.Count; i++)
        {
            playerdata.balconies[BalconyState.BALCONY.Balcony1].proceedProduceState(i);

            int[] pos = playerdata.balconies[BalconyState.BALCONY.Balcony1].plantpos[i];
            garden.wateringProduce(pos, false);
        }

        // Spite情報を更新
        renewBalcorySprites(BalconyState.BALCONY.Balcony1);
    }




    /* ViewPanelの情報更新 */
    void renewViewInfo()
    {
        /* 選択Blockのposition */
        int[] pos = playerdata.balconies[BalconyState.BALCONY.Balcony1].plantpos[selectedBlockId];

        Produce prod = null;
        //対象の作物が存在している場合
        if (playerdata.balconies[BalconyState.BALCONY.Balcony1].produces.ContainsKey( selectedBlockId ))
            prod = playerdata.balconies[BalconyState.BALCONY.Balcony1].produces[selectedBlockId];
        //更新
        GameObject.Find("ProduceViewPanel").GetComponent<ProduceView>().renew(garden.FieldBlocks[pos[0], pos[1]], prod);

    }


    
    /* おせわめにゅーひらく */
    void openCareMenu(NogyoItem.NogyoItemGroup care)
    {
        if(mode == NOWMODE.Main)
        {
            mode = NOWMODE.OpeningMenu;
            GameObject ItemMenuPanel = Instantiate(Resources.Load<GameObject>("Prefab/Nogyo/CareMenuPanel"), GameObject.Find("NogyoCanvas").transform);
            openingItemMenu = ItemMenuPanel.transform.GetChild(0).gameObject;
            openingItemMenu.GetComponent<CareMenu>().Activate(care, playerdata.itembox);
        }
        else
        {
            openingItemMenu.GetComponent<CareMenu>().cancel();
        }

    }
    public void openSeedMenu()
    {
        // 作物が植えられていない場合のみ
        if (!playerdata.balconies[BalconyState.BALCONY.Balcony1].produces.ContainsKey(selectedBlockId)) 
        {
            openCareMenu(NogyoItem.NogyoItemGroup.Seed);
        }
        //収穫
        else if (playerdata.balconies[BalconyState.BALCONY.Balcony1].produces[selectedBlockId].status == Produce.PRODUCE_STATE.Harvest) 
        {
            Produce harv = playerdata.balconies[BalconyState.BALCONY.Balcony1].harvestProduce(selectedBlockId);
            playerdata.itembox.changeItemNum(NogyoItemDB.getinstance().getItemFromPType(NogyoItem.NogyoItemGroup.Flower, harv.type), 1);
            renewBalcorySprites(BalconyState.BALCONY.Balcony1);
        }
    }
    public void openWaterMenu()
    {
        openCareMenu(NogyoItem.NogyoItemGroup.Water);
    }
    public void openChemiMenu()
    {
        openCareMenu(NogyoItem.NogyoItemGroup.Chemi);
    }

    public void openItemListMenu()
    {
        playerdata.openItemListMenu();
    }
}