using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public Vector3 direction;

    public Vector3 originPosition;

    // Intialization
    void Awake()
    {
        direction = new Vector3(1, 0, 0);
        originPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.instance.player != null)
        {
            Follow(gameManager.instance.player);
        }
        else
        {
            Follow(gameManager.instance.deathAreaPrefab);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            gameManager.instance.activeEnemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameManager.instance.removingEnemies)
        {
            if(collision.gameObject.tag == "Board")
            {
                gameManager.instance.activeEnemies.Remove(this.gameObject);
                Debug.Log("Enemy was destroyed");
                Destroy(this.gameObject);
            }
        }
    }
    void Follow(GameObject target)
    {
        direction = new Vector3(1, 0, 0);
        transform.Translate(direction * Time.deltaTime * gameManager.instance.enemySpeed);
        //Targeting
        direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, gameManager.instance.enemyShipRotation * Time.deltaTime);
        transform.Translate(Time.deltaTime * gameManager.instance.enemySpeed, 0, 0);
    }
}

