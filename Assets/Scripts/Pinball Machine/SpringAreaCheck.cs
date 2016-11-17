using UnityEngine;

public class SpringAreaCheck : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag(Tags.ball))
        {
            cameraFollow.CanFollow = false;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag(Tags.ball))
        {
            cameraFollow.CanFollow = true;
        }
    }
}
