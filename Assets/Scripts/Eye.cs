using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : EnemyBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player")
            return;

        // Send damage to the game manager
        GameManager.instance.DealDamage(mDamage);
    }
}
