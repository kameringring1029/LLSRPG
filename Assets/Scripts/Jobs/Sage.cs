using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using General;

public class Sage : Unit {

    public GameObject explosionPrefab;

    public override void targetAttack(GameObject targetUnit)
    {
        int damage = unitInfo.attack_magic[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_magic[1];
        targetUnit.GetComponent<Unit>().beDamaged(damage, gameObject);

        int spritevector = (targetUnit.transform.position.x > transform.position.x) ? 1 : -1;
        changeSpriteFlip(spritevector);

        Instantiate(explosionPrefab, targetUnit.transform.position, transform.rotation);

        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
        deleteReachArea();
    }

    public override int getAttackDamage(GameObject targetUnit)
    {
        int damage = unitInfo.attack_magic[1]
        - targetUnit.GetComponent<Unit>().unitInfo.guard_magic[1];
        return damage;
    }
}
