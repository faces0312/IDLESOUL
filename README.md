Soul 키우기
=============
SCC_Unity_6기_9조_IdleSoul
-------------

## [프로젝트 소개]
![image](https://github.com/user-attachments/assets/498db46a-1225-4d20-8987-aa990e31a2e2)

**최종프로젝트 :: 2.5D RPG 방치 수집형 캐릭터 키우기**

**각종 영혼들을 수집하여 , 강해지고 화려한 스킬을 써 적을 물러트려라!!
방치 수집형 캐릭터 키우기 !! Soul 키우기 !!**

## [개발환경] 
C# / VisualStudio2022 / Unity 2022.3.17f1

---

## [게임플레이 방법]
> 게임 플레이 화면의 UI , 버튼을 눌러 사용하면됩니다.  
**조이스틱은 현재 키보드로 조작이 불가능**하여 **마우스로 조작** 부탁드립니다.
![image](https://github.com/user-attachments/assets/8ee0b4fe-01dc-4857-a4a5-dd669d87c794)

> Soul 편성 조작 방법
![image](https://github.com/user-attachments/assets/ab7eb06e-c7c8-4a05-b5bb-2e832aee3979)


---
## [프로젝트 목표]
- 모바일 시장에서 강세인 **수집형,방치형**과 **서브컬쳐**를 조합하여 하나의 게임을 개발하는것이 목표
- **배포(itch.io)를 목적**으로 **게임의 재미** 및 **확장성 있는 설계 구조**를 잡는것이 목표

---
## [개발 기간]
**2024.11.25(월) ~ 202412.20(금) 약 4주**
**개발 인원 : 4명**
<pre>
<code>
송명성 : Game Roop Cycle, DataManager, StageManager, Player, Inventory, Item  
--------------------------------------
박두산 : Achievement, Shop, Minimap, Loading, ObjectPoolManager,SceneDataManager  
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
