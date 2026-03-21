using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState instance;
    private bool HasGameStarted = false;
    private bool HasPlayerLost = false;
    private Coroutine RespawnCoroutine;

    public Transform spawnPoint;

    public static GameState Instance
    {
        get { return instance; }
    } 

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        HasGameStarted = true;
        HasPlayerLost = false;
    }

    public void EndGame()
    {
        HasGameStarted = false;
        HasPlayerLost = true;
    }

    public void ResetGame()
    {
        HasGameStarted = false;
        HasPlayerLost = false;
    }

    public void RespawnPlayer(Collision collision)
    {
        if (RespawnCoroutine != null)
        {
            StopCoroutine(RespawnCoroutine);
            RespawnCoroutine = StartCoroutine(RespawnPlayerCoroutine(collision));
        } else
        {
            RespawnCoroutine = StartCoroutine(RespawnPlayerCoroutine(collision));
            // StartCoroutine(RespawnPlayerCoroutine(collision));
        }
    }

    public void RespawnPlayer(Collider collider)
    {
        if (RespawnCoroutine != null)
        {
            StopCoroutine(RespawnCoroutine);
            RespawnCoroutine = StartCoroutine(RespawnPlayerCoroutine(collider));
        } else
        {
            RespawnCoroutine = StartCoroutine(RespawnPlayerCoroutine(collider));
            // StartCoroutine(RespawnPlayerCoroutine(collision));
        }
    }

    private IEnumerator RespawnPlayerCoroutine(Collision collision)
    {
        yield return new WaitForSeconds(1f);
        collision.gameObject.transform.position = spawnPoint.position;
        ResetGame();
    }

    private IEnumerator RespawnPlayerCoroutine(Collider collider)
    {
        yield return new WaitForSeconds(1f);
        collider.gameObject.transform.position = spawnPoint.position;
        ResetGame();
    }

    public bool GetGameState()
    {
        return HasGameStarted;
    }

    public bool GetPlayerState()
    {
        return HasPlayerLost;
    }
}
