using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public class Unit_info{
        public string name = "";
        public string job = "";
        public string subname = "";

        // ステータスは{基のステ, 修正後のステ}
        public int[] level = new int[2];
        public int[] movable = new int[2];
        public int[] reach = new int[2];
        public int[] hp = new int[2];
        public int[] attack_phy = new int[2];
        public int[] guard_phy = new int[2];
        public int[] attack_magic = new int[2];
        public int[] guard_magic = new int[2];
        public int[] luck = new int[2];

        public Unit_info(string name, string job, string subname,
                            int level, int movable, int reach, int hp, 
                            int attack_phy, int guard_phy, int attack_magic, int guard_magic, int luck)
        {
            this.name = name;
            this.job = job;
            this.subname = subname;

            // ステータスは{基のステ, 修正後のステ}
            for (int i=0; i<2; i++)
            {
                this.level[i] = level;
                this.movable[i] = movable;
                this.reach[i] = reach;
                this.hp[i] = hp;
                this.attack_phy[i] = attack_phy;
                this.guard_phy[i] = guard_phy;
                this.attack_magic[i] = attack_magic;
                this.guard_magic[i] = guard_magic;
                this.luck[i] = luck;
            }
        }

        public string outputInfo()
        {
            string outinfo =
                name + "\n" +
                " (" + subname + ")" + "\n" +
                job + "  Lv: " + level[1] + "\n" +
                "  HP: " + hp[1] + " / " + hp[0] + "\n\n" +
                "移動：" + movable[1] + "  射程：" + reach[1] + "\n" +
                "力：" + attack_phy[1] + "   防：" + guard_phy[1] + "\n" +
                "魔力：" + attack_magic[1] + "  魔防：" + guard_magic[1] + "\n" +
                "運：" + luck[1] + "";

            return outinfo;
        }
    }

    /*
    public class Kanan_TT : Unit_info
    {
        public Kanan_TT(string name, string job, string subname, 
                        int level, int movable, int reach, int hp, 
                        int attack_phy, int guard_phy, int attack_magic, int guard_magic, 
                        int luck) 
                    : base(name, job, subname, level, movable, reach, hp, attack_phy, guard_phy, attack_magic, guard_magic, luck)
        {


        }
    }
    */
    /*

    public class Kanan_TT:Unit_info
    {
        public string name = "カナン";
        public string job = "ファイター";
        public string subname = "トワイライトタイガー";
        public int[] level = { 18, 18 };
        public int[] movable = { 3, 3 };
        public int[] reach = { 1, 1 };
        public int[] hp = { 20, 20 };
        public int[] attack_phy = { 16, 16};
        public int[] guard_phy = { 15, 15 };
        public int[] attack_magic = { 0, 0 };
        public int[] guard_magic = { 5, 5 };
        public int[] luck = { 11, 11 };

        public Kanan_TT()
        {
            //Debug.Log(outputInfo());
        }

        public string outputInfo()
        {
            string outinfo =
                name + "\n" +
                " (" + subname + ")" + "\n" +
                job + "  Lv: " + level[1] + "\n" +
                "  HP: "+hp[1]+" / "+hp[0]+"\n\n" +
                "移動：" + movable[1] + "  射程：" + reach[1] + "\n" +
                "力：" + attack_phy[1] + "   防：" + guard_phy[1] + "\n" +
                "魔力：" + attack_magic[1] + "  魔防：" + guard_magic[1] + "\n" +
                "運：" + luck[1] + "";

            return outinfo; 
        }
    }

    */

    public class Enemy
    {
        public string name = "てき";
        public string job = "とり";
        public string subname = "スマイル";
        public int level = 1;
        public int movable = 2;
        public int reach = 1;
        public int hp = 17;
        public int attack_phy = 2;
        public int guard_phy = 7;
        public int attack_magic = 0;
        public int guard_magic = 7;
        public int luck = 1;

        public Enemy()
        {
            //Debug.Log(outputInfo());
        }

        public string outputInfo()
        {
            string outinfo =
                name + "\n" +
                " (" + subname + ")" + "\n" +
                job + "  Lv: " + level + "\n" +
                "  HP: 17 / 17\n\n" +
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
