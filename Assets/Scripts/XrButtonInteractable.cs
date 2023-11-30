using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class XrButtonInteractable : XRSimpleInteractable
{
    [SerializeField] Image buttonImage;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color selectedColor;
    private bool isPressed;
    void Start()
    {
        buttonImage.color = normalColor;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        isPressed = false;
        buttonImage.color = highlightedColor;
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        if(!isPressed)
        {
            buttonImage.color = normalColor; 
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isPressed = true;
        buttonImage.color = pressedColor;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        buttonImage.color = selectedColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
    

}
