using System.Collections;
using System.Collections.Generic;


/*
 * enumの定義とか
 */

namespace General
{
    public enum WHOLEMODE { SELECTMODE, SELECTUNIT, GAME,ROOM, OTHER}

    public enum UNITGROUP { MUSE, AQOURS, RIVAL, OTHERS, ENEMY}

    public enum CAMP { ALLY, ENEMY, GAMEMASTER }
    public enum SCENE { MAIN, UNIT_SELECT_MOVETO, UNIT_MENU, UNIT_SELECT_TARGET, UNIT_ACTION_FORECAST, GAME_INEFFECT };


    public enum ACTION { ATTACK, HEAL, REACTION, WAIT }
    public enum MOVETYPE { WALK, FLY, SWIM }
    public enum GROUNDTYPE { NORMAL, HIGH, UNMOVABLE, SEA }

    
}
