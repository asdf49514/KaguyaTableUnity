using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] statPointButtons;
    public List<GameObject> skillButtons;
    public GameObject rebirthButton;

    private Text descTextField;

    private void Start()
    {
        statPointButtons = GameObject.FindGameObjectsWithTag("StatPointUIButton");
        skillButtons     = GameObject.FindGameObjectsWithTag("SkillButton").ToList();

        rebirthButton = GameObject.Find("RebirthButton");
        
        descTextField = GameObject.Find("DescTextField").GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var eventButton = eventData.pointerEnter.GetComponentInParent<Button>().gameObject;
        Debug.Log(eventButton.name);

        var description = eventButton.name switch {
            "TacticsButton"      => "Improves damage dealt to table.",
            "RAGE!!!Button"      => "Increases maximum damage dealt to table.",
            "NEETeryButton"      => "Improves the amount of xp gained per damage.",
            "BossinessButton"    => "Does nothing, LIKE A BOSS.",
            "ImpatienceButton"   => "Amount of points allocated per click.",
            "HardcorenessButton" => "Increases table defense. Costs 10,000 Stat Points.",
            
            "Skill1Button" => "Returns all points spent on impatience.",
            "Skill2Button" => "Continually levels until xp is depleted.",
            "Skill3Button" => "Cuts amount of xp needed for mastery by 10%",

            "MasteryButton" => "Drop all XP into Mastery to gain Mastery levels!",
            "RebirthButton" => "Resets all levels and stats. Increases stats per level by 1.",

            _ => throw new ArgumentOutOfRangeException($"Invalid value: \'{eventButton.name}\'")
        };

        descTextField.text = description;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        var eventButton = eventData.pointerEnter.GetComponentInParent<Button>().gameObject;
        Debug.Log(eventButton.name);

        if(statPointButtons.Any(button => button == eventButton) || 
           skillButtons.Any(button => button == eventButton) || 
           rebirthButton == eventButton) {
            descTextField.text = "";
        }
    }
}
