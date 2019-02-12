using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offScreenDetection : MonoBehaviour
{
    public Transform targetPoint;
    public GameObject Player;

    private Camera camera;

    // Starts before first frame
    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (GameObject.Find("SpaceshipSprite") != null)
            {
                Vector3 screenPoint = camera.WorldToViewportPoint(targetPoint.position);
                bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

                if (!onScreen)
                {
                    if (gameManager.instance.playerLives > 0)
                    {
                        gameManager.instance.playerLives--;
                        Player.transform.position = playerMovement.originPosition;
                        Debug.Log("You now have a total of " + gameManager.instance.playerLives + " lives left.");
                    }
                    else
                    {
                        Debug.LogWarning("Player ship was destroyed for being out of camera view");
                        Destroy(Player);
                    }
                }
            }
        }
        catch
        {
            //Does nothing
        }
    }
}
