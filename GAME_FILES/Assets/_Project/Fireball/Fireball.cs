using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField]
    GameObject[] spawnPoints;

    [SerializeField]
    GameObject fireballPrefab;

    [SerializeField]
    GameObject fireballPrimeSound;

    public GameManager gameManager;

    GameObject firstPoint;
    GameObject secondPoint;

    private int gameState = 0;
    private float fireballSpeed = 8f;
    private float fireballRepetition = 7f;

    void Start()
    {
        InvokeRepeating("PrimeFireBall", 5.0f, fireballRepetition);
    }

    void Update()
    {
        if (gameManager != null)
        {
            gameState = gameManager.GetGameState();
        }
    }

    private void SendFireball()
    {
        Debug.Log("shoot fireball");

        Vector3 firstPosition = firstPoint.transform.position;
        Vector3 secondPosition = secondPoint.transform.position;

        GameObject fireball = Instantiate(fireballPrefab, firstPosition, Quaternion.identity);

        StartCoroutine(MoveFireball(fireball, firstPosition, secondPosition, fireballSpeed));

    }

    private void PrimeFireBall()
    {
        Get2Points();
        GameObject sound = Instantiate(fireballPrimeSound, firstPoint.transform.position, Quaternion.identity);
        Invoke("SendFireball", 1f);
    }

    void LateUpdate()
    {
        if (gameState == 0)
        {
            fireballSpeed = 6f;
            fireballRepetition = 8f;

        }
        else if (gameState == 1)
        {
            fireballSpeed = 8f;
            fireballRepetition = 7f;

        }
                    
        else if (gameState == 2)
        {
            fireballSpeed = 9f;
            fireballRepetition = 6f;

        }
        else if (gameState == 3)
        {
            fireballSpeed = 10f;
            fireballRepetition = 5f;

        }
        else if (gameState == 4)
        {
            fireballSpeed = 12f;
            fireballRepetition = 4f;

        }
        else
        {
            fireballSpeed = 20f;
            fireballRepetition = 3f;

        }
    }

    void Get2Points()
    {
        int firstIndex = Random.Range(0, 23);
        int secondIndex = firstIndex + 12 + Random.Range(-2, 2);
        if (secondIndex > 23)
        {
            secondIndex = secondIndex - 23;
        }

        if (secondIndex < 0)
        {
            secondIndex = secondIndex + 23;
        }

        firstPoint = spawnPoints[firstIndex];
        secondPoint = spawnPoints[secondIndex];
    }

    IEnumerator MoveFireball(GameObject fireball, Vector3 start, Vector3 end, float speed)
    {
        float journeyLength = Vector3.Distance(start, end);
        float startTime = Time.time;

        while (fireball != null)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            fireball.transform.position = Vector3.Lerp(start, end, fracJourney);

            if (fracJourney >= 1.0f)
            {
                Destroy(fireball);
                yield break;
            }

            yield return null;
        }

    }
}
