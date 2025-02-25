using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private Enemy enemyPrefab;

    public void AddEnemy()
    {
        Enemy newEnemy = Instantiate(enemyPrefab, transform);
        newEnemy.transform.position = new Vector3(Random.Range(-40, 40),0, Random.Range(-40, 40));
        enemyList.Add(newEnemy);
    }
    public async void InitEnemy(int numberOfEnemy)
    {
        while(enemyList.Count < numberOfEnemy)
        {
            AddEnemy();
            await Task.Delay(5);
        }
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].gameObject.SetActive(i < numberOfEnemy);
            if (i < numberOfEnemy)
            {
                enemyList[i].SetInit();
                enemyList[i].SetWeapon();
                // -v- test
                //enemyList[i].Setspeed(0.01f);
            }
        }
    }
}
