using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
  [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;
  [SerializeField] [Range(0, 50)] int poolSize = 5;
  [SerializeField] GameObject objectPrefab;

  GameObject[] pool;

  void Awake() {
    PopulatePool();
  }
  void Start() {
    StartCoroutine(SpawnEnemies());
  }
  void PopulatePool() {
    pool = new GameObject[poolSize];
    for (int i = 0; i < poolSize; i++) {
      GameObject poolObject = Instantiate(objectPrefab, transform);
      pool[i] = poolObject;
      poolObject.SetActive(false);
    }
  }
  void EnableObjectInPool() {
    foreach (GameObject go in pool) {
      if (!go.activeInHierarchy) {
        go.SetActive(true);
        break;
      }
    }
  }
  IEnumerator SpawnEnemies() {
    while (true) {
      EnableObjectInPool();
      yield return new WaitForSeconds(spawnTimer);
    }
  }

}
