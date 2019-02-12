using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Adding in random
using Random = UnityEngine.Random;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject[] spawnPoint;

    public float asteriodVelocity;

    public List<GameObject> enemies;

    public float enemySpeed;
    public float enemyShipRotation;

    public List<GameObject> activeEnemies;
    public bool removingEnemies;
    public int maxNumberOfActiveObstacles;

    public GameObject player;
    public GameObject deathAreaPrefab;

    public int playerLives;
    IEnumerator coroutine;
    //called before the first frame
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = new List<GameObject>();
        removingEnemies = false;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCommand();
        for (int i = 0; i < maxNumberOfActiveObstacles; i++)
            AddEnemy();

        coroutine = WaitForGameEnd(6);

        if (playerLives < 1)
            StartCoroutine(coroutine);
    }
    void KeyCommand()
    {
        //Quits the game when standalone is active
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
    void AddEnemy()
    {
        if (activeEnemies.Count < maxNumberOfActiveObstacles)
        {
            //Determining player spawn point
            int id = Random.Range(0, spawnPoint.Length);
            GameObject point = spawnPoint[id];

            //Determining which enemy to spawn
            GameObject enemy = enemies[Random.Range(0, enemies.Count)];

            //Instantiate an enemy
            GameObject enemyInstance = Instantiate<GameObject>(enemy, point.transform.position, Quaternion.identity);

            if(enemyInstance.GetComponent<Asteroid>() != null)
            {
                Vector2 directionVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                directionVector.Normalize();
                enemyInstance.GetComponent<Asteroid>().direction = directionVector;
            }
            //Add to enemy list
            activeEnemies.Add(enemyInstance);
        }
    }
    public void RemoveEnemies()
    {
        removingEnemies = true;
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            Destroy(activeEnemies[i]);
        }
        activeEnemies.Clear();
        removingEnemies = false;
    }
    IEnumerator WaitForGameEnd(int time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
        Debug.Log("Game Over, thank you for playing");
    }
}
