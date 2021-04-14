using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
  [SerializeField] bool isPlaceable = false;
  [SerializeField] Tower towerPrefab;
  public bool IsPlaceable { get { return isPlaceable; } }

  GridManager gridManager;
  PathFinder pathFinder;
  Vector2Int coordinates = new Vector2Int();

  Bank bank;

  private void Awake() {
    gridManager = FindObjectOfType<GridManager>();
    pathFinder = FindObjectOfType<PathFinder>();
    bank = FindObjectOfType<Bank>();
  }

  private void Start() {
    if (gridManager != null) {
      coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

      if (!isPlaceable) {
        gridManager.BlockNode(coordinates);
      }
    }
  }
  bool canAffordTower() {
    return bank.CurrentBalance >= towerPrefab.Cost;
  }
  void OnMouseDown() {
    Node node = gridManager.GetNode(coordinates);
    if (null == node) return;
    if (!canAffordTower()) return;
    if (!node.isWalkable) return;

    if (!pathFinder.WillBlockPath(coordinates)) {
      if (towerPrefab.CreateTower(towerPrefab, transform.position)) {
        isPlaceable = false;
        gridManager.BlockNode(coordinates);
      }
    }
  }
}
