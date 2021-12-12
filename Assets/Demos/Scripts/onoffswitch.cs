using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class onoffswitch : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    AudioSource audioSource;
    GameObject[] SpeakerSound;
    AudioEchoFilter _echo;
    private bool _isStageChange = false;
    bool onof = false;
    public string objName;
    public List<string> muteList = new List<string>();
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SpeakerSound = GameObject.FindGameObjectsWithTag("Speaker");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStageChange && onof == true)
        {
            if (photonView.IsMine)
            {
                //範囲に入ったからミュートにする
                muteList.Clear();
                foreach (GameObject SpeakerSounds in SpeakerSound)
                {
                    _echo = SpeakerSounds.GetComponent<AudioEchoFilter>();
                    audioSource = SpeakerSounds.GetComponent<AudioSource>();
                    _echo.enabled = false;
                    if (audioSource.mute)
                    {
                        muteList.Add(SpeakerSounds.name);
                    }
                    else if (SpeakerSounds.name != objName)
                    {
                        audioSource.mute = true;
                    }
                }

                onof = false;
            }
        }

        if (!_isStageChange && onof == false)
        {
            if (photonView.IsMine)
            {
                //範囲を出たからミュート解除する
                foreach (GameObject SpeakerSounds in SpeakerSound)
                {
                    if (photonView.IsMine)
                    {
                        audioSource = SpeakerSounds.GetComponent<AudioSource>();
                        audioSource.mute = false;

                        //スピーカーから離れたとき聞こえにくくする処理
                        _echo = SpeakerSounds.GetComponent<AudioEchoFilter>();
                        _echo.enabled = true;
                    }
                    foreach (string muteLists in muteList)
                    {
                        if (SpeakerSounds.name == muteLists)
                        {
                            audioSource.mute = true;
                        }
                    }
                }
                Debug.Log("a");

                onof = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "onoffswitch")
        {
            if (photonView.IsMine)
            {
                _isStageChange = true;
                objName = other.transform.root.gameObject.name;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //離れたオブジェクトのタグが"onoffswitch"のとき
        if (other.gameObject.tag == "onoffswitch")
        {
            if (photonView.IsMine)
            {
                _isStageChange = false;
                objName = other.transform.root.gameObject.name;
            }
        }
    }
}
