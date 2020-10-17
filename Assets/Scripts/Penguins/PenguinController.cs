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
        transform.Translate(Vector3.forward * Time.deltaTime * PlayerManager.Instance.moveSpeed);
    }
}
