using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
  
public class Spawner: MonoBehaviour {
    public Vector3 spawnValues;
    public bool stop;  
    public GameObject enemyPrefab;
  
    
  
    void Start() {  
        StartCoroutine(WaitSpawner());  
    }

    IEnumerator WaitSpawner() {  
        yield return new WaitForSeconds(5); 
  
        while (!stop) {

            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1f, Random.Range(-spawnValues.z, spawnValues.z));  
  
            Instantiate(enemyPrefab, spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            
            yield return new WaitForSeconds(100);  
        }  
    }  
}