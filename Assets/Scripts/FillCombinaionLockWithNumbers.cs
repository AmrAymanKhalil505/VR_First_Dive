using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class FillCombinaionLockWithNumbers : MonoBehaviour
{
    [SerializeField] private GameObject numberBtn;
    [SerializeField] private Transform panelBtns;
    [SerializeField] private TMP_Text userInputText;
    
    [SerializeField] private TMP_Text Un_LockedText;
    
    
    private int [] userInputedDigits;
    private int index = 0;
    
    void Start()
    {
        userInputedDigits = new int[]{0,0,0,0 } ;
        
        for (int i = 1; i <= 9; i++)
        {
            GameObject buttonGO = Instantiate(numberBtn, panelBtns);
            XrButtonInteractable button = buttonGO.GetComponent<XrButtonInteractable>();
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            
            // Set button number
            buttonText.text = i.ToString();
            buttonGO.transform.name = i.ToString();
            // Add a listener to the button (optional)
            int buttonNumber = i;
            button.selectEntered.AddListener(OnComboButtonPressed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {
        if (index < userInputedDigits.Length)
        {
            userInputedDigits[index] = Int32.Parse(arg0.interactableObject.transform.name);
            index++;
            userInputText.text = string.Join(" ", userInputedDigits.Select(x => x.ToString()));
        }
      
    }

    public void restUserInputDigits()
    {
        index = 0;
        userInputedDigits = new int[]{0,0,0,0 };
        userInputText.text = string.Join(" ", userInputedDigits.Select(x => x.ToString()));
    }

    public void submit()
    {
        if (userInputText.text.Equals("5 2 3 8"))
        {
            Un_LockedText.text = "Unlocked";
        }
        else
        {
            Un_LockedText.text = "Locked";
        }

        restUserInputDigits();
    }
    

    
    
}
