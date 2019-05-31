
using General;


/*
 * Aqoursユニットのステータス定義
 * 継承元はInformationStatusParent.cs
 */


namespace Information
{
    public class Chika_GF : statusTable
    {

        public override string graphic_id() { return "Chika-GF"; }
        public override string job_id() { return "Pirates"; }
        public override string name() { return "チカ"; }
        public override string job() { return "けんし"; }
        public override string subname() { return "ぐらぶる"; }
        public override int level() { return 18; }
        public override int movable() { return 3; }
        public override int reach() { return 1; }
        public override int hp() { return 25; }
        public override int attack_phy() { return 6; }
        public override int guard_phy() { return 7; }
        public override int attack_magic() { return 14; }
        public override int guard_magic() { return 13; }
        public override int agility() { return 9; }
        public override int luck() { return 18; }

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
        public override int agility(){ return 9; }
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
        public override int agility() { return 18; }
        public override int luck() { return 11; }


        public override MOVETYPE movetype() { return MOVETYPE.SWIM; }
    }

    public class You_GF : statusTable
    {

        public override string graphic_id() { return "You-GF"; }
        public override string job_id() { return "Sage"; }
        public override string name() { return "ヨウ"; }
        public override string job() { return "ガンナー"; }
        public override string subname() { return "ぐらぶる"; }
        public override int level() { return 18; }
        public override int movable() { return 6; }
        public override int reach() { return 2; }
        public override int hp() { return 14; }
        public override int attack_phy() { return 2; }
        public override int guard_phy() { return 6; }
        public override int attack_magic() { return 10; }
        public override int guard_magic() { return 14; }
        public override int agility() { return 17; }
        public override int luck() { return 1; }

        public override MOVETYPE movetype() { return MOVETYPE.SWIM; }

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
        public override int reach() { return 2; }
        public override int hp() { return 14; }
        public override int attack_phy() { return 2; }
        public override int guard_phy() { return 6; }
        public override int attack_magic() { return 10; }
        public override int guard_magic() { return 14; }
        public override int agility() { return 17; }
        public override int luck() { return 1; }

        public override MOVETYPE movetype() { return MOVETYPE.FLY; }

    }


}