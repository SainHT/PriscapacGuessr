using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    public GameObject sel;
    public GameObject inGame;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void animEnd()
    {
        sel.SetActive(false);
        inGame.SetActive(true);
    }
}
