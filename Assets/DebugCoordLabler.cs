using UnityEngine;
using TMPro;



// indicates that this should always be called, not just when playing
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class DebugCoordLabler : MonoBehaviour {
  [SerializeField] Color defaultColor = Color.white;
  [SerializeField] Color blockedColor = new Color(1f, 1f, 1f, 0.5f);
  // [SerializeField] Color blockedColor = Color.blue;
  // [SerializeField] Color blockedColor = Color.gray;
  [SerializeField] Color exploredColor = Color.yellow;
  [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

  TextMeshPro label;
  Vector2Int coordinates = new Vector2Int();
  GridManager gridManager;

  bool isActive = true;

  void Awake() {
    gridManager = FindObjectOfType<GridManager>();

    label = GetComponent<TextMeshPro>();
    label.enabled = true;

    // call once - playing will never call again
    DisplayCoordinates();
  }
  void Update() {
    if (!Application.isPlaying) {
      DisplayCoordinates();
      UpdateObjectName();
      label.enabled = true;
    }

    SetLabelColor();
    ToggleLabels();
  }

  void SetCoordinates() {
    if (null == gridManager) return;
    coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.WorldTileSize);
    coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.WorldTileSize);
  }
  void DisplayCoordinates() {
    SetCoordinates();
    label.text = coordinates.x + "," + coordinates.y;
  }

  private void SetLabelColor() {
    if (gridManager == null) return;

    Node node = gridManager.GetNode(coordinates);
    if (node == null) return;

    if (!node.isWalkable) label.color = blockedColor;
    else if (node.isPath) label.color = pathColor;
    else if (node.isExplored) label.color = exploredColor;
    else label.color = defaultColor;
  }

  void ToggleLabels() {
    if (Input.GetKeyDown(KeyCode.C)) {
      label.enabled = !label.IsActive();
    }
  }
  void UpdateObjectName() {
    transform.parent.name = coordinates.ToString();
  }
}
