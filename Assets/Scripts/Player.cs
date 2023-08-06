using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float mSpeed = 4.0f;
    private Animator mAnimator;
    private SpriteRenderer mRenderer;
    private int mAttackDelay = 0;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX, inputY, 0).normalized;
        movement *= Time.deltaTime;
        Debug.Log(movement.magnitude);
        transform.Translate(movement);

        //Animation
        if (inputX > 0)
        {
            mRenderer.flipX = false;
        }
        else
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
