using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ENEMY_PREFAB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Enemy SpawnEnemy()
    {
        Enemy enemy =
                Instantiate(ENEMY_PREFAB,
                            GlobalManager.Instance.Player.transform.position + Vector3.up * 2,
                            Quaternion.identity,
                            this.transform)
                .GetComponent<Enemy>();
        return enemy;
    }
}
