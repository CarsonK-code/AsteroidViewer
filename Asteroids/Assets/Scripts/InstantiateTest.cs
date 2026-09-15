using UnityEngine;
using System.Collections.Generic;
public class InstantiateTest : MonoBehaviour
{
    public List<GameObject> asteroids = new List<GameObject>();
    void Start()
    {
        if (asteroids.Count <= 3)
        {
            for (int i = 0; i < asteroids.Count; i++ )
            {
                Vector3 spawnPosition = new Vector3(0, 0, i * 4);

                Instantiate(asteroids[i], spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("Too many items in list!");
        }
    }
}
