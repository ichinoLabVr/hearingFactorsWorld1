using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Video;
using UnityEngine.Audio;
using System;

public class NierSP : MonoBehaviourPunCallbacks
{
    public float speed;
    public Rigidbody rb;
    private GameObject[] targets;
    GameObject[] SpeakerMute;
    GameObject[] Speaker;
    GameObject nierobj;
    Transform unitychanpos;
    Transform targetpos;
    Vector3 tarpos;
    Vector3 unipos;
    GameObject noisecube;
    Transform tf;
    AudioSource audioSource;
    public float span = 15f;
    private float currentTime = 0f;

    private GameObject closeEnemy;
    // Start is called before the first frame update
    void Start()
    {
    }
    public GameObject nier()
    {

        targets = GameObject.FindGameObjectsWithTag("Speaker");
        float closeDist = 1000;
        foreach (GameObject t in targets)
        {
            float tDist = Vector3.Distance(transform.position, t.transform.position);
            // もしも「初期値」よりも「計測した敵までの距離」の方が近いならば、
            if (closeDist > tDist)
            {
                // 「closeDist」を「tDist（その敵までの距離）」に置き換える。
                // これを繰り返すことで、一番近い敵を見つけ出すことができる。
                closeDist = tDist;

                // 一番近い敵の情報をcloseEnemyという変数に格納する（★）
                closeEnemy = t;
            }
        }
        return closeEnemy;
    }
    void Nierlog()
    {
        float pos = Mathf.Sqrt(Mathf.Pow(tarpos.x - unipos.x, 2f) + Mathf.Pow(tarpos.z - unipos.z, 2f));
        FileLog.AppendLog("log/NierSP.txt", System.DateTime.Now.ToString() + " : " + nierobj.name + " UserID=" + photonView.OwnerActorNr + " : " + pos + "unitychan : " + unipos + "\n");
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb.IsSleeping())
        {
            nierobj = nier();
            unitychanpos = this.transform;
            targetpos = nierobj.transform;
            unipos = unitychanpos.position;
            tarpos = targetpos.position;

        }

        currentTime += Time.deltaTime;
        if (currentTime > span)
        {
            currentTime = 0f;
            Nierlog();
        }

        if (photonView.IsMine)
        {
            Speaker = GameObject.FindGameObjectsWithTag("Speaker");
            SpeakerMute = GameObject.FindGameObjectsWithTag("SpeakerMute");

            foreach (GameObject Speakers in Speaker)
            {
                if (Speakers.name == nierobj.name)
                {
                    audioSource = Speakers.GetComponent<AudioSource>();
                    audioSource.mute = false;
                }
                else
                {
                    audioSource = Speakers.GetComponent<AudioSource>();
                    audioSource.mute = true;
                }
            }
            foreach (GameObject Speakers in SpeakerMute)
            {
                if (Speakers.name == nierobj.name)
                {
                    audioSource = Speakers.GetComponent<AudioSource>();
                    audioSource.mute = false;
                }
                else
                {
                    audioSource = Speakers.GetComponent<AudioSource>();
                    audioSource.mute = true;
                }
            }
        }
    }
}