
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class U_TriggerRotazione : UdonSharpBehaviour
{
    public Transform porta;

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        SendCustomNetworkEvent(NetworkEventTarget.All, "ApriPorta");
    }
    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        SendCustomNetworkEvent(NetworkEventTarget.All, "ChiudiPorta");
    }
    public void ApriPorta()
    {
        porta.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
    }
    public void ChiudiPorta()
    {
        porta.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
