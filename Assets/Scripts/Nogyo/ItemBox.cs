using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemBox
{
    public NogyoItem[] items;

    public ItemBox()
    {
        items = new NogyoItem[0];
    }

    /*
     * アイテム数増減
     */
    public void changeItemNum(NogyoItem item, int num)
    {
        for(int i=0; i<items.Length; i++)
        {
            // すでに所持してたら
            if(items[i] == item)
            {
                if (items[i].qty == 99) return; // 99は無限判定

                items[i].qty += num;
                if (items[i].qty < 0) items[i].qty = 0;
                return;
            }

        }

        // 新規追加のとき
        List<NogyoItem> list = items.ToList<NogyoItem>();
        item.qty = num;
        list.Add(item);
        items = list.ToArray();
    }

    public void showItems()
    {
        // リストの出力
        foreach (NogyoItem item in items) // Dict型のforeach http://kan-kikuchi.hatenablog.com/entry/Dictionary_foreach
        {
            Debug.Log(item.name + ":" + item.qty);
        }
    }



    // アイテムグループでフィルタしたものを返却
    public ItemBox filterByItemgroup(NogyoItem.NogyoItemGroup[] group)
    {
        ItemBox itembox = new ItemBox();

        foreach(NogyoItem item in items)
        {
            // group[]ごとに判定
            foreach(NogyoItem.NogyoItemGroup g in group)
            {
                if (item.group == g)
                {
                    itembox.changeItemNum(item, item.qty);
                }
            }
        }
        return itembox;
    }
    // タブ表示用にfilter
    public ItemBox filterByItemgroupForTab(NogyoItem.NogyoItemGroup group)
    {
        ItemBox itembox = new ItemBox();

        switch (group)
        {
            case NogyoItem.NogyoItemGroup.Flower:
            case NogyoItem.NogyoItemGroup.Vegi:
            case NogyoItem.NogyoItemGroup.Fruit:
                itembox.items = filterByItemgroup(new NogyoItem.NogyoItemGroup[1] { group }).items;
                break;

            case NogyoItem.NogyoItemGroup.Null:
                itembox.items = items;
                break;

            default:
                NogyoItem.NogyoItemGroup[] g 
                    = new NogyoItem.NogyoItemGroup[4]{ NogyoItem.NogyoItemGroup.Soil, NogyoItem.NogyoItemGroup.Chemi, NogyoItem.NogyoItemGroup.Water, NogyoItem.NogyoItemGroup.Seed};
                itembox.items = filterByItemgroup(g).items;
                break;
        }

        return itembox;
    }


    /* idからitemのインスタンスを取得 */
    public NogyoItem getItemById(string id)
    {
        foreach(NogyoItem item in items)
        {
            if (item.id == id) return item;
        }
        return null; 
    }


    /* shop用のやつ */
    public void forShop()
    {
        changeItemNum(NogyoItemDB.getinstance().db["Chemi_Normal"], 3);
        changeItemNum(NogyoItemDB.getinstance().db["Seed_GMary"], 3);
        changeItemNum(NogyoItemDB.getinstance().db["Seed_WClover"], 3);
    }

}
