using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

static public class LocalStorage 
{


    // ローカルストレージから読み込み
    static public void LoadLocalStageData()
    {
        string myJson = "[{\"NUM\":\"1\",\"TEXT\":\"HELLO\"},{\"NUM\":\"2\",\"TEXT\":\"BONJOUR\"},]";
        string dataName = "data.txt";

        Debug.Log(GetPath());
        string savePath = GetPath();

        // ディレクトリが無い場合はディレクトリを作成して保存
        if (!Directory.Exists(savePath))
        {
            // ディレクトリ作成
            Directory.CreateDirectory(savePath);
            // ローカルに保存
            SaveToLocal(myJson, dataName);
        }
        else
        {
            // ローカルからデータを取得
            string json = LoadFromLocal(dataName);
            Debug.Log(json);
        }
    }

    // 保存
    static public void SaveToLocal(string json, string dataName)
    {
        // jsonを保存
        File.WriteAllText(GetPath() + dataName, json);
    }

    // 取得
    static public string LoadFromLocal(string dataName)
    {
        // jsonを読み込み
        string json = File.ReadAllText(GetPath() + dataName);

        return json;
    }

    // パス取得
    static string GetPath()
    {
        return Application.persistentDataPath + "/AppData/";
    }
}