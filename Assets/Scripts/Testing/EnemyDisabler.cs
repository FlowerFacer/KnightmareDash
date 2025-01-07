using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisabler : MonoBehaviour
{
    public List<GameObject> enemiesToDisable; // List of enemies to disable

    public void DisableEnemies()
    {
        foreach (GameObject enemy in enemiesToDisable)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
                Debug.Log($"Disabled enemy: {enemy.name}");
            }
        }
    }

    public void EnableEnemies()
    {
        foreach (GameObject enemy in enemiesToDisable)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
                Debug.Log($"Enabled enemy: {enemy.name}");
            }
        }
    }
}
