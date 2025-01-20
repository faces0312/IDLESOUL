Soul 키우기
=============
SCC_Unity_6기_9조_IdleSoul
-------------
![Icon_최종](https://github.com/user-attachments/assets/bbec7e8d-185d-4df8-9237-daecb9862440)

## [프로젝트 소개]
![타이틀씬 움짤](https://github.com/user-attachments/assets/3ea7d91d-bae9-46ed-b36f-7fb04bbae9a7)

**당신의 몸에는 영혼이 깃들 수 있습니다.
영혼의 힘을 이용해 몰려오는 적들을 물리치는 당신은 어디까지 나아갈 수 있을까요?**

- **장르 : 핵앤슬래시, 수집방치형, 2.5D**
- **레퍼런스 : 달토끼 키우기 , 원신의 캐릭터 교환 시스템**
- **특징 : 장착된 Soul(영혼) 고유의 스킬을 사용하며 적을 쓰러트리고, 적재 적소에 맞춰 Soul을 전환하여 진행하는 게임**

## 🏹 전투 시스템

### 화려한 스킬들로 끊임없이 몰려오는 적들을 물리치세요!
![전투 움짤](https://github.com/user-attachments/assets/ecea2d7f-247b-4876-b28f-91cf6dbb08d9)

---

## ☠️ 보스 시스템
![보스 패턴](https://github.com/user-attachments/assets/c0b0c5ef-7760-432e-aa33-c21e7b1cf28c)


### `다양한 패턴`을 가진 보스들을 물리치세요!

---

## 👻 소울 시스템

### 서로 다른 소울들의 **`고유 스킬`**을 통해 효율적으로 적을 물리치세요!
![image](https://github.com/user-attachments/assets/751bcabb-e660-45f9-acf7-426f637380c7)

---
## ✨ 성장 시스템

### **`플레이어의 스텟` , `아이템 강화` , `소울의 스킬 레벨업`을 통해 캐릭터의 성장 쾌감을 느껴보세요!**
![image](https://github.com/user-attachments/assets/ef7809e3-2421-4463-b618-b40944cf2a36)
![image](https://github.com/user-attachments/assets/c7189a8e-c6a3-4156-a1f1-a89f52c4d54b)
![image](https://github.com/user-attachments/assets/9dcf366d-9012-46a0-bc85-90faee5a15f0)

---
## 😴 방치형 게임

### Auto 모드를 켜서 골드와 다이아몬드 재화를 벌어 캐릭터를 성장시키세요!!

---

## [개발환경] 
C# / VisualStudio 2022 / Unity 2022.3.17f1

---

## [게임플레이 방법]
W,A,S,D : 캐릭터 이동  
N : 자동 사냥 ON/OFF 스위치  
J : 기본 스킬  
K : 궁극기 스킬  
1,2,3 : Soul 전환  
기타 UI 상호작용 : 마우스 클릭 및 모바일 터치 지원  

---
## [프로젝트 목표]
- 모바일 시장에서 강세인 **수집형,방치형**과 **핵앤슬래쉬**를 조합하여 하나의 게임을 개발하는것이 목표
- **배포(itch.io)를 목적**으로 **게임의 재미** 및 **확장성 있는 설계 구조**를 잡는것이 목표
- https://mssong94.itch.io/soulidle : 배포 링크

---
## [개발 기간]
**2024.11.25(월) ~ 2025.01.17(금) 약 7주**  
**개발 인원 : 4명**  
**브로셔 : https://teamsparta.notion.site/Idle-Soul-e78dacd5fd8049c08b49a6f489e1f667**  
<pre>
<code>
송명성 : Game Roop Cycle, DataManager, StageManager, Player, Inventory, Item, DropItem
--------------------------------------
박두산 : Achievement, Shop, Minimap, Loading, ObjectPoolManager, SceneDataManager
---------------------------------------
서현석 : GameManager, SoundManager, Enemy, EnemySkill, JoyStick
---------------------------------------
신현수 : Soul, PlayerSkill, PostProcessing , BigInteger , UIManager , Player & Soul Stat UI
---------------------------------------
</code>
</pre>
---
## [사용된 기술]
+ **MVC 패턴**
  + UI에 사용됨  
+ **façade(퍼사드) 패턴과 Strategy Pattern(전략)패턴**
  + Player와 Enemy 및 Projectile,Skill에 적용됨
+ **FSM(유한상태기계)**
  + Player와 Enemy의 동작로직에 사용됨  
+ **Spine(스파인)애니메이션**
  + Player의 애니메이션에 사용됨
+ **PostProcessing 과 CineMachine**
  + 스킬과 각종 Player,Enemy 등장 연출에 적용됨
+ **재사용스크롤 로직**
  + 도전과제,상점 스크롤을 사용하는곳에 적용됨
+ **JsonUtility**
  + Json을 이용한 외부 데이터 관리에 사용됨  
+ **ObjectPool**
  + Enemy 및 투사체,데미지 폰트 오브젝트 최적화에 사용됨 
+ **Singleton 패턴**
  + GameManager,DataManager,ObjectPollManager 등등에 사용됨
+ **BigInteger**
  + 범위를 벗어나는 숫자들 및 런타임시 숫자 가독성을 위해 사용됨
  + **ScottGarland 라이브러리를 사용함**
  + <라이브러리 링크> :  https://github.com/keiwando/biginteger 
+ **EventBus**
  + 도전과제 관리에 사용됨
+ **DOTween 에셋**
  + UI에 애니메이션에 사용됨

---

## [사용된 최적화 기법]
+ **동적 배칭**
+ **Stage의 배경 오브젝트에 정적(Static) 배칭**
+ **오브젝트 풀을 사용하여 메모리 최적화)**
