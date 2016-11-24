using UnityEngine;

public class OuterSpringWallCheck : MonoBehaviour
{
    [SerializeField] private SpringWall springWall;

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag(Tags.ball))
            StartCoroutine(springWall.raiseWall);
    }
}
