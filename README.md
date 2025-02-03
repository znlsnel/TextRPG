![Typing SVG](https://readme-typing-svg.demolab.com?font=Fira+Code&size=50&pause=1000&width=635&height=100&lines=Sparta+Text+RPG)
---

# Description
 - **프로젝트 소개** <br>
   C#으로 만든 Text RPG 입니다. 개인 프로젝트로 진행한 과제이지만, <br>
   팀 프로젝트로 진행을 한다면 어떻게 작업을 분할해야 할지 고민하며 코드를 작성했습니다. <br>
   클래스를 최대한 활용하여 최대한 깔끔하고, 확장이 용이한 형태로 구현해보는 것을 목표로 개발했습니다.<br><br>
 - **개발 기간** : 2025.01.27 - 2025.02.03
<br><br>

---
# 핵심 기능
- 플레이어 상태 보기<br>
- 인벤토리 시스템 (아이템 장착 관리)<br>
- 상점 (아이템 구매, 판매)<br>
- 휴식 기능<br>
- 던전 시스템<br>
- 게임 저장 기능<br>
<br><br>

---
# 와이어 프레임
![image](https://github.com/user-attachments/assets/49ea337e-3f32-4389-8fd5-753825e04f9b)
<br><br><br>

---
# 핵심 구현 로직 설명
 ## Scene 관리
  ![image](https://github.com/user-attachments/assets/f67b1080-9fa2-4b8d-ae7d-37c93d1657c8)
  - 각 기능들을 Scene 단위로 분할하여 작업 했습니다.<br><br>
  ![image](https://github.com/user-attachments/assets/7e947394-3b70-490d-a4e7-6b580b86c19e)<br>
  - Scene 클래스는 추상 클래스로 만들었으며 EnterScene 함수가 호출되면 각 기능들이 시작되도록 설계했습니다.<br>
  interface로도 구현이 가능하지만, interface의 경우 변수를 지정할 수 없다는 특징이 있기에 추상 클래스 기능을 이용했습니다. <br>
<br><br>

## 유저의 입력
 ![image](https://github.com/user-attachments/assets/5363a8ba-788f-4a0d-9518-004ed8579bed)
 - 게임 플레이 중에는 숫자를 통해 옵션을 선택하는 기능을 많이 사용하게 됩니다. <br>
   모든 곳에서 해당 기능을 구현하는 것은 유지 보수 측면에서 좋지 않다고 생각했고 <br>
   static 함수로 만들어서 자유롭게 호출할 수 있도록 구현했습니다.<br>
<br><br>

## 플레이어 데이터
   ![image](https://github.com/user-attachments/assets/7ed8a104-6f70-43c8-8005-61a8febded77)
   - 플레이어의 데이터는 모든 곳에서 접근이 가능해야 하기에 데이터를 관리하는 DataManager 클래스를 생성하고 singleton 구조로 만들었습니다.<br>
     해당 클래스를 통해서 ItemManager, Inventory, PlayerData에 접근이 가능 하도록 만들었습니다. <br>
     또한 Player Class(직업) 정보도 DataManager에서 관리를 하도록 설계했습니다.
<br>

### 
   ![image](https://github.com/user-attachments/assets/6547ff4d-f553-434c-8998-b37fea11c9b6)
  - Newtonsoft의 Json을 이용하여 데이터를 Json의 형태로 저장하고, 불러오는 기능을 구현했습니다.<br>
    보유중인 아이템의 경우 아이템의 이름이 저장되도록 하였습니다. ( **Dictionary<string(이름),Item>** 의 형태로 아이템을 관리하기에 이름만 저장했습니다.)
<br><br>

## 일정한 글자 간격
![image](https://github.com/user-attachments/assets/a99ffb84-5ba0-480f-82cd-84baed6fbeb9)
- 상점이나 인벤토리에 "|"을 통해 표시되는 내용의 섹션을 나누고 있는데, 이것을 일정한 간격이 유지되도록 구현했습니다.<br>
  스페이스 문자열을 추가하여 일정한 간격이 유지되도록 구현했는데도 생각대로 동작하지 않았습니다. <br>
  찾아보니 한글과 영문의 길이가 2배 차이가 나는 것이 이유라는 것을 알았습니다. <br>
 <br>
 
###
 ![image](https://github.com/user-attachments/assets/f7afaf97-3f66-4322-90f3-891090ac4607)
- 문자열에서 한글의 수를 구한 후 그 수에 맞게 스페이스 문자를 추가해서 간격을 맞췄습니다.
<br><br>

## 직업별 아이템
![image](https://github.com/user-attachments/assets/0d130d18-4373-4c90-9d76-9aaf2017e87a)<br>
![image](https://github.com/user-attachments/assets/23ca68ad-6219-42da-8b2a-00fb1a3b41ec)
- 아이템에 직업 조건을 추가했습니다.
<br>

### 
![image](https://github.com/user-attachments/assets/6b39cf97-dd10-4737-a417-4a776cf99593)
- Enum으로 만든 ClassType에 비트 값을 넣었습니다.
 <br>
 
###
![image](https://github.com/user-attachments/assets/a75c1ba7-a080-4625-94f1-e4344497fdf9)
- 이후 Item Class에서 플레이어의 직업 type을 인자로 받은 후 착용할 수 있는지 체크하는 로직을 만들었습니다.
<br><br>

## 무기와 장비
![image](https://github.com/user-attachments/assets/e5836e5a-fd20-4ca7-b2a6-af17a9a71a9a)
- 무기와 장비는 Item을 상속 하였으며, 속성의 이름을 반환하는 가상 함수만 구현했습니다. <br>
<br>

###
![image](https://github.com/user-attachments/assets/facd8b8e-6b12-4f87-b618-90734413df9a)
![image](https://github.com/user-attachments/assets/13d69cee-0508-447f-b052-2c7b1975db5f)
- 아이템을 장착할 때, Weapon class인지 Armor 클래스인지 체크하는 방식으로 아이템 타입을 구분했습니다.
<br><br>


# 감사합니다.
  

 

