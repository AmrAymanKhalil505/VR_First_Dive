using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class DrawerInterctable : XRGrabInteractable
{
    [SerializeField] Transform drawerTransform;
    [SerializeField] XRSocketInteractor keySocket;
    [SerializeField] bool isLocked;
    private Transform parentTransform;
    private const string defaultLayer = "Default";
    private const string grabLayer = "Grab";
    private bool isGrabbed;
    private Vector3 limitPositions;
    [SerializeField] float drawerLimitZ = 0.8f;
    [SerializeField] private Vector3 limitDistances = new Vector3(.02f,.02f,0);
    [SerializeField] private float differanceTranDrawer = 0.1f;
    
    void Start()
    {   
        if(keySocket != null)
        {
            keySocket.selectEntered.AddListener(OnDrawerUnlocked);
            keySocket.selectExited.AddListener(OnDrawerLocked);
        }
        parentTransform = transform.parent.transform;
        limitPositions = drawerTransform.localPosition;
    }
    private void OnDrawerLocked(SelectExitEventArgs arg0)
    {
        isLocked = true;
        Debug.Log("****DRAWER LOCKED");
    }
    private void OnDrawerUnlocked(SelectEnterEventArgs arg0)
    {
        isLocked = false;
        Debug.Log("****DRAWER UNLOCKED");
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if(!isLocked)
        {
           transform.SetParent(parentTransform);
           isGrabbed = true;                     
        }
        else
        {
            ChangeLayerMask(defaultLayer);
        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);        
        ChangeLayerMask(grabLayer);   
        isGrabbed = false; 
        transform.localPosition = drawerTransform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if(isGrabbed && drawerTransform != null)
        {
            drawerTransform.localPosition = new Vector3(drawerTransform.localPosition.x
            , drawerTransform.localPosition.y, transform.localPosition.z);

            CheckLimits();
        }
    }

    private void CheckLimits()
    {
        if (isAway())
        {
            ChangeLayerMask(defaultLayer);
        }
        // if(transform.localPosition.x >= limitPositions.x + limitDistances.x ||
        //     transform.localPosition.x <= limitPositions.x - limitDistances.x)
        // {
        //     ChangeLayerMask(defaultLayer);
        // }
        // else if(transform.localPosition.y >= limitPositions.y + limitDistances.y ||
        //     transform.localPosition.y <= limitPositions.y - limitDistances.y)
        // {
        //     ChangeLayerMask(defaultLayer);
        // }
        else if(drawerTransform.localPosition.z <= limitPositions.z - limitDistances.z)
        {
            // isGrabbed = false;
            drawerTransform.localPosition = limitPositions;
            // ChangeLayerMask(defaultLayer);
        }
        else if(drawerTransform.localPosition.z >= drawerLimitZ + limitDistances.z)
        {
            // isGrabbed = false;
            drawerTransform.localPosition = new Vector3(
                drawerTransform.localPosition.x,
                drawerTransform.localPosition.y,
                drawerLimitZ
            );
            // ChangeLayerMask(defaultLayer);
        }
    }

    private void ChangeLayerMask(string mask)
    {
        interactionLayers = InteractionLayerMask.GetMask(mask);
    }

    private bool isAway()
    {
        return Vector3.Distance(transform.position, drawerTransform.position) > differanceTranDrawer;
    }

}
