using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
  [SerializeField] Vector2Int gridSize;
  [SerializeField] int worldTileSize = 10;
  public int WorldTileSize { get { return worldTileSize; } }

  Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
  public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

  void Awake() {
    CreateGrid();
  }
  public Node GetNode(Vector2Int coordinates) {
    if (grid.ContainsKey(coordinates)) {
      return grid[coordinates];
    }

    return null;
  }
  void CreateGrid() {
    for (int x = 0; x < gridSize.x; x++) {
      for (int y = 0; y < gridSize.y; y++) {
        Vector2Int coordinates = new Vector2Int(x, y);
        Node node = new Node(coordinates, true);
        grid.Add(coordinates, node);
      }
    }
  }
  public void BlockNode(Vector2Int coordinates) {
    if (grid.ContainsKey(coordinates)) {
      grid[coordinates].isWalkable = false;
    }
  }
  public Vector2Int GetCoordinatesFromPosition(Vector3 position) {
    Vector2Int coordinates = new Vector2Int();
    coordinates.x = Mathf.RoundToInt(position.x / worldTileSize);
    coordinates.y = Mathf.RoundToInt(position.z / worldTileSize);

    return coordinates;
  }
  public Vector3 GetPositionFromCoordinates(Vector2Int coordinates) {
    Vector3 position = new Vector3();
    position.x = coordinates.x * worldTileSize;
    position.z = coordinates.y * worldTileSize;

    return position;
  }
  public void ResetNodes() {
    foreach (KeyValuePair<Vector2Int, Node> entry in grid) {
      entry.Value.connectedTo = null;
      entry.Value.isExplored = false;
      entry.Value.isPath = false;
    }
  }
}
