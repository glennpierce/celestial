using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    public float gizmoSize = 0.5f;
    public Color gizmoColor = Color.yellow;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    // Uncomment this and comment out OnDrawGizmos if you prefer the Gizmo to only appear when the GameObject is selected in the Editor
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = gizmoColor;
    //     Gizmos.DrawWireSphere(transform.position, gizmoSize);
    // }
}
