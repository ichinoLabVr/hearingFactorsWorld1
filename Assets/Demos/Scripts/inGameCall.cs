using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class inGameCall : MonoBehaviourPunCallbacks
{
    // 他プレイヤーがルームへ参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("現在 " + PhotonNetwork.CurrentRoom.PlayerCount + "名");
    }

    // 他プレイヤーがルームから退出した時に呼ばれるコールバック
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("現在 " + PhotonNetwork.CurrentRoom.PlayerCount + "名");
    }
}