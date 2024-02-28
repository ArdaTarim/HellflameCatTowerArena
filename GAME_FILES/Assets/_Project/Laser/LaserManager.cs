using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class LaserManager : MonoBehaviour
{

    [SerializeField]
    GameObject laserSoundPrefab;

    [SerializeField]
    GameObject laserPrimeSoundPrefab;

    [SerializeField]
    GameObject particlesPrefab;

    [SerializeField]
    GameObject catsHead;

    [SerializeField]
    LineRenderer laser1;

    [SerializeField]
    LineRenderer laser2;

    [SerializeField]
    GameObject[] laserTargets;

    [SerializeField]
    GameObject bone;

    public GameManager gameManager;

    GameObject firstPoint;
    GameObject secondPoint;
    GameObject particles;

    private int gameState = 0;
    private float laserPrimeTime;
    private float laserDuration;
    private float timeBetweenLasers;
    private float laserRepetition;


    private void EnableLaser()
    {
        laser1.startWidth = 0.6f;
        laser1.endWidth = 0.6f;

        laser2.startWidth = 0.6f;
        laser2.endWidth = 0.6f;

        Invoke("DisableLaser", laserDuration);
    }

    private void DisableLaser()
    {
        laser1.startWidth = 0f;
        laser1.endWidth = 0f;

        laser2.startWidth = 0f;
        laser2.endWidth = 0f;
    }

    private void PlayLaserPrimeSound()
    {
        laserPrimeSoundPrefab.GetComponent<AudioSource>().enabled = true;
        Invoke("StopLaserPrimeSound", laserPrimeTime);
    }

    private void StopLaserPrimeSound()
    {
        laserPrimeSoundPrefab.GetComponent<AudioSource>().enabled = false;
    }

    private void CreateLaserPrimeParticles()
    {
        // fill this method
        Invoke("StopLaserPrimeParticles", laserPrimeTime);
    }

    private void StopLaserPrimeParticles()
    {
        //Destroy();
    }

    private void PlayLaserSound()
    {
        laserSoundPrefab.GetComponent<AudioSource>().enabled = true;
        Invoke("StopLaserSound", laserDuration);
    }

    private void StopLaserSound()
    {
        laserSoundPrefab.GetComponent<AudioSource>().enabled = false;
    }

    private void CreateLaserParticles()
    {
        particles = Instantiate(particlesPrefab, firstPoint.transform.position, Quaternion.identity);
    }

    private void PrimeLaser()
    {
        Debug.Log("prime laser");
        Find2Points();
        PlayLaserPrimeSound();
        CreateLaserPrimeParticles();
        bone.GetComponent<HeadFollowLaser>().LookAtFirstPosition(firstPoint, laserPrimeTime);
        Invoke("ShootLaser", laserPrimeTime);
    }

    private void ShootLaser()
    {
        Debug.Log("shoot laser");

        CreateLaserParticles();
        laser1.SetPosition(1, firstPoint.transform.position);
        laser2.SetPosition(1, firstPoint.transform.position);

        EnableLaser();
        PlayLaserSound();

        Vector3 targetPosition = secondPoint.transform.position;

        bone.GetComponent<HeadFollowLaser>().AnimateHead(firstPoint, secondPoint, laserDuration);

        StartCoroutine(MoveLaser(laser1, targetPosition));
        StartCoroutine(MoveLaser(laser2, targetPosition));
        StartCoroutine(MoveParticle(particles, targetPosition));
    }

    // START OF THE CLASS
    // -- Helper Methods At Bottom
    // -- Main Methods At Top

    void Awake()
    {
        DisableLaser();
        laserRepetition = 1f + 8f + 8f; // BE CAREFUL, HARD CODED
        
        laserPrimeSoundPrefab.GetComponent<AudioSource>().enabled = false;
        laserSoundPrefab.GetComponent<AudioSource>().enabled = false;
    }

    void Start()
    {
        InvokeRepeating("PrimeLaser", 2.0f, laserRepetition);
    }

    void Update()
    {
        if (gameManager != null)
        {
            gameState = gameManager.GetGameState();
        }

        laserPrimeTime = gameState switch
        {
            0 => 1f, // Easy
            1 => 1f, // Normal
            2 => 1f, // Hard
            3 => 1f, // Harder
            4 => 1f, // Hardest
            _ => 1f, // Death
        };

        laserDuration = gameState switch
        {
            0 => 8f, // Easy
            1 => 8f, // Normal
            2 => 6f, // Hard
            3 => 4f, // Harder
            4 => 3f, // Hardest
            _ => 2f, // Death
        };

        timeBetweenLasers = gameState switch
        {
            0 => 8f, // Easy
            1 => 6f, // Normal
            2 => 4f, // Hard
            3 => 4f, // Harder
            4 => 3f, // Hardest
            _ => 2f, // Death
        };

    }

    void LateUpdate()
    {
        laserRepetition = laserPrimeTime + laserDuration + timeBetweenLasers ;
    }

    private void Find2Points()
    {

        Debug.Log("found 2 points");
        int firstIndex = Random.Range(0, 11);
        int secondIndex = firstIndex + 6 + Random.Range(-1, 1);

        if (secondIndex > 11)
        {
            secondIndex = secondIndex - 12;
        }

        if (secondIndex < 0)
        {
            secondIndex = secondIndex + 12;
        }

        firstPoint = laserTargets[firstIndex];
        secondPoint = laserTargets[secondIndex];
    }

    private IEnumerator MoveLaser(LineRenderer laser, Vector3 targetPosition)
    {
        float elapsedTime = 0f;

        Vector3 initialPosition = laser.GetPosition(1);

        while (elapsedTime < laserDuration)
        {
            laser.SetPosition(1, Vector3.Lerp(initialPosition, targetPosition, elapsedTime / laserDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        laser.SetPosition(1, targetPosition);
    }

    private IEnumerator MoveParticle(GameObject particle, Vector3 targetPosition)
    {
        float elapsedTime = 0f;

        Vector3 initialPosition = particle.transform.position;

        while (elapsedTime < laserDuration)
        {
            particle.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / laserDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        particle.transform.position = targetPosition;

        if (elapsedTime >= laserDuration)
        {
            Destroy(particle);
        }
    }

    public GameObject[] GetTargetPoints()
    {
        GameObject[] targets = new GameObject[2];
        targets[0] = firstPoint;
        targets[1] = secondPoint;

        return targets;
    }



}