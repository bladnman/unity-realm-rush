using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
  [SerializeField] bool isPlaceable = false;
  [SerializeField] Tower towerPrefab;
  public bool IsPlaceable { get { return isPlaceable; } }

  void OnMouseDown() {
    if (isPlaceable) {
      if (towerPrefab.CreateTower(towerPrefab, transform.position)) {
        isPlaceable = false;
      }
    }
  }
}
