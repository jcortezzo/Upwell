using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public EnemySpawner EnemySpawner { get; private set; }

    [SerializeField]
    private GameObject ENEMY_SPAWNER_PREFAB;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner =
                Instantiate(ENEMY_SPAWNER_PREFAB,
                            transform.position,
                            Quaternion.identity,
                            this.transform)
                .GetComponent<EnemySpawner>();

        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            EnemySpawner.SpawnEnemy();
        }
    }
}
