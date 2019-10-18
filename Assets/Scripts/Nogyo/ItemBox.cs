using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemBox:MonoBehaviour
{
    public Dictionary<NogyoItem, int> items { get; private set; }

    public ItemBox()
    {
        items = new Dictionary<NogyoItem, int>();
    }

    /*
     * アイテム数増減
     */
    public void changeItemNum(NogyoItem item, int num)
    {

        switch (items.ContainsKey(item))
        {
            case true:
                if (items[item] == 99) break; // 99は無限判定

                items[item] += num;
                if (items[item] == 0) items.Remove(item);
                break;
            case false:
                items.Add(item, num);
                break;
        }
        showItems();
    }

    public void showItems()
    {
        // リストの出力
        foreach (KeyValuePair<NogyoItem, int> item in items) // Dict型のforeach http://kan-kikuchi.hatenablog.com/entry/Dictionary_foreach
        {
            Debug.Log(item.Key.name + ":" + item.Value);
        }
    }



    // アイテムグループでフィルタしたものを返却
    public ItemBox filterByItemgroup(NogyoItem.NogyoItemGroup[] group)
    {
        ItemBox itembox = new ItemBox();

        foreach(KeyValuePair<NogyoItem, int> pair in items)
        {
            // group[]ごとに判定
            foreach(NogyoItem.NogyoItemGroup g in group)
            {
                if (pair.Key.group == g)
                {
                    itembox.items.Add(pair.Key, pair.Value);
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




}
