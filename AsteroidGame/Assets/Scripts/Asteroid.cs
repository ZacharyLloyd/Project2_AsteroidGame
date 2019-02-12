using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector2 direction;

    public Vector3 originPosition;

    // Called before the first frame
    void Awake()
    {
        originPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * gameManager.instance.asteriodVelocity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player")
        {
            gameManager.instance.activeEnemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
       if (other.gameObject.tag == "Board")
        {
            Debug.Log("Asteroid in playing field");
        }
        Debug.Log(other);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameManager.instance.removingEnemies)
        {
            if (collision.gameObject.tag == "Board")
            {
                gameManager.instance.activeEnemies.Remove(this.gameObject);
                Debug.Log("Asteroid was destoryed");
                Destroy(this.gameObject);
            }
        }
    }
}
