using System.Collections;
using System.Collections.Generic;


/*
 * enumの定義とか
 */

namespace General
{
    public enum WHOLEMODE { SELECTMODE, SELECTUNIT, GAME, ROOM, OTHER}

    public enum UNITGROUP { MUSE, AQOURS, RIVAL, OTHERS, ENEMY}

    public enum CAMP { ALLY, ENEMY, GAMEMASTER }
    public enum SCENE { MAP_SELECT, MAIN, UNIT_SELECT_MOVETO, UNIT_MENU, UNIT_SELECT_TARGET, UNIT_ACTION_FORECAST, GAME_INEFFECT, STORY };


    public enum ACTION { ATTACK, HEAL, REACTION, WAIT }
    public enum MOVETYPE { WALK, FLY, SWIM }
    public enum GROUNDTYPE { NORMAL, HIGH, UNMOVABLE, SEA }

    public enum STORYACTION { DISAPP, APPEAR, TALK, FINE, DROWN}
}
