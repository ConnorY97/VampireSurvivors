using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private Transform mPlayerLocation;
    [SerializeField] protected float mSpeed = 1;
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

    // Update is called once per frame
    void Update()
    {
        float step = mSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mPlayerLocation.position, step);
    }
}
