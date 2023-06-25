using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class butonprocess : MonoBehaviour
{
    public Button Jump;

   
    private void Start()
    {
        Jump.gameObject.GetComponent<Button>().onClick.AddListener(JumpProcess);

        
    }
    void JumpProcess()
    {
        Rigidbody playerRigid = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerRigid.AddForce(0, 200, 0);
    }
}
