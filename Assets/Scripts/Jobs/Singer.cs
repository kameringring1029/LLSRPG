using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;


public class Singer : Unit {

    public GameObject explosionPrefab;

    public override void targetAttack(GameObject targetUnit)
    {
        int damage = unitInfo.attack_phy[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_phy[1];
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);


        Instantiate(explosionPrefab, targetUnit.transform.position, transform.rotation);
        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);

        deleteReachArea();
    }


    public override void targetReaction(GameObject targetUnit)
    {

        int damage = 0;
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);
        targetUnit.GetComponent<Unit>().restoreActionRight();

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);
        
        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);

        deleteReachArea();
    }


    public override List<ACTION> getActionableList()
    {
        List<ACTION> actionlist = new List<ACTION>();
        actionlist.Add(ACTION.ATTACK);
        actionlist.Add(ACTION.REACTION);
        actionlist.Add(ACTION.WAIT);

        return actionlist;
    }
}
