using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState instance;
    private bool HasGameStarted = false;

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
    }

    public bool GetGameState()
    {
        return HasGameStarted;
    }
}
