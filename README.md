# Photon-Multigame2D

유튜브 강의를 따라 하며 Photon PUN2로 실시간 멀티플레이를 붙여 본 Unity 학습용 저장소입니다.

- 인원: 1인
- 사용 기술: Unity / C# / Photon PUN2
- 참고한 강의: [2D 횡스크롤 멀티게임 만들기 - 처음부터 끝까지ㅣ포톤PUN2](https://www.youtube.com/watch?v=9Bn1C9O0hzY&list=PL3KKSXoBRRW3YE4UMnRH762vOhSHLdnpK)

## 코드 — `Assets/Scripts/`

| 파일 | 내용 |
|---|---|
| `NetworkManager.cs` | 접속, 방 참가, 플레이어 스폰, 리스폰 |
| `PlayerScript.cs` | 이동 · 점프 · 사격, 위치와 체력 동기화 |
| `BulletScript.cs` | 총알 진행 방향 전달과 충돌 시 제거 |

- `MonoBehaviourPunCallbacks`를 상속해 `OnConnectedToMaster`, `OnJoinedRoom`
  콜백 시점에 로직을 붙였습니다.
- 방은 `JoinOrCreateRoom`으로 최대 6명짜리 방 하나를 씁니다.
  없으면 만들고 있으면 들어갑니다.
- 플레이어는 `PhotonNetwork.Instantiate`로 생성해 모든 클라이언트에 스폰됩니다.
  `PhotonView.IsMine`으로 내 캐릭터일 때만 입력을 받고 카메라를 붙입니다.
- 동기화는 `IPunObservable.OnPhotonSerializeView`에서 위치와 체력을 주고받습니다.
  받는 쪽은 매 프레임 `Lerp`로 보간해 끊겨 보이지 않게 하되,
  차이가 크게 벌어지면 보간하지 않고 바로 그 위치로 옮깁니다.
- 좌우 반전, 점프, 총알 방향, 오브젝트 제거는 `RPC`로 전파합니다.
  제거는 `RpcTarget.AllBuffered`를 써서 나중에 들어온 클라이언트에도 반영되게 했습니다.
- `SendRate` 60 / `SerializationRate` 30 으로 전송 주기를 조정했습니다.
- 로비에서 방 목록을 띄우는 단계까지는 만들지 않았습니다.
  방 이름이 고정되어 있어 접속하면 모두 같은 방으로 들어갑니다.
