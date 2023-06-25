using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        boxTurn();
    }
    void boxTurn()
    {
        transform.Rotate(new Vector3(30, 0, 0) * Time.deltaTime);
    }
}
