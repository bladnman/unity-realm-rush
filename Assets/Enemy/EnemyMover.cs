using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour {
  [SerializeField] [Range(0f, 5f)] float speed = 1f;

  List<Waypoint> path = new List<Waypoint>();
  Enemy enemy;

  void Start() {
    enemy = GetComponent<Enemy>();
  }
  void OnEnable() {
    FindPath();
    ReturnToStart();
    StartCoroutine(FollowPath());
  }
  void FindPath() {
    path.Clear();

    GameObject pathContainer = GameObject.FindGameObjectWithTag("Path");
    foreach (Waypoint waypoint in pathContainer.GetComponentsInChildren<Waypoint>()) {
      path.Add(waypoint);
    }
  }
  void ReturnToStart() {
    if (path.Count > 0) {
      transform.position = path[0].transform.position;
    }
  }
  void FinishPath() {
    enemy.StealGold();
    gameObject.SetActive(false);
  }
  IEnumerator FollowPath() {
    foreach (Waypoint waypoint in path) {

      Vector3 startPosition = transform.position;
      Vector3 endPosition = waypoint.transform.position;
      float travelPerc = 0f;

      transform.LookAt(endPosition);

      while (travelPerc < 1) {
        travelPerc += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, endPosition, travelPerc);
        yield return new WaitForEndOfFrame();
      }
    }

    // end of path
    FinishPath();
  }
}
