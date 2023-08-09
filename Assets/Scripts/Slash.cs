using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] private float mTimeSpan = 0.25f;
    [SerializeField] public Vector3 mAttackArea;
    private float mModifer;
    private float mTimer;

    public void Init(float modifer)
    {
        // Make sure that the attack is never invisible
        if (modifer < 1)
        {
            modifer = 1;
        }

        mModifer = modifer;
        transform.localScale = mAttackArea * mModifer;
    }

    // Update is called once per frame
    void Update()
    {
        mTimer += Time.deltaTime;

        if (mTimer > mTimeSpan)
        {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Player")
    //        return;

    //    // Pass the object to be killed by the game manager,
    //    // that way there is no spaghetti code. Keep it clean boi
    //    GameManager.instance.Killed(collision.gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            return;

        // Pass the object to be killed by the game manager,
        // that way there is no spaghetti code. Keep it clean boi
        GameManager.instance.Killed(collision.gameObject);
    }
}
