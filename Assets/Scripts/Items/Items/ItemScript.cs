using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Rigidbody2D itemBody;

    public Transform itemTransform;

    //Applies a rotational force to the item
    public void applyTorque (float maxTorque) {
        itemBody.AddTorque(Random.Range(-maxTorque, maxTorque));
    }
}
