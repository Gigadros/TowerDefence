using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : Tower {

    void Start()
    {
        // set base stats
        range = 8f;
        fireCooldown = 0.5f;
        fireCooldownLeft = 0f;
        power = 0.3f;
        speed = 15f;
        buildCost = 60;
        bulletPoolID = 0;
        // set sound variables
        volMin = 0.25f;
        volMax = 0.4f;
        pitchMin = 0.4f;
        pitchMax = 0.55f;
    }

    // Shoot the Enemy with a slow effect
    public override GameObject Shoot(Enemy e)
    {
        GameObject bulletGO = base.Shoot(e);
        if (bulletGO == null) return null;

        bulletGO.GetComponent<Bullet>().tower = this;
        return bulletGO;
    }
}
