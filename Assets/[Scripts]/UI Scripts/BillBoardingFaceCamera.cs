using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardingFaceCamera : MonoBehaviour
{

    public Transform targetLook;
    // Start is called before the first frame update
    void Start()
    {
        if(!targetLook)
        {
            targetLook = GameObject.FindWithTag("MainCamera").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(2 * transform.position - targetLook.position);
    }
}
