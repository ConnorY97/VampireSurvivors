using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    [SerializeField] protected float mDamage = 10.0f;
    [SerializeField] protected float mLifeSpan = 0.25f;
    [SerializeField] protected Vector3 mAttackArea;
    protected float mModifier = 1.0f;
    protected float mTimer = 0;

    public virtual void Init(float modifier)
    {
        // Make sure that the attack is never invisible
        if (modifier < 1)
        {
            modifier = 1;
        }

        mModifier = modifier;
        transform.localScale = mAttackArea * mModifier;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        mTimer += Time.deltaTime;

        if (mTimer > mLifeSpan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            return;

        // Pass the object to be killed by the game manager,
        // that way there is no spaghetti code. Keep it clean boi
        GameManager.instance.Killed(collision.gameObject);
    }
}
