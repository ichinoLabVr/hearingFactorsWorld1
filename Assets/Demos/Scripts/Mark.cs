using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//visualFactorsWorld1
public class Mark : MonoBehaviourPunCallbacks
{
    private bool _isMark = false;
    public AudioSource audioSource;
    private Animator anim; // キャラにアタッチされるアニメーターへの参照

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetButtonDown("Master"))
        {	// 9キーを入力したら
            if (photonView.IsMine)
            {
                PhotonNetwork.SetMasterClient(photonView.Owner);
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    Debug.Log("Master Clientになりました");
                }
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
    private void ChangeMark()
    {
        _isMark = true;
        StartCoroutine("Blink");
        FileLog.AppendLog("log/log.txt", System.DateTime.Now.ToString() + " UserID=" + photonView.OwnerActorNr + " Reaction\n");
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