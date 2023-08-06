using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Unsafe Singleton
    private static GameManager mInstance = null;
    public static GameManager instance => mInstance;
    //
    [SerializeField] TMP_Text mKillsCount;
    [SerializeField] private List<GameObject> mEnemies = new List<GameObject>();
    [SerializeField] private List<GameObject> mCurrentEnemies = new List<GameObject>();
    private int mLevel = 0;
    private float mLevelTimer = 0;
    private int mKills = 0;

    private void Awake()
    {
        // Initialize singleton
        if (mInstance == null)
        {
            mInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (mKillsCount == null)
        {
            Debug.LogError("Missing kills count text component");
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(mEnemies[0], new Vector3(Random.Range(-15, 15), Random.Range(-10, 10), 0), Quaternion.identity);
            mCurrentEnemies.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mLevelTimer = Time.deltaTime;
        // Once we hit 100 level up
        if (mLevelTimer == 100.0f || mCurrentEnemies.Count == 0)
        {
            SceneManager.LoadScene("EndScreen");
            mLevel++;
            mLevelTimer = 0;
        }

        //UI
        if (mKillsCount != null)
        {
            mKillsCount.text = mKills.ToString();
        }

        // Check if game should exit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }

    public void Killed(GameObject gameObject)
    {
        mCurrentEnemies.Remove(gameObject);
        Destroy(gameObject);
        mKills++;
    }

    public void DebudKill()
    {
        for (int i = 0; i < mCurrentEnemies.Count; i++)
        {
            mCurrentEnemies.Clear();
        }
    }
}
