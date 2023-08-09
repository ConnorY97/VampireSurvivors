using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private Transform mPlayerLocation;
    [SerializeField] protected float mSpeed = 1.0f;
    [SerializeField] protected float mDamage = 10.0f;
    protected float mCurrentLevelModifier = 1.0f;
    protected float mHelth = 100.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        mPlayerLocation = player.GetComponent<Transform>();
        if (mPlayerLocation == null)
        {
            Debug.LogError("Missing player location in Eye prefab");
        }
    }

    public virtual void Init(float currentLevel)
    {
        // Modifying the base values with the current level
        // this will hopefully increase with time.
        mCurrentLevelModifier = currentLevel;
        mDamage *= mCurrentLevelModifier;
        mSpeed *= mCurrentLevelModifier;
        mHelth *= mCurrentLevelModifier;
    }

    // Update is called once per frame
    void Update()
    {
        float step = mSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mPlayerLocation.position, step);
    }
}
