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
## [프로젝트 목표]
- 모바일 시장에서 강세인 **수집형,방치형**과 **서브컬쳐**를 조합하여 하나의 게임을 개발하는것이 목표
- **배포(itch.io)를 목적**으로 **게임의 재미** 및 **확장성 있는 설계 구조**를 잡는것이 목표

---
## [개발 기간]
**2024.11.25(월) ~ 202412.20(금) 약 4주**
**개발 인원 : 4명**
<pre>
<code>
송명성 : Game Roop Cycle,DataManager,StageManager,Player,Inventory,Item  
--------------------------------------
박두산 : Achievement, Shop, Minimap, Loading, ObjectPoolManager,SceneDataManager  
---------------------------------------
서현석 : GameManager,SoundManager,Enemy,EnemySkill    
---------------------------------------
신현수 : Soul, PlayerSkill, PostProcessing , BigInteger , UIManager
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
+ **EventBus**
  + 도전과제 관리에 사용됨
+ **DOTween 에셋**
  + UI에 애니메이션에 사용됨

---
## [기능 구현]

### 1. 

---

### 2. 

---

### 3.

---

## [미구현 사항 및 이슈리스트]
>
1. **캐릭터의 능력치 로직 미구현**
> 아이템 장착 & 캐릭터 및 Soul 스텟 레벨업이 Player의 UserData와 연결만 된 상태, 능력치가 정상적으로 적용 되지않습니다.
2. **상점에서 제공되는 뽑기 아이템 및 아이템 구매시 Player의 인벤토리에 저장되지 않습니다.**
> 상점 및 뽑기 로직만 구현되어있으며 , Player의 데이터에 연결 되어있지 않습니다.
3. **스테이지가 10스테이지를 넘어갈경우 오류가 납니다.**
> StageDB의 데이터가 10개까지만 있으며 , 최종 스테이지 도달시 무한 모드로 진입은 미구현 상태입니다.
4. **같은 종류의 보스가 두번쨰 등장시 기본 공격(평타)를 쿨타임 없이 계속 사용합니다.**
> 보스가 패턴(스킬)을 사용하면 정상적으로 진행됩니다.
5. **게임 오버된 경우 연출에 UI창들이 비춰지며 , 마스크로 가려져 있는 부분도 조작이 가능합니다.**
> 마스크로 가려져 있어도 조이스틱이나, 스킬이 계속 사용되는 버그가 있습니다.
6. **TitleScene에서 런타임시 SkillSprite에 null값이 들어가는 버그가 간헐적으로 발생합니다.**
> ![image](https://github.com/user-attachments/assets/d6a2179a-267e-4ba8-8d9f-5f8c02ca37bf)
7. **상점 - 뽑기 - 소울,무기픽업,무기 일러스트 연결 안됨**
> ![image](https://github.com/user-attachments/assets/954fbcb4-e386-4310-a77a-f1509992dae3)
8. **상점 - 재화 교환 구현 X**
> 상점의 재화 교환은 현재 미구현 상태입니다.
9. **메뉴 버튼의 던전입장 버튼 미구현**
> ![image](https://github.com/user-attachments/assets/351942ea-88a4-4615-94e5-b055cd804052)
10.**인벤토리 - 선택된 아이템 - 아이템 강화하기 미구현**
> 아이템 강화하기 UI 버튼이 있지만 동작하지 않음
11.**인벤토리 - 선택된 아이템 - 아이템 장착하기 - 인벤토리에 장착UI 업데이트 미구현**
> 장착시에 인벤토리 UI가 업데이트되어 장착된 아이템이 바로 체크가 되야하지만 미구현 상태
12. **Soul 편성 구조는 되있으나 순서 변경및 교체 미구현**
> Soul의 종류가 3종류 + 더미가 2개 존재 , MVP 이후 테스트 및 구현 진행예정입니다.
13. **Soul 전환 UI 버튼 Sprite 별개 적용 미구현**
> ![image](https://github.com/user-attachments/assets/e3018057-f05f-4d67-bf28-13987dc4e155)
---
