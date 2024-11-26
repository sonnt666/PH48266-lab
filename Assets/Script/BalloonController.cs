using System.Collections;
using UnityEditor;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public GameObject balloon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(routine:SpawnSpheres());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnSpheres()
    {
        while (true)
        {
            float randomX = Random.Range(-6f, 7f);
            Vector3 spawnPosition = new Vector3 (randomX, y:-8.1f, z:0f);
            Instantiate(balloon, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
}
