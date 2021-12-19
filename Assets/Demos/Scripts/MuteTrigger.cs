using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MuteTrigger : MonoBehaviourPunCallbacks
{
    GameObject Echo;
    GameObject Echoloop;
    GameObject nierobj;
    GameObject[] Speaker;
    GameObject[] SpeakerMute;
    AudioSource audioSource;
    GameObject nierroot;
    bool EchoSwitch = true;
    private void Start()
    {
        nierobj = this.GetComponent<NierSP>().nier();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            nierobj = this.GetComponent<NierSP>().nier();
            Speaker = GameObject.FindGameObjectsWithTag("Speaker");
            SpeakerMute = GameObject.FindGameObjectsWithTag("SpeakerMute");

            foreach (GameObject Speakers in Speaker)
            {
                if (nierobj.name == Speakers.name && EchoSwitch)
                {
                    Echoloop = GameObject.Find(Speakers.name + "/Echo");
                    audioSource = Echoloop.GetComponent<AudioSource>();
                    audioSource.mute = false;
                }
                else
                {
                    Echoloop = GameObject.Find(Speakers.name + "/Echo");
                    audioSource = Echoloop.GetComponent<AudioSource>();
                    audioSource.mute = true;
                }
            }
            foreach (GameObject Speakers in SpeakerMute)
            {
                if (nierobj.name == Speakers.name && EchoSwitch)
                {
                    Echoloop = GameObject.Find(Speakers.name + "/Echo");
                    audioSource = Echoloop.GetComponent<AudioSource>();
                    audioSource.mute = false;
                }
                else
                {
                    Echoloop = GameObject.Find(Speakers.name + "/Echo");
                    audioSource = Echoloop.GetComponent<AudioSource>();
                    audioSource.mute = true;
                }
            }
            if (EchoSwitch)
            {
                Echoloop = GameObject.Find(nierobj.name + "/Echo");
                audioSource = Echoloop.GetComponent<AudioSource>();
                audioSource.mute = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "SpeakerMute")
        {
            nierobj = this.GetComponent<NierSP>().nier();
            if (!photonView.IsMine)
            {
                other.gameObject.tag = "SpeakerMute";
                Echo = GameObject.Find(other.name + "/Echo");
                Echo.gameObject.tag = "SpeakerEchoMute";
            }
            if (photonView.IsMine)
            {
                EchoSwitch = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nierobj = this.GetComponent<NierSP>().nier();
        if (!photonView.IsMine)
        {
            other.gameObject.tag = "Speaker";
            Echo = GameObject.Find(other.name + "/Echo");
            Echo.gameObject.tag = "SpeakerEcho";
        }
        if (photonView.IsMine)
        {
            EchoSwitch = true;
        }
    }
}