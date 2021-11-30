using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//visualFactorsWorld1
public class Mark : MonoBehaviourPunCallbacks
{
    private bool _isMark = false;
    public GameObject mark;
    public GameObject mark1;
    public GameObject mark2;
    public GameObject PhotonController;
    public RandomMatchMaker script;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // int myId = photonView.ViewID;
        // Debug.Log(myId);
        PhotonController = GameObject.Find("PhotonController");
        script = PhotonController.GetComponent<RandomMatchMaker>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Mark"))
        {	// 1キーを入力したら
            if (photonView.IsMine)
            {
                photonView.RPC("ChangeMark", RpcTarget.All);
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
        Logger logger = new Logger(System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString() + "-" + System.DateTime.Now.Second.ToString() + "-" + System.DateTime.Now.Millisecond.ToString() + "-MarkLog-No" + PhotonNetwork.CurrentRoom.PlayerCount + ".txt");

        logger.Log(" : UserID=" + PhotonNetwork.CurrentRoom.PlayerCount + " 1ボタンを押した");
        logger.Close();
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
