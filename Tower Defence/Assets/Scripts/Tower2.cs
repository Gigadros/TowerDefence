using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : Tower {

    float slowDuration = 0.5f, slowPower = 0.5f;

    private void Start()
    {
        // set base stats
        range = 5f;
        fireCooldown = 0.7f;
        fireCooldownLeft = 0f;
        power = 0.15f;
        speed = 15f;
        buildCost = 40;
        bulletPoolID = 0;
        // set sound variables
        volMin = 0.25f;
        volMax = 0.4f;
        pitchMin = 0.8f;
        pitchMax = 1f;
    }

    // Shoot the Enemy with a slow effect
    public override GameObject Shoot(Enemy e)
    {
        GameObject bulletGO = base.Shoot(e);
        if (bulletGO == null) return null;

        bulletGO.GetComponent<Bullet>().tower = this;
        e.GetComponent<Enemy>().SlowDown(slowDuration, slowPower);
        return bulletGO;
    }

}
