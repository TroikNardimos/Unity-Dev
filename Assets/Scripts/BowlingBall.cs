using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    [SerializeField][Range(1, 15)] float maxForce = 10;
    [SerializeField] Transform view;

    public Rigidbody rb;
    Vector3 force = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        force = view.rotation * direction * maxForce;


    }

    private void FixedUpdate()
    {
        rb.AddForce(force, ForceMode.Force);
    }
}
