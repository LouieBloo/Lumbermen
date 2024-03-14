using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static AllUnitPrefabs;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]
    public List<UnitSpawnerUI> unitsToSpawn;

    [System.Serializable]
    public class UnitSpawnerUI
    {
        [SerializeField]
        private UnitName unitToSpawn;

        [SerializeField]
        private int maxAlive = 1;
        private int currentUnitCount = 0;

        [SerializeField]
        private float xDistanceSpawnRange = 1;
        [SerializeField]
        private float yDistanceSpawnRange = 1;

        [SerializeField]
        private float spawnTimeInSeconds = 30;
        private float timeSinceLastSpawn = 0;

        [SerializeField]
        private int startingUnits = 1;

        private Transform spawnTransform;

        public void setup(Transform spawnTransform)
        {
            this.spawnTransform = spawnTransform;
            for(int x = startingUnits; x > 0; x--)
            {
                spawnUnit();
            }
        }

        // I avoided calling this Update because I dont want people to think its getting called by the engine
        public void UpdateSpawnTime()
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnTimeInSeconds) {
                spawnUnit();
                timeSinceLastSpawn = 0;
            }
        }

        private void spawnUnit()
        {
            if(currentUnitCount < maxAlive)
            {
                currentUnitCount++;
                GameObject spawnedUnit = GameObject.Instantiate(AllUnitPrefabs.Instance.getUnit(UnitName.Enemy), GenerateRandomPoint(), Quaternion.identity);
                spawnedUnit.GetComponent<Enemy>().setup(unitToSpawn, unitDied);
            }
        }

        void unitDied()
        {
            currentUnitCount--;
        }

        Vector3 GenerateRandomPoint()
        {
            // Generate random x and y distances within the specified bounds
            float randomXDistance = Random.Range(0, xDistanceSpawnRange);
            float randomYDistance = Random.Range(0, yDistanceSpawnRange);

            // Calculate random x and y offsets
            float randomXOffset = Random.Range(-randomXDistance, randomXDistance);
            float randomYOffset = Random.Range(-randomYDistance, randomYDistance);

            // Calculate the random point's position
            return spawnTransform.position + new Vector3(randomXOffset, randomYOffset, 0f);
        }
    }

    void Start()
    {
        foreach (UnitSpawnerUI unitToSpawn in unitsToSpawn)
        {
            unitToSpawn.setup(transform);
        }
    }

    void Update()
    {
        foreach (UnitSpawnerUI unitToSpawn in unitsToSpawn)
        {
            unitToSpawn.UpdateSpawnTime();
        }
    }

}
