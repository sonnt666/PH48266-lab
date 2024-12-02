using System.Collections;
using UnityEngine;

public class EggFalling : MonoBehaviour
{
    public GameObject Egg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEgg());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEgg()
    {
        while (true)
        {
            float randomX = Random.Range(-2.3f, 2.3f);
            Vector3 spawnPosition = new Vector3(randomX, y: 4.5f, z: 0f);
            Instantiate(Egg, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
}
