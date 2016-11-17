using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 
    // Transform of the camera
    [SerializeField] private Transform cameraTrans;
    // Object we want to follow.
    [SerializeField] private Transform followTarget;
    // Delayed time to reach our needed position.
    [Range(0,5)] [SerializeField] private float dampTime;

    // Distance between our target and the camera.
    private Vector3 offset;
    // Main position we will use as reference when playing on the board.
    private Vector3 startPosition;
    // The end position we will head towards.
    private Vector3 targetPosition;
    // Position of the spring.(Needs to be improved)
    [SerializeField] private Transform springTrans;

    // We enable and disable this boolean when we want to switch to another angle.
    private bool canFollow;
    public bool CanFollow
    {
        get { return canFollow; }
        set { canFollow = value; }
    }
    private bool isWaiting = true;
    public bool IsWaiting
    {
        get { return isWaiting; }
        set { isWaiting = value; }
    }

    void Start()
    {
        startPosition = cameraTrans.position;
        CalculateOffset();
    }

    void FixedUpdate()
    {
        if (canFollow && !isWaiting)
            FollowTarget();
        else if (!canFollow && !isWaiting)
            GoToSpring();
    }

    void GoToSpring()
    {
        targetPosition = springTrans.position;

        cameraTrans.position = Vector3.Lerp(cameraTrans.position, targetPosition, dampTime * Time.deltaTime);
    }

    void CalculateOffset()
    {
        offset = cameraTrans.position - followTarget.position;
    }

    // Follow our target with a smooth delay.
    void FollowTarget()
    {
        targetPosition = new Vector3(startPosition.x, followTarget.position.y + offset.y, Mathf.Clamp(followTarget.position.z + offset.z, 105, 200));
    
        cameraTrans.position = Vector3.Lerp(cameraTrans.position, targetPosition, dampTime * Time.deltaTime);
    }
}
