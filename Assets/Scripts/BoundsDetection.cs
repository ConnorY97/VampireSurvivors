using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoundsDetection : MonoBehaviour
{
    public GameObject Player = null;
    public GameObject chunk = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            Vector3 dir = Player.transform.position - chunk.transform.position;
            Debug.Log(dir.normalized);

            // The player is moving right
            if (dir.x > 0.5f)
            {
                Chunk.instance.LeavingCurrentChunk(chunk, "Right");
            }
            // The player is moving left
            else if (dir.x < -0.5f)
            {
                Chunk.instance.LeavingCurrentChunk(chunk, "Left");
            }
            else if (dir.y > 0.5f)
            {
                Chunk.instance.LeavingCurrentChunk(chunk, "Top");
            }
            else if (dir.y < -0.5f)
            {
                Chunk.instance.LeavingCurrentChunk(chunk, "Bottom");
            }
        }
    }
}
