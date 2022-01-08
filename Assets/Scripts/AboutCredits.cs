using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AboutCredits : MonoBehaviour
{
    public GameObject aboutCreditsWindow;
    
    void Start()
    {
        aboutCreditsWindow = GameObject.Find("AboutCreditsWindow");
    }

    public void AboutCreditsButton_Click()
    {
        aboutCreditsWindow.SetActive(!aboutCreditsWindow.activeSelf);
    }
    void Update()
    {
        
    }
}
