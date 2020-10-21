using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlightController : MonoBehaviour
{
    public float initSpeed;
    public float initAcceleration;
    public float Sprint;
    public float BaseX;
    public float DownBoost;

    Rigidbody2D rb;

    public float RotationControl;

    float Speed;
    float Acceleration;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Speed = initSpeed;
        Acceleration = initAcceleration;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Fly(float MovX, float MovY, bool FacingRight) {
        //Acceleration
        float FinalSpeed = initSpeed;
        float FinalAcceleration = initAcceleration;

        //Sprint
        if (MovX != 0) {
            FinalSpeed += Sprint;
            FinalAcceleration += Sprint;
        }
        //Dive
        if (((int)Math.Floor(transform.up.x * 10) == 9 && FacingRight) || ((int)Math.Ceiling(transform.up.x * 10) == -9 && !FacingRight)) {
            FinalSpeed += DownBoost;
            FinalAcceleration += DownBoost;
        }
        Speed = FinalSpeed;
        Acceleration = FinalAcceleration;

        Vector2 Vel;
        Vel = transform.right * (BaseX * Acceleration);
        rb.AddForce(Vel);


        //float Dir = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right));
        if (Acceleration > 0) {
            if (FacingRight) {
                rb.rotation += MovY * RotationControl * (Math.Abs(rb.velocity.magnitude) / Speed);
            }
            else {
                rb.rotation -= MovY * RotationControl * (Math.Abs(rb.velocity.magnitude) / Speed);
            }
        }

        float thrustForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.down)) * 2.0f;

        Vector2 relForce = Vector2.up * thrustForce;

        rb.AddForce(rb.GetRelativeVector(relForce));

        if(rb.velocity.magnitude > Speed) {
            rb.velocity = rb.velocity.normalized * Speed;
        }
    }
}
