using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDislay : MonoBehaviour
{

    public GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void afficheMenu()
    {
        go.SetActive(!go.active);
    }
}
