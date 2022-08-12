using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletScript : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    int dir;

    void Start() => Destroy(gameObject, 3.5f);

    void Update() => transform.Translate(Vector3.right * 7 * Time.deltaTime * dir);

    private void OnTriggerEnter2D(Collider2D collision) // collision�� RPC�� �Ű������� �Ѱ��� �� ����.
    {
        if (collision.tag == "Ground") PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        if(!PV.IsMine && collision.tag == "Player" && collision.GetComponent<PhotonView>().IsMine) // �����ʿ� ���缭 Hit����
        {
            collision.GetComponent<PlayerScript>().Hit();
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void DirRPC(int dir) => this.dir = dir;

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

}
