using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : EnemyBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Debug.Log("Created me");
    }
}
