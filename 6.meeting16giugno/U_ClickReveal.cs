
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class U_ClickReveal : UdonSharpBehaviour
{
    public GameObject Reveal;
    
     public override void Interact() {
		SendCustomNetworkEvent(NetworkEventTarget.All,"Rivela");
	}
    public void Rivela()
    {
        Reveal.SetActive(!Reveal.activeSelf);
    }
}
