using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settingWindow;
    
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        settingWindow = GameObject.Find("SettingWindow");
        
        settingWindow.SetActive(false);

        
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();  
        
        //var options = resolutions.Select(resolution => resolution.width + "x" + resolution.height).ToList();
        var options = new List<string>();
        var curResolutionIndex = 0;
        for(var i = 0; i < resolutions.Length; i++) {
            var resolution = resolutions[i];
            var option = resolution.width + "x" + resolution.height;
            options.Add(option);

            if(resolution.width != Screen.width || resolution.height != Screen.height) continue;
            curResolutionIndex = i;
            break;
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = curResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        //https://www.youtube.com/watch?v=YOaYQrN1oYQ&t=94s
    }

    public void SetResolution()
    {
        Debug.Log("SetResolution Executed");
        var resolution = resolutions[resolutionDropdown.value];
        Debug.Log(resolution);
        Screen.SetResolution(resolution.width, resolution.height, false);
    }

    public void SettingsButton_Click()
    {
        settingWindow.SetActive(!settingWindow.activeSelf);
    }

    public void CloseButton_click()
    {
        settingWindow.SetActive(false);
    }
    
    
    void Update()
    {
        
    }
}
