using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    [SerializeField] private int springPower;
    private int springMaxPower = 50;
    private bool canPull = true;
    [SerializeField] private Transform springTrans;
    
    [SerializeField] private Transform rayTrans;
    [SerializeField] private LayerMask rayLayer;
    [SerializeField] private float rayDistance;

    void IncreasePower()
    {
        // Increase power and shrink spring as long as we hold down the pull button.
        if (springPower < springMaxPower){
            springPower ++;
            springTrans.localScale = new Vector3(springTrans.localScale.x, springTrans.localScale.y - 2f, springTrans.localScale.z);
        }
    }

    void ResetSpring()
    {
        // Decrease power and enlarge spring as long as the button was letgo.
        if (springPower != 0)
        {
            springPower --;
            canPull = false;
            springTrans.localScale = new Vector3(springTrans.localScale.x, springTrans.localScale.y + 2f, springTrans.localScale.z);
        }
        // The spring is reset.
        else if (springPower == 0)
        {
            canPull = true;
            CancelInvoke("ResetSpring");
        }
    }

    void CheckForBall()
    {
        // Make a raycast at the top of our spring to check if the ball is in our range.
        RaycastHit rayHit;
        if (Physics.Raycast(rayTrans.position, rayTrans.forward, out rayHit, rayDistance, rayLayer)){
            PushBall(rayHit.rigidbody);
        }
    }

    void PushBall(Rigidbody ball)
    {
        // Shoot the ball Forward.
        ball.AddForce(-springTrans.up * (springPower * 3), ForceMode.Impulse);
    }

    // Button to charge spring power.
    public void PullSpring()
    {
        if (canPull)
            InvokeRepeating("IncreasePower", 0, 0.05f);
    }

    // Once we charged the spring; check for the ball.
    public void LetGoSpring()
    {
        CancelInvoke("IncreasePower");
        InvokeRepeating("ResetSpring", 0, 0.05f);
        CheckForBall();
    }
}
