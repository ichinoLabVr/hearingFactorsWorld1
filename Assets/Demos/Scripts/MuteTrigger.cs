using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MuteTrigger : MonoBehaviourPunCallbacks
{
    AudioSource audioSource;
    private bool _isStageChange = false;
    bool num = false;
    public string objName;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStageChange && num == true)
        {
            if (!photonView.IsMine)
            {//範囲に入ったからミュートにする
                GameObject SpeakerSound = GameObject.Find(objName);
                audioSource = SpeakerSound.GetComponent<AudioSource>();
                audioSource.mute = true;

                num = false;
            }
        }

        if (!_isStageChange && num == false)
        {
            if (!photonView.IsMine)
            {
                try
                {
                    //範囲を出たからミュート解除する
                    GameObject SpeakerSound = GameObject.Find(objName);
                    audioSource = SpeakerSound.GetComponent<AudioSource>();
                    audioSource.mute = false;
                }
                catch
                {
                    ;
                }
                num = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "onoffswitch")
        {
            if (!photonView.IsMine)
            {
                _isStageChange = true;
                objName = other.transform.root.gameObject.name;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //離れたオブジェクトのタグが"Player"のとき
        if (other.gameObject.tag == "onoffswitch")
        {
            if (!photonView.IsMine)
            {
                _isStageChange = false;
                objName = other.transform.root.gameObject.name;
            }
        }
    }
}
