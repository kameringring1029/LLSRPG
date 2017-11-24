using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;


public class Healer : Unit {


    public GameObject aidPrefab;
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

    public override void targetHeal(GameObject targetUnit)
    {

        int heal = unitInfo.attack_magic[1]/2;
        targetUnit.GetComponent<Unit>().beHealed(heal, gameObject);

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        Instantiate(aidPrefab, targetUnit.transform.position, transform.rotation);


        gameObject.GetComponent<Animator>().SetBool("isHealing", true);

        deleteReachArea();
    }

    public override List<ACTION> getActionableList()
    {
        List<ACTION> actionlist = new List<ACTION>();
        actionlist.Add(ACTION.ATTACK);
        actionlist.Add(ACTION.HEAL);
        actionlist.Add(ACTION.WAIT);

        return actionlist;
    }
}
