using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ByoantScript : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float buoyancyForce = 10;
    private Rigidbody thisRigidbody;
    private bool hasTouchedWater;

    // Start is called before the first frame update
    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        float diffy = transform.position.y;
        bool isUnderwater = diffy < 0;

        if (isUnderwater)
        {
            hasTouchedWater = true;
        }
        
        if(!hasTouchedWater){
            return;
        }

        if (isUnderwater)
        {
            Vector3 vector = Vector3.up * buoyancyForce * -diffy;
            thisRigidbody.AddForce(vector, ForceMode.Acceleration);
        }
        thisRigidbody.drag = isUnderwater ? underwaterDrag : airDrag;
        thisRigidbody.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;
    }
}
