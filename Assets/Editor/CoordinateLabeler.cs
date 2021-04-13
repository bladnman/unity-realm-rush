using UnityEngine;
using TMPro;

// indicates that this should always be called, not just when playing
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour {
  [SerializeField] Color defaultColor = Color.white;
  [SerializeField] Color blockedColor = Color.yellow;

  TextMeshPro label;
  Vector2Int coordinates = new Vector2Int();
  Waypoint waypoint;
  void Awake() {
    label = GetComponent<TextMeshPro>();
    label.enabled = false;

    waypoint = GetComponentInParent<Waypoint>();

    // call once - playing will never call again
    DisplayCoordinates();
    SetLabelColor();
  }
  void Update() {
    UpdateLabel();
  }
  void UpdateLabel() {
    // call this always in edit mode
    if (!Application.isPlaying) {
      DisplayCoordinates();
      UpdateObjectName();
    }

    SetLabelColor();
    ToggleLabels();
  }

  void DisplayCoordinates() {
    float gridXf = transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x;
    float gridYf = transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z;
    coordinates.x = Mathf.RoundToInt(gridXf);
    coordinates.y = Mathf.RoundToInt(gridYf);

    label.text = $"{coordinates.x},{coordinates.y}";
  }

  private void SetLabelColor() {
    label.color = waypoint.IsPlaceable ? defaultColor : blockedColor;
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
