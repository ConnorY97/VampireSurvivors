using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] TMP_Text mKillsCountUI;
    [SerializeField] TMP_Text mHealthUI;
    [SerializeField] TMP_Text mLevelTimerUI;
    [SerializeField] private List<GameObject> mEnemies = new List<GameObject>();
    [SerializeField] private List<EnemyBase> mCurrentEnemies = new List<EnemyBase>();
    private float mLevel = 1;
    private float mLevelTimer = 0;
    private int mKills = 0;
    private float mHealth = 100;

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
        if (mKillsCountUI == null)
        {
            Debug.LogError("Missing kills count text component");
        }
        if (mHealthUI == null)
        {
            Debug.LogError("Missing Health count text component");
        }
        if (mLevelTimerUI == null)
        {
            Debug.LogError("Missing level timer UI text component");
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(mEnemies[0], new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0), Quaternion.identity);
            EnemyBase enemy = temp.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.Init(mLevel);
                mCurrentEnemies.Add(enemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        mLevelTimer = Time.time;
        // Once we hit 100 level up
        if (mLevelTimer == 100.0f)// || mCurrentEnemies.Count == 0)
        {
            SceneManager.LoadScene("EndScreen");
            mLevel++;
            mLevelTimer = 0;
        }

        //UI
        mKillsCountUI.text = mKills.ToString();
        mLevelTimerUI.text = mLevelTimer.ToString();

        // Check if game should exit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }

    public void Killed(GameObject gameObject)
    {
        EnemyBase enemy = gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            // This will stop the same object being removed more than once from the list
            // e.g. triggering twice by accident or some other reason
            bool remove = mCurrentEnemies.Contains(enemy);
            if (remove)
            {
                mCurrentEnemies.Remove(enemy);
                Destroy(gameObject);
                mKills++;
                if (mCurrentEnemies.Count == 0)
                {
                    Debug.Log("End of round");
                }
            }
        }
    }

    public void DebudKill()
    {
        for (int i = 0; i < mCurrentEnemies.Count; i++)
        {
            mCurrentEnemies.Clear();
        }
    }

    public void DealDamage(float damage)
    {
        mHealth -= damage;
        mHealthUI.text = mHealth.ToString();
    }
}
