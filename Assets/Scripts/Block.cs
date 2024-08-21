using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            Vector3 dir = collision.gameObject.transform.position - transform.position;
            Debug.Log(dir.normalized);

            // The player is moving right
            if (dir.x > 0.5f)
            {
                Chunk.instance.LeavingCurrentChunk(gameObject, "Right");
            }
            // The player is moving left
            else if (dir.x < -0.5f)
            {
                Chunk.instance.LeavingCurrentChunk(gameObject, "Left");
            }
            else if (dir.y > 0.5f)
            {
                Chunk.instance.LeavingCurrentChunk(gameObject, "Top");
            }
            else if (dir.y < -0.5f)
            {
                Chunk.instance.LeavingCurrentChunk(gameObject, "Bottom");
            }
        }
    }
}
