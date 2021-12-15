using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Video;
using UnityEngine.Audio;

//visualFactorsWorld1
public class Mark : MonoBehaviourPunCallbacks
{
    private bool _isMark = false;
    public GameObject PhotonController;
    public RandomMatchMaker script;
    AudioSource audioSource;
    VideoPlayer videoPlayer;
    CreateSP SP;
    private Animator anim; // キャラにアタッチされるアニメーターへの参照

    // Start is called before the first frame update
    void Start()
    {
        PhotonController = GameObject.Find("PhotonController");
        script = PhotonController.GetComponent<RandomMatchMaker>();
        anim = GetComponent<Animator>(); // Animatorコンポーネントを取得する
    }

    void Update()
    {
        if (Input.GetButtonDown("Mark"))
        {	// 1キーを入力したら
            if (photonView.IsMine)
            {
                photonView.RPC("ChangeMark", RpcTarget.All);
                anim.SetBool("Rest", true);
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetButtonDown("Start"))
            {
                photonView.RPC("Audiostart", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void Audiostart()
    {
        if (photonView.IsMine)
        {
            //スピーカー再生
            GameObject PanelPlayer = GameObject.Find("panel");
            SP = PanelPlayer.GetComponent<CreateSP>();
            var videoPlayer = PanelPlayer.GetComponent<VideoPlayer>();
            foreach (GameObject i in SP.Sobj)
            {
                var audioSource = i.GetComponent<AudioSource>();
                audioSource.time = 0f;
                audioSource.Play();
            }
            //動画再生
            videoPlayer.time = 0f;
            videoPlayer.Play();
        }
    }


    [PunRPC]
    private void ChangeMark()
    {
        _isMark = true;
        StartCoroutine("Blink");
        FileLog.AppendLog("log/log.txt", System.DateTime.Now.ToString() + " UserID=" + PhotonNetwork.CurrentRoom.PlayerCount + " Reaction\n");
        audioSource.Play();
    }

    IEnumerator Blink()
    {
        if (_isMark)
        {
            yield return new WaitForSeconds(2.0f); //2秒待って
        }
    }
}
