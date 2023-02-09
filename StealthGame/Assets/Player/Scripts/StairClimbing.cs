using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairClimbing : MonoBehaviour
{
    /*public float maxStepHeight = 0.4f;
    public float stepSearchOvershoot = 0.01f;

    List<ContactPoint> allCPs = new List<ContactPoint>();

    private void OnCollisionEnter(Collision collision)
    {
        allCPs.AddRange(collision.contacts);
    }

    private void OnCollisionStay(Collision collision)
    {
        allCPs.AddRange(collision.contacts);
    }

    private void FixedUpdate()
    {
        ContactPoint groundCP = default(ContactPoint);
        bool grounded = FindGround(out groundCP, allCPs);

        allCPs.Clear();

    }

    bool FindGround(out ContactPoint groundCP, List<ContactPoint> allCPs)
    {
        groundCP = default(ContactPoint);
        bool foundGround = false;
        foreach (ContactPoint cp in allCPs)
        {
            if (cp.normal.y > 0.0001f && (foundGround == false || cp.normal.y > groundCP.normal.y))
            {
                groundCP = cp;
                foundGround = true;
            }
        }

        return foundGround;
    }

    bool FindStep(out Vector3 stepUpOffset, List<ContactPoint> allCPs, ContactPoint groundCP)
    {
        stepUpOffset = default(Vector3);
        Vector2 velocityXZ = new Vector2()
    }*/

}
