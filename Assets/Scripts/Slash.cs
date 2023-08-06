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

}
