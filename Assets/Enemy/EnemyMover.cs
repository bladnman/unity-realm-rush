using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour {
  [SerializeField] [Range(0f, 5f)] float speed = 1f;

  List<Node> path = new List<Node>();
  Enemy enemy;
  GridManager gridManager;
  PathFinder pathFinder;

  private void Awake() {
    gridManager = FindObjectOfType<GridManager>();
    pathFinder = FindObjectOfType<PathFinder>();
    enemy = GetComponent<Enemy>();
  }
  void OnEnable() {
    FindPath();
    ReturnToStart();
    StartCoroutine(FollowPath());
  }
  void FindPath() {
    path.Clear();
    path = pathFinder.GetNewPath();
  }
  void ReturnToStart() {
    if (path.Count > 0) {
      transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }
  }
  void FinishPath() {
    enemy.StealGold();
    gameObject.SetActive(false);
  }
  IEnumerator FollowPath() {
    for (int i = 0; i < path.Count; i++) {
      Vector3 startPosition = transform.position;
      Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);

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
