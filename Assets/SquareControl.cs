using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareControl : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector3 lastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = _rb.velocity;
    }
     private void OnCollisionEnter2D(Collision2D other)
    {
         MapControl otherScript = other.gameObject.GetComponentInParent<MapControl>();
        if(other.gameObject.CompareTag("Pass"))
        {
            if(otherScript.count==0)
            {
            otherScript.Ispass = true;
            }
            else
            {
             otherScript.NotPass = true;
            }
        }
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

        _rb.velocity = direction * Mathf.Max(speed, 0f);
        otherScript.minusCount(1);
    }
}
