using UnityEngine;
using System.Collections;

public class Flipper : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    private bool canFlip;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
            canFlip = true;
	}

    void FixedUpdate () {
        if (canFlip)
            RotateFlipper();
        if (rb.rotation.y > -45)
            rb.rotation = Quaternion.Euler(0, -45, 0);
        else if (rb.rotation.y < 0)
            rb.rotation = Quaternion.Euler(0, 0, 0);
    }

    void RotateFlipper()
    {
        print("hey hahah ik werk.");
        rb.AddTorque(-transform.up * 10000);
        canFlip = false;
    }
}
