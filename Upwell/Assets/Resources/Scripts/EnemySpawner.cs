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

    public Enemy[] SpawnEnemies(int numEnemies)
    {
        Enemy[] enemies = new Enemy[numEnemies];
        var positions = GetTopScreenPositions(numEnemies);

        for (int i = 0; i < numEnemies; i++)
        {
            Vector2 spawnPosition = positions[i];
            spawnPosition.y = GlobalManager.Instance.Camera.pixelHeight;

            Enemy enemy = SpawnEnemy(GlobalManager.Instance.Camera.ScreenToWorldPoint(spawnPosition));
            enemy.EnterScreenPosition = GlobalManager.Instance.Camera.ScreenToWorldPoint(positions[i]);

            enemies[i] = enemy;
        }

        return enemies;
    }

    public Vector2[] GetTopScreenPositions(int numEnemies)
    {
        Vector2[] positions = new Vector2[numEnemies];
        float width = GlobalManager.Instance.Camera.pixelWidth;
        float height = GlobalManager.Instance.Camera.pixelHeight;
        Debug.Log(width);
        for (int i = 0; i < numEnemies; i++)
        {
            Vector2 screenPoint = new Vector2(((float) i / numEnemies) * width, height - height / GlobalManager.PPU);

            positions[i] = screenPoint;
        }
        return positions;
    }

    public Enemy SpawnEnemy()
    {
        return SpawnEnemy(GlobalManager.Instance.Player.transform.position + Vector3.up * 2);
    }

    public Enemy SpawnEnemy(Vector2 position)
    {
        Enemy enemy =
                Instantiate(ENEMY_PREFAB,
                            position,
                            Quaternion.identity,
                            this.transform)
                .GetComponent<Enemy>();
        return enemy;
    }
}
