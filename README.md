# Unity_2st_Homework
 3D게임

[ 필수구현 ]

Q1 **기본 이동 및 점프**
`Input System`, `Rigidbody ForceMode` (난이도 : ★★☆☆☆)

구현 완료 - 점프, 카메라 이동, 플레이어 이동 을 input system으로 구현

Q2 **체력바 UI** `UI` (난이도 : ★★☆☆☆)

구현완료 - UI 캔버스에 체력바를 추가하고 플레이어의 체력을 나타내도록 설정. 플레이어의 체력이 변할 때마다 UI 갱신.
        
Q3 **동적 환경 조사** `Raycast` `UI` (난이도: ★★★☆☆)

구현완료 - Raycast를 통해 플레이어가 조사하는 오브젝트의 정보를 UI에 표시.
        - 예) 플레이어가 바라보는 오브젝트의 이름, 설명 등을 화면에 표시.

Q4 **점프대** `Rigidbody ForceMode` (난이도 : ★★★☆☆)

구현완료 - 캐릭터가 밟을 때 위로 높이 튀어 오르는 점프대 구현
    - **OnCollisionEnter** 트리거를 사용 **ForceMode.Impulse**를 사용

Q5 **아이템 데이터** `ScriptableObject` (난이도 : ★★★☆☆)

구현완료 - 다양한 아이템 데이터를 `ScriptableObject`로 정의. 각 아이템의 이름, 설명, 속성 등을 `ScriptableObject`로 관리

Q6 **아이템 사용** `Coroutine` (난이도 : ★★★☆☆)

구현완료 - 특정 아이템 사용 후 효과가 일정 시간 동안 지속되는 시스템 구현
        - 예) 아이템 사용 후 일정 시간 동안 스피드 부스트.


[ 도전구현 ]

+ 추가 UI
    점프나 대쉬로 스태미나 표시하는 바 구현 (배고픔을 쉴드로 바꿈)

+ 다양한 아이템 구현
    음식 이외에도 스피드 부스트, 더블 점프 구현


    => 리드미를 이렇게 쓰는게 맞나 싶군요..
       커밋은 제때제때 잘 해내어서 깔끔하니 보기 좋습니다.   