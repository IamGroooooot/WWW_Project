using System.Collections;
using System.Collections.Generic;

public class WPEnum {

    // 게임데이터 저장 타입
    public enum VaraibleType
    {
        eUserMoney = 0,                 // 돈

        eUserTerritory = 100,           // 영지 개수

        eUserWorkerCount = 200,         // 일꾼 개수

		eUserDebt = 300,				// 빚

		eUserDate_Year  = 400,          // 게임 상의 시간 년

		eUserDate_Month = 500,          // 게임 상의 시간 월

		eUserDate_Day = 600,			// 게임 상의 시간 일
	}

	// 재정 상태
	public enum Financial_State
	{
		eBankStable = 0,
		eBankruptcy = 1,
	}

	// 액터 움직입 타입
    public enum ActorMoveType
    {
        eMoveNone = 0,                  // 안 움직이는 상태
        eMoveRoaming = 1,               // 로밍. 무작위로 움직이는 상태
    }
	
	// return value 타입
	public enum rvType
	{
		eTypeSuccess = 0,				// 무난히 성공
		eTypeFail = 1,					// 치명적인 실패
	}

	// 액터키 모음. 추후 이걸 데이터로 빼는작업 필요.
	public enum ActorKey
	{
		eActorWorkerTemp = 10,			// 임시 워커
		eActorField = 20,				// 밭
	}

	// 현재 액터의 상태를 정의해놓음.
	public enum ActorState
	{
		eActorStateNone = 0,            // 아무상태도 아님
		eActorStateIdle = 1,            // 대기상태
		eActorStateMoving = 2,			// 움직이는 상태
	}

    // 계절
    public enum Season
    {
        eSpring = 1,
        eSummer,
        eAutumn,
        eWinter,
    }

	// 기후 
    public enum Weather					//버프 (일 당 +점수), 디버프 (일 당 -점수)
    {
        eSunny,							//화창 버프2, 디버프 1  
        eDrought,						//가뭄 버프4, 디버프 3
        eRain,							//비   버프3, 디버프 3
        eCold,							//눈   버프4, 디버프 3
    }

	// Initial Date(게임 시작했을 때 날짜)
	public enum InitialDate
	{
		eInitYear = 2019,
		eInitMonth = 3,
		eInitDay = 1,
	}

}
