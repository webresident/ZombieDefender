using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float forwardFOV = 90f;
    [SerializeField] private float backFOV = 60f;
    [SerializeField] private float viewDistance = 20f;
    [SerializeField] private LayerMask cullableMask;

    private void Update()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, cullableMask);

        foreach (Collider col in targets)
        {
            if (col.TryGetComponent<Renderer>(out var rend))
            {
                Vector3 dirToTarget = (col.transform.position - cameraTransform.position).normalized;
                float angleToCamera = Vector3.Angle(cameraTransform.forward, dirToTarget);

                Vector3 dirToTargetFromPlayer = (col.transform.position - player.position).normalized;
                float angleToBack = Vector3.Angle(-player.forward, dirToTargetFromPlayer);

                bool shouldBeVisible = angleToCamera <= forwardFOV / 2f || angleToBack <= backFOV / 2f;

                rend.enabled = shouldBeVisible;
            }
        }
    }
}
