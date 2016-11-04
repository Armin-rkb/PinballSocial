using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour
{
    public delegate void OnBallHit(Bumper bumper);
    public static event OnBallHit BallHit;

    private Vector3 originalScale;
    private bool hasGrown;

    // How hard we will push the ball away.
    [SerializeField] private float bumpForce;

    // The amount of points this bumper holds.
    [SerializeField] private int bumpPoints;
    public int BumpPoints
    {
        get { return bumpPoints; }
    }

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
            BounceBall(coll.rigidbody);
    }

    void BounceBall(Rigidbody rb)
    {
        // Bounce the ball Away.
        rb.AddExplosionForce(bumpForce, transform.position, 0);

        // Add the small grow effect of the bumper.
        StartCoroutine(EnlargeBumper());

        // Notify all subscribers of BallHit.
        if (BallHit != null)
            BallHit(this);        
    }

    IEnumerator EnlargeBumper()
    {
        while (true)
        {
            if (!hasGrown)
            {
                transform.localScale = new Vector3(originalScale.x * 1.5f, originalScale.y, originalScale.z * 1.5f);
                hasGrown = true;
            }
            else
            {
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                hasGrown = true;
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(0.15f);
        }
    }
}
