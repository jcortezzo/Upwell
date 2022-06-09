using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; set; }
    public Player Player { get; private set; }

    public Camera Camera { get; private set; }

    [SerializeField]
    private GameObject LEVEL_MANAGER_PREFAB;

    void Awake()
    {
        SetUpSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpScene()
    {
        Player = FindObjectOfType<Player>();
        Camera = Camera.main;
        LevelManager levelManager = 
                Instantiate(LEVEL_MANAGER_PREFAB, 
                            transform.position, 
                            Quaternion.identity, 
                            this.transform)
                .GetComponent<LevelManager>();
    }

    private void SetUpSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
