using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float mSpeed = 4.0f;
    [SerializeField] private GameObject mSlash;
    [SerializeField] Transform mAttackPos;
    [SerializeField] private Rigidbody2D mRB;
    private Animator mAnimator;
    private SpriteRenderer mRenderer;
    [SerializeField] private float mAttackDelay = 1.0f;
    [SerializeField] private float mAttackTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        if (mAnimator == null)
        {
            Debug.LogError("Missing animator");
        }
        mRenderer = GetComponent<SpriteRenderer>();
        if (mRenderer == null)
        {
            Debug.LogError("Missing sprite renderer");
        }
        if (mAttackPos == null)
        {
            Debug.LogError("Missing attack Position");
        }
        mRB = GetComponent<Rigidbody2D>();
        if (mRB == null)
        {
            Debug.LogError("Missing rigidbody2D");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Timers
        mAttackTimer += Time.deltaTime;
        if (mAttackTimer > mAttackDelay )
        {
            // Create the slash slightly infrom of the Player
            GameObject attack = Instantiate(mSlash, mAttackPos.position, Quaternion.identity, transform);
            Slash slash = attack.GetComponent<Slash>();
            if (slash == null)
            {
                Debug.Log("Failed to get Slash from prefab");
            }
            slash.Init(1.0f);
            mAttackTimer = 0.0f;
        }
        // Movement
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX, inputY, 0).normalized;
        transform.position += movement * mSpeed * Time.deltaTime;

        //Animation
        if (inputX > 0)
        {
            mRenderer.flipX = false;
        }
        if (inputX < 0)
        {
            mRenderer.flipX = true;
        }

        if (movement != Vector3.zero)
        {
            mAnimator.SetInteger("AnimState", 1);
        }
        else
        {
            mAnimator.SetInteger("AnimState", 0);
        }


    }
}
