using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

  [SerializeField] Vector2Int startCoordinates;
  public Vector2Int StartCoordinates { get { return startCoordinates; } }
  [SerializeField] Vector2Int destinationCoordinates;
  public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

  Node startNode;
  Node destinationNode;
  Node currentSearchNode;

  Queue<Node> frontier = new Queue<Node>();
  Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

  Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
  GridManager gridManager;
  Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
  List<Node> finalPath;

  void Awake() {
    gridManager = FindObjectOfType<GridManager>();
    if (gridManager != null) {
      grid = gridManager.Grid;
    }

    startNode = grid[startCoordinates];
    destinationNode = grid[destinationCoordinates];
  }
  void Start() {
    GetNewPath();
  }
  public List<Node> GetNewPath() {
    gridManager.ResetNodes();
    BreadthFirstSearch();
    return BuildPath();
  }
  void ExploreNeighbors() {
    List<Node> neighbors = new List<Node>();

    foreach (Vector2Int direction in directions) {
      Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
      Node neighborNode = gridManager.GetNode(neighborCoords);
      if (neighborNode != null && !neighborNode.isExplored) {
        neighbors.Add(neighborNode);
      }
    }

    foreach (Node neighbor in neighbors) {
      if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable) {
        neighbor.connectedTo = currentSearchNode;
        reached.Add(neighbor.coordinates, neighbor);
        frontier.Enqueue(neighbor);
      }
    }
  }
  void BreadthFirstSearch() {


    startNode.isWalkable = true;
    destinationNode.isWalkable = true;

    frontier.Clear();
    reached.Clear();

    bool isRunning = true;
    frontier.Enqueue(startNode);
    reached.Add(startNode.coordinates, startNode);

    while (frontier.Count > 0 && isRunning) {
      currentSearchNode = frontier.Dequeue();
      currentSearchNode.isExplored = true;

      ExploreNeighbors();
      if (currentSearchNode.coordinates == destinationCoordinates) {
        isRunning = false;
      }
    }
  }
  List<Node> BuildPath() {

    List<Node> path = new List<Node>();
    Node currentNode = destinationNode;

    while (currentNode.connectedTo != null) {
      path.Add(currentNode);
      currentNode.isPath = true;

      currentNode = currentNode.connectedTo;
    }

    path.Reverse();
    return path;
  }
  public bool WillBlockPath(Vector2Int coordinates) {
    if (grid.ContainsKey(coordinates)) {
      bool prevState = grid[coordinates].isWalkable;
      // pretend we placed something at these coords
      grid[coordinates].isWalkable = false;
      // and get the path
      List<Node> newPath = GetNewPath();
      // unpretend
      grid[coordinates].isWalkable = prevState;

      // no path means it would block things!
      if (newPath.Count < 1) {
        // go back to previous path
        GetNewPath();
        return true;
      }
    }
    return false;
  }
}
