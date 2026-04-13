# DinoRun3D

Unity로 개발한 3D 공룡 러닝 게임입니다. 플레이어는 공룡 무리를 이끌고 앞으로 달리며, 문을 통과할 때마다 사칙연산이 적용되어 공룡 수가 변하면서 스테이지를 클리어합니다.

## 게임 소개

공룡 무리를 이끌고 달리는 3D 러너 게임입니다. 길 위에 배치된 두 개의 문 중 하나를 선택해 통과하면, 문에 표시된 연산(+, -, ×, ÷)이 공룡 무리 수에 적용됩니다. 공룡이 한 마리도 남지 않으면 게임 오버이며, 골 지점까지 살아남으면 스테이지 클리어입니다.

## 주요 기능

- **연산 문 시스템** — 덧셈, 뺄셈, 곱셈, 나눗셈이 공룡 무리 수에 실시간으로 적용
- **피보나치 나선 대형** — 공룡 무리가 황금각(137.508°)을 기반으로 나선형으로 배치
- **적 공룡 AI** — 탐지 범위 내 공룡을 쫓아오는 적 AI
- **5개 스테이지** — ScriptableObject로 구성된 스테이지 데이터, 진행 상황은 PlayerPrefs에 저장
- **프로시저럴 맵 조합** — 맵 프리팹을 순서대로 조합해 스테이지 구성

## 조작 방법

| 키 | 동작 |
|---|---|
| `←` / `→` 방향키 | 좌우 이동 |

공룡은 자동으로 앞으로 달립니다.

## 게임 흐름

```
타이틀 화면
    ↓ START 버튼
게임 진행
    ├─ 문 선택 → 연산 적용 → 공룡 수 변화
    ├─ 적 공룡 회피
    ├─ 공룡 전멸 → 게임 오버
    └─ 골 도달 → 스테이지 클리어 → 다음 스테이지
```

## 프로젝트 구조

```
Assets/
├── 01.Scenes/          # GameScene (단일 씬 구조)
├── 02.Scripts/         # 게임 로직 스크립트
├── 04.Sounds/          # 효과음 WAV 파일
├── 05.Prefabs/         # 게임 오브젝트 프리팹
├── 07.StageData/       # 스테이지 ScriptableObject (Stage0~4)
├── 99.Fonts/           # DNFBitBitv2 TMP 폰트
├── ComicCreator/       # 서드파티 에셋
└── FerociousIndustries/PBRDinosaurs/  # 공룡 3D 모델
```

## 주요 스크립트

| 스크립트 | 역할 |
|---|---|
| `DinoController.cs` | 플레이어 이동, 문/골 충돌 감지 |
| `DinoPositionController.cs` | 공룡 무리 수 관리, 피보나치 나선 배치 |
| `GameManager.cs` | 게임 상태(시작/진행/오버/클리어), UI 패널 관리 |
| `MapManager.cs` | 스테이지 맵 프리팹 순차 로드 및 배치 |
| `SelectDoors.cs` | 문 연산 설정 및 시각적 표시 |
| `Enermy.cs` | 적 공룡 AI (탐지 및 추격) |
| `SoundManager.cs` | 효과음 재생 관리 |
| `DinoFollowCamera.cs` | 3인칭 카메라 추적 |
| `StageScriptableObject.cs` | 스테이지 데이터 컨테이너 |

## 개발 환경

- **Unity** 2022.3.62f1 (LTS)
- **렌더 파이프라인** Built-in
- **플랫폼** Windows

## 스크린샷

> *(스크린샷 추가 예정)*

## 라이선스

이 프로젝트에 사용된 에셋의 라이선스는 각 에셋 폴더의 라이선스 파일을 참고하세요.
- `FerociousIndustries/PBRDinosaurs` — Ferocious Industries 라이선스 적용
- `ComicCreator` — 별도 라이선스 적용
