using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    private int springPower = 10;
    private int springMaxPower = 175;
    private bool canPull = true;
    [SerializeField] private Transform springTrans;

    private IEnumerator increasePower;
    private IEnumerator resetSpring;

    // Raycast variables
    [SerializeField] private Transform rayTrans;
    [SerializeField] private LayerMask rayLayer;
    [SerializeField] private float rayDistance;

    void Start()
    {
        increasePower = IncreasePower();
        resetSpring = ResetSpring();
    }

    IEnumerator IncreasePower()
    {
        while (true)
        {
            // Increase power and pull the spring down as long as we hold down the spring button.
            if (springPower < springMaxPower)
            {
                springPower += 5;
                springTrans.Translate(-Vector3.down * 40 * Time.deltaTime);
            }

            yield return new WaitForSeconds(0.025f);
        }
    }

    IEnumerator ResetSpring()
    {
        while (true)
        {
            // Decrease power and push the spring up as long as the spring button is letgo off.
            if (springPower != 0)
            {
                springPower -= 5;
                canPull = false;
                springTrans.Translate(Vector3.down * 40 * Time.deltaTime);
            }

            // The spring is reset.
            else if (springPower == 0)
            {
                canPull = true;
                StopCoroutine(resetSpring);
            }

            yield return new WaitForSeconds(0.025f);
        }
    }

    void CheckForBall()
    {
        // Make a raycast at the top of our spring to check if the ball is in our range.
        RaycastHit rayHit;
        if (Physics.Raycast(rayTrans.position, rayTrans.forward, out rayHit, rayDistance, rayLayer))
        {
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
            StartCoroutine(increasePower);
    }

    // Once we charged the spring; check for the ball.
    public void LetGoSpring()
    {
        StopCoroutine(increasePower);
        StartCoroutine(resetSpring);
        CheckForBall();
    }
}
