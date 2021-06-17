
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;


public class UdonSharp_ClickToggleSound : UdonSharpBehaviour
{    
    private AudioSource TheAudio;


    public void Start() { 
        TheAudio = gameObject.GetComponent<AudioSource>();
    }

    public override void Interact() {
		SendCustomNetworkEvent(NetworkEventTarget.All,"Suona");
	}

    
	public void Suona() {
		Debug.Log("Called Suona");
		if (TheAudio.isPlaying) {						
			TheAudio.Stop();
		} else {
			TheAudio.Play();
		}
	}
    
}
