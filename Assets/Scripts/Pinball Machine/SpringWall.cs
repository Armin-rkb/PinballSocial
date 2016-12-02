using UnityEngine;
using System.Collections;

public class SpringWall : MonoBehaviour
{
    [SerializeField] private Transform lowerTrans;
    [SerializeField] private Transform defaultTrans;

    public IEnumerator raiseWall;
    public IEnumerator lowerWall;
    
    void Start()
    {
        raiseWall = RaiseWall();
        lowerWall = LowerWall();
    }

    IEnumerator RaiseWall()
    {
        while (true)
        {
            if (transform.position == defaultTrans.position)
                StopCoroutine(raiseWall);
            else
                transform.position = Vector3.MoveTowards(transform.position, defaultTrans.position, 20 * Time.deltaTime);

            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator LowerWall()
    {
        while (true)
        {
            if (transform.position == lowerTrans.position)
                StopCoroutine(lowerWall);
            else
                transform.position = Vector3.MoveTowards(transform.position, lowerTrans.position, 20 * Time.deltaTime);

            yield return new WaitForSeconds(0.02f);
        }
    }
}
