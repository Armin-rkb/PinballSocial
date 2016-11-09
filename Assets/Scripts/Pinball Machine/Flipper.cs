using UnityEngine;
using System.Collections;

public class Flipper : MonoBehaviour
{
    // The HingeJoint component of the flipper.
    [SerializeField] private HingeJoint hinge;

    // Variables of the HingeJoint that we HAVE to use in order to make changes to them.
    private JointMotor motor;
    private JointLimits limits;

    // How far we will rotate.
    [SerializeField] private float maxLimit;
    
    // The rate at how fast we will try to reach.
    [SerializeField] private float targetVelocity;
    
    // The amount of force we will keep using to reach the targetVelocity.
    [SerializeField] private float forcePower;

    // Key we need to press to activate motor.
    [SerializeField]
    private string inputKeyName;

    private IEnumerator limitAngle;

    void Start()
    {
        UseFlipper();
        ResetFlipper();
        limitAngle = LimitAngle();
    }
    
    public void UseFlipper()
    {
        // Stop the running LimitAngle coroutine.
        StopAllCoroutines();

        // Setting our variables.
        limits.max = maxLimit;
        motor.force = forcePower;
        motor.targetVelocity = targetVelocity;

        // Use the hinge motor to rotate our flipper.
        hinge.useMotor = true;

        // Setting the hinge variables.
        hinge.limits = limits;
        hinge.motor = motor;
    }

    public void ResetFlipper()
    {
        StartCoroutine(LimitAngle());

        // Stop using the hinge motor.
        hinge.useMotor = false;

        // Setting the hinge variable.
        hinge.motor = motor;
    }

    IEnumerator LimitAngle()
    {
        while (true)
        {
            // By decreasing the limit to zero will smoothly reach the default position.
            if (hinge.limits.max != 0)
                limits.max -= 30;

            // Once we reached our default position; stop running this function.
            else if (hinge.limits.max == 0)
                StopCoroutine(limitAngle);

            // Setting the hinge variable.
            hinge.limits = limits;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
