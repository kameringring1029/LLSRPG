using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{

    public class UnitStatus{

        public string graphic_id = "";

        public string name = "";
        public string job = "";
        public string subname = "";

        // ステータスは{基のステ, 修正後のステ}
        public int[] level = new int[2];
        public int[] reach = new int[2];
        public int[] hp = new int[2];
        public int[] attack_phy = new int[2];
        public int[] guard_phy = new int[2];
        public int[] attack_magic = new int[2];
        public int[] guard_magic = new int[2];
        public int[] luck = new int[2];


        public int[] movable = new int[2];
        public Unit.MOVETYPE movetype;


        public UnitStatus(statusTable status)
        {
            this.graphic_id = status.graphic_id();

            this.name = status.name();
            this.job = status.job();
            this.subname = status.subname();

            // ステータスは{基のステ, 修正後のステ}
            for (int i=0; i<2; i++)
            {
                this.level[i] = status.level();
                this.reach[i] = status.reach();
                this.hp[i] = status.hp();
                this.attack_phy[i] = status.attack_phy();
                this.guard_phy[i] = status.guard_phy();
                this.attack_magic[i] = status.attack_magic();
                this.guard_magic[i] = status.guard_magic();
                this.luck[i] = status.luck();
                
                this.movable[i] = status.movable();
            }

            movetype = status.movetype();
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
    

    public class statusTable{
        public virtual string graphic_id() { return ""; }
        public virtual string job_id() { return ""; }
        public virtual string name() { return ""; }
        public virtual string job() { return ""; }
        public virtual string subname() { return ""; }
        public virtual int level() { return 0; }
        public virtual int movable() { return 0; }
        public virtual int reach() { return 0; }
        public virtual int hp() { return 0; }
        public virtual int attack_phy() { return 0; }
        public virtual int guard_phy() { return 0; }
        public virtual int attack_magic() { return 0; }
        public virtual int guard_magic() { return 0; }
        public virtual int luck() { return 0; }
        public virtual Unit.MOVETYPE movetype() { return Unit.MOVETYPE.WALK; }
    }


    public class Enemy1_Smile : statusTable
    {
        public override string graphic_id() { return "Enemy1-Smile"; }
        public override string job_id() { return "Fighter"; }
        public override string name(){return "てき";}
        public override string job() { return "とり"; }
        public override string subname() { return "スマイル"; }
        public override int level() { return 1; }
        public override int movable() { return 2; }
        public override int reach() { return 1; }
        public override int hp() { return 17; }
        public override int attack_phy() { return 7; }
        public override int guard_phy() { return 7; }
        public override int attack_magic() { return 0; }
        public override int guard_magic() { return 7; }
        public override int luck() { return 1; }
        
    }

    public class Riko_SN : statusTable
    {

        public override string graphic_id() { return "Riko-SN"; }
        public override string job_id() { return "Healer"; }
        public override string name() { return "リコ"; }
        public override string job() { return "ナース"; }
        public override string subname() { return "職業編"; }
        public override int level() { return 18; }
        public override int movable() { return 3; }
        public override int reach() { return 1; }
        public override int hp() { return 25; }
        public override int attack_phy() { return 6; }
        public override int guard_phy() { return 7; }
        public override int attack_magic() { return 14; }
        public override int guard_magic() { return 13; }
        public override int luck() { return 18; }

    }



    public class Kanan_TT : statusTable
    {
        //"カナン", "ファイター", "トワイライトタイガー",18, 3, 1, 20, 16, 15, 0, 5, 11));

        public override string graphic_id() { return "Kanan-TT"; }
        public override string job_id() { return "Fighter"; }
        public override string name() { return "カナン"; }
        public override string job() { return "ファイター"; }
        public override string subname() { return "トワイライトタイガー"; }
        public override int level() { return 18; }
        public override int movable() { return 4; }
        public override int reach() { return 1; }
        public override int hp() { return 20; }
        public override int attack_phy() { return 16; }
        public override int guard_phy() { return 15; }
        public override int attack_magic() { return 0; }
        public override int guard_magic() { return 5; }
        public override int luck() { return 11; }


        public override Unit.MOVETYPE movetype() { return Unit.MOVETYPE.SWIM; }
    }


    public class Yohane_JA : statusTable
    {

        public override string graphic_id() { return "Yohane-JA"; }
        public override string job_id() { return "Sage"; }
        public override string name() { return "ヨハネ"; }
        public override string job() { return "堕天使"; }
        public override string subname() { return "ハロウィン編"; }
        public override int level() { return 18; }
        public override int movable() { return 6; }
        public override int reach() { return 3; }
        public override int hp() { return 14; }
        public override int attack_phy() { return 2; }
        public override int guard_phy() { return 6; }
        public override int attack_magic() { return 10; }
        public override int guard_magic() { return 14; }
        public override int luck() { return 1; }

        public override Unit.MOVETYPE movetype() { return Unit.MOVETYPE.FLY; }

    }


    public class Kotori_KY : statusTable
    {
        public override string graphic_id() { return "Kotori-KY"; }
        public override string job_id() { return "Sage"; }
        public override string name() { return "コトリ"; }
        public override string job() { return "賢者"; }
        public override string subname() { return "トワイライトタイガー"; }
        public override int level() { return 28; }
        public override int movable() { return 3; }
        public override int reach() { return 2; }
        public override int hp() { return 18; }
        public override int attack_phy() { return 5; }
        public override int guard_phy() { return 8; }
        public override int attack_magic() { return 24; }
        public override int guard_magic() { return 28; }
        public override int luck() { return 15; }

    }



    public class Eli_DS : statusTable
    {
        public override string graphic_id() { return "Eli-DS"; }
        public override string job_id() { return "Pirates"; }
        public override string name() { return "エリ"; }
        public override string job() { return "海賊"; }
        public override string subname() { return "Dancing Stars on Me"; }
        public override int level() { return 33; }
        public override int movable() { return 7; }
        public override int reach() { return 2; }
        public override int hp() { return 28; }
        public override int attack_phy() { return 25; }
        public override int guard_phy() { return 18; }
        public override int attack_magic() { return 14; }
        public override int guard_magic() { return 8; }
        public override int luck() { return 14; }


        public override Unit.MOVETYPE movetype() { return Unit.MOVETYPE.SWIM; }
    }

    public class Rin_HN : statusTable
    {
        public override string graphic_id() { return "Rin-HN"; }
        public override string job_id() { return "Pirates"; }
        public override string name() { return "リン"; }
        public override string job() { return "ニンジャ"; }
        public override string subname() { return "星空忍法"; }
        public override int level() { return 23; }
        public override int movable() { return 5; }
        public override int reach() { return 1; }
        public override int hp() { return 22; }
        public override int attack_phy() { return 20; }
        public override int guard_phy() { return 13; }
        public override int attack_magic() { return 4; }
        public override int guard_magic() { return 11; }
        public override int luck() { return 25; }

    }

    public class Rin_LB : statusTable
    {
        public override string graphic_id() { return "Rin-LB"; }
        public override string job_id() { return "Sage"; }
        public override string name() { return "リン"; }
        public override string job() { return "？"; }
        public override string subname() { return "Love wing bell"; }
        public override int level() { return 1; }
        public override int movable() { return 4; }
        public override int reach() { return 5; }
        public override int hp() { return 4; }
        public override int attack_phy() { return 0; }
        public override int guard_phy() { return 3; }
        public override int attack_magic() { return 26; }
        public override int guard_magic() { return 8; }
        public override int luck() { return 50; }

    }

    public class Hanayo_LB : statusTable
    {
        public override string graphic_id() { return "Hanayo-LB"; }
        public override string job_id() { return "Sage"; }
        public override string name() { return "ハナヨ"; }
        public override string job() { return "？"; }
        public override string subname() { return "Love wing bell"; }
        public override int level() { return 1; }
        public override int movable() { return 4; }
        public override int reach() { return 5; }
        public override int hp() { return 4; }
        public override int attack_phy() { return 0; }
        public override int guard_phy() { return 3; }
        public override int attack_magic() { return 26; }
        public override int guard_magic() { return 8; }
        public override int luck() { return 50; }

    }


    public class Kusa
    {
        public string type = "草";
        public string effect = "なし";
        public Unit.GROUNDTYPE groundtype = Unit.GROUNDTYPE.NORMAL;


        public Kusa(){
            //Debug.Log(outputInfo());

        }


    }

}
