using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PenguinAutoMove();
    }

    private void PenguinAutoMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 10f);
    }
    
    // Disable the behaviour when it becomes invisible...
    void OnBecameInvisible()
    {
        print("INVISIBLE");
        enabled = false;
    }

    // ...and enable it again when it becomes visible.
    void OnBecameVisible()
    {
        print("VISIBLE");
        enabled = true;
    }
}
