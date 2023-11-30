using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private InputActionProperty touchpad;
    // [SerializeField] private InputActionProperty uiInteactionSelect;
    private float uiInteactionHoverValue;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();
        Vector2 TouchPadValue = touchpad.action.ReadValue<Vector2>();
        
        
        anim.SetFloat("Trigger",triggerValue);
        anim.SetFloat("Grip",gripValue);
        anim.SetFloat("PointFinger",Mathf.Max(TouchPadValue.y, uiInteactionHoverValue));
    }

    public void setUiInteactionHoverValue(float uiInteactionHoverValue)
    {
        this.uiInteactionHoverValue = uiInteactionHoverValue;
    }
}
