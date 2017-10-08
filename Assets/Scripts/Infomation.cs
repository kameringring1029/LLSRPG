using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{

    public class Kanan_TT
    {
        public string name = "カナン";
        public string job = "ファイター";
        public string subname = "トワイライトタイガー";
        public int level = 18;
        public int movable = 3;
        public int reach = 1;
        public int hp = 20;
        public int attack_phy = 16;
        public int guard_phy = 15;
        public int attack_magic = 0;
        public int guard_magic = 5;
        public int luck = 11;

        public Kanan_TT()
        {
            //Debug.Log(outputInfo());
        }

        public string outputInfo()
        {
            string outinfo =
                name + "\n" +
                " (" + subname + ")" + "\n" +
                job + "  Lv: " + level + "\n" +
                "  HP: 23 / 23\n\n" +
                "移動：" + movable + "  射程：" + reach + "\n" +
                "力：" + attack_phy + "   防：" + guard_phy + "\n" +
                "魔力：" + attack_magic + "  魔防：" + guard_magic + "\n" +
                "運：" + luck + "";

            return outinfo; 
        }
    }



    public class Kusa
    {
        public string type = "草";
        public string effect = "なし";

        public Kusa(){
            //Debug.Log(outputInfo());
        }

        public string outputInfo()
        {
            string outinfo =
                "ブロック(" + type + ")" + "\n" +
                "特殊効果；" + effect + "\n";

            return outinfo;
        }
    }

}
