using UnityEngine;

public class Periscope : MonoBehaviour
{
    [SerializeField]
    Vector2 clamp = new Vector2 (-20, 20);

    [SerializeField]
    Transform seaPivot;

    [SerializeField]
    Transform leftHandle;
    [SerializeField]
    Transform rightHandle;

    public void OnInteract(XRHand hand, IInteractor interact)
    {
        transform.LookAt(hand.transform, Vector3.up);

        float angle = Vector3.SignedAngle(Vector3.forward, interact.transform.localPosition.normalized, Vector3.up);
        angle = transform.eulerAngles.y - angle;
        //angle = Mathf.Clamp(transform.eulerAngles.y - angle, clamp.x, clamp.y);

        if (angle > 180)
            angle -= 360;

        if (angle < clamp.x)
            angle = clamp.x;
        if (angle > clamp.y)
            angle = clamp.y;

        transform.eulerAngles = new Vector3 (0, angle, 0);
        seaPivot.rotation = transform.rotation;

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward);

        Vector3 dir = rightHandle.position - transform.position;
        Gizmos.DrawRay(transform.position, dir.normalized);

        float angle = Vector3.SignedAngle(Vector3.forward, rightHandle.localPosition.normalized, Vector3.up);
        UnityEditor.Handles.Label(transform.position + dir.normalized, angle.ToString());

        dir = leftHandle.position - transform.position;
        Gizmos.DrawRay(transform.position, dir.normalized);
        angle = Vector3.SignedAngle(Vector3.forward, leftHandle.localPosition.normalized, Vector3.up);
        UnityEditor.Handles.Label(transform.position + dir.normalized, angle.ToString());
    }
#endif
}
