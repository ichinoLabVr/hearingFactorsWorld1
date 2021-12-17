using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class scale : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 tmp;
    Vector3 tmp2;
    void Start()
    {
        tmp = GameObject.Find("Cube").transform.position;
        tmp2 = GameObject.Find("Cube2").transform.position;
    }
    public void Scale()
    {
        float y = tmp.y;
        float z = tmp.z;
        float yy = tmp2.y;
        float zz = tmp2.z;

        GameObject.Find("Cube").transform.position = new Vector3(1.22f + (PhotonNetwork.CurrentRoom.PlayerCount / 8) * 1.5f, tmp.y, tmp.z);

        GameObject.Find("Cube2").transform.position = new Vector3(1.22f + (PhotonNetwork.CurrentRoom.PlayerCount / 8) * 1.5f, tmp2.y, tmp2.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
