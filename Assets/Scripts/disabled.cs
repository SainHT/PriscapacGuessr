using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disabled : MonoBehaviour
{
    public Image im;
    public Color col1;
    public Color col2;
    public bool isActive;
    public Animation anim;
    public GameObject logic;

    public bool startActive;

    int i = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        i=0;
        
        if (!startActive)
        {
            im.color = col1;
        }
    }

    public void Click()
    {
        i++;
        if (i%2==0)
        {
            SetInActive();
        }
        else
        {
            SetActive();
        }
    }
    
    // Update is called once per frame
    public void SetActive()
    {
        isActive = true;
        im.color = col2;
        anim.enabled=true;
        print(logic.GetComponent<marker>().players);
        logic.GetComponent<marker>().players++;
    }

        // Update is called once per frame
    public void SetInActive()
    {
        if (!startActive)
        {
            isActive = false;
            im.color = col1;
            anim.enabled=false;
            logic.GetComponent<marker>().players--;
        }
    }
}
