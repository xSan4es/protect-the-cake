using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject flyMonster;
    public GameObject thornMonster;

    public int countDirectionAttack;
    public float timeBeforeStartGame;
    public float timeBeforeSpawningEnemy;
    public Transform respawnEnemy;

    private int randomDirection;
    private float heightScreen;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        heightScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).x;
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBeforeStartGame);

        while (true)
        {
            InstantiateMonster(flyMonster);
            yield return new WaitForSeconds(timeBeforeSpawningEnemy);

            InstantiateMonster(thornMonster);
            yield return new WaitForSeconds(timeBeforeSpawningEnemy);
        }
        
    }

    public void InstantiateMonster (GameObject gameobj)
    {
        randomDirection = Random.Range(0, countDirectionAttack + 1);

        Instantiate(gameobj, new Vector3
            (respawnEnemy.position.x, respawnEnemy.position.y +
            (heightScreen / countDirectionAttack) * randomDirection, 0),
            new Quaternion(0, 0, 0, 0));
    }
}


