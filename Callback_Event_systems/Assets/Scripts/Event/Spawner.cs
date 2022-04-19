using UnityEngine;

namespace Event
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemyPrefab;

        public delegate void OnUnitSpawnedDelegate(Health health);

        public event OnUnitSpawnedDelegate OnUnitSpawnedListeners;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SpawnUnit();
            }
        }

        void SpawnUnit()
        {
            GameObject go = Instantiate(enemyPrefab);
            if (OnUnitSpawnedListeners != null)
            {
                OnUnitSpawnedListeners(go.GetComponent<Health>());
            }
        }
    }
}