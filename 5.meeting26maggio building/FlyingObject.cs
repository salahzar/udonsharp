
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FlyingObject : UdonSharpBehaviour
{
    private VRCPlayerApi playerLocal;
    private bool isActive;
    public float maxvelocity = 3;
    
    void Start()
    {
        playerLocal = Networking.LocalPlayer;
    }

    private void OnPickupUseDown()
    {
        Debug.Log("set active true");
        isActive = true;
    }
    private void OnPickupUseUp()
    {
        Debug.Log("set active false");
        isActive = false;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Vector3 newvel = Vector3.ClampMagnitude(playerLocal.GetVelocity() + transform.forward, maxvelocity);
            Debug.Log("setting velocity to " + newvel);
            playerLocal.SetVelocity(newvel);
        }
    }
}
