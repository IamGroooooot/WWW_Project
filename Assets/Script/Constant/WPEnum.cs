﻿using System.Collections;
using System.Collections.Generic;

public class WPEnum {

    // 게임 데이터의 타입
    public enum GameData
    {
        eSeed,
        eNews,
        eFertilizer,
        eEvent
    }


    // 비교연산자 타입
    public enum CompareType
	{
		eEqual = 0,                     // 같음
		eLess = 1,                      // 이하
		eMore = 2,                      // 이상
		eUnder = 3,                     // 미만
		eOver = 4,                      // 초과
		eNot = 5                        // 다름
	}

    public enum IsAble2Pay
    {
        eEnoughMoney,
        eNoMoney,
    }

    public enum Header_Seed
    {
        ERROR = 0,
        eID,
        eName,
        eDataName,
        eGrowthTime,
        eComparePrice,
        eSalePrice,
        eScoreIncreaseRatio,
        eBuffWeather,
        eDebuffWeather,
        eUnlockLevel,
        eDescription
    }

    public enum Header_Fertilizer
    {
        ERROR = 0,
        eID,
        eName,
        eDataName,
        eItemType,
        eTier,
        eValue,
        ePrice,
        eDescription
    }

    public enum Header_News
    {
        ERROR = 0,
        eID,
        eName,
        eDescription
    }

    public enum Header_Event
    {
        ERROR = 0,
        eID,
        eMonth,
        eNewsInfo
    }

    public enum FertilizerType
    {
        ERROR = 0,
        eGrowth,
        eScore,
        eWorkmanship,
        eWeatherResist
    }

    // 게임데이터 저장 타입
    public enum VariableType
    {
        eUserMoney = 0,                 // 돈

		eFarmFieldCount = 100,          // FarmField 개수 

		eUserWorkerCount = 200,         // 일꾼 개수'

        eUserFertilizer = 201,           // 인벤토리 ( 비료 )

		eUserDebt = 300,				// 빚

		eUserDate = 400,                // 게임 상의 시간

        eDebtTimer = 500,

        eField0 = 800,
        eField1 = 900,
        eField2 = 1000,
        eField3 = 1100,
        eField4 = 1200,
        eField5 = 1300,
	}

	// 재정 상태
	public enum Financial_State
	{
		eBankStable = 0,				//안파산
		eBankruptcy = 1,				//파산
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
        eActorNullWorker = 10,          // 임시 워커
        eActorFarmField = 20,           // 밭
        eActorWorker = 30,              // 워커
    }

    // 현재 액터의 상태를 정의해놓음.
    public enum ActorState
	{
		eActorStateNone = 0,            // 아무상태도 아님
		eActorStateIdle = 1,            // 대기상태
		eActorStateMoving = 2,			// 움직이는 상태
		eActorTrkingSickness =3,		// 병충해를 제거하려고 움직이는 상태
        eSeed_Empty,


	}

    // 계절
    public enum Season
    {
        eSpring = 1,					//봄
        eSummer,						//여름
        eAutumn,						//가을
        eWinter,						//겨울
    }

	// 기후 
    public enum Weather					//버프 (일 당 +점수), 디버프 (일 당 -점수)
    {
        ERROR = 0,
        eSunny,							//화창 버프2, 디버프 1  
        eDrought,						//가뭄 버프4, 디버프 3
        eRain,							//비   버프3, 디버프 3
        eCold,							//눈   버프4, 디버프 3
    }


	// Initial Date(게임 시작했을 때의 날짜)
	public enum InitialDate
	{
		eInitYear = 2019,		
		eInitMonth = 3,			
		eInitDay = 1,
        eInitHour = 6,
	}

	/// <summary>
	/// 이벤트에 대한 ENUM////////////////////////////////////////////////////
	/// http://www.cien.or.kr/pages/viewpage.action?pageId=15761543
	/// </summary>
    /// 

    public enum NewsType
    {

    }

	// Special Event
	public enum SpecialEvent
	{
		eChristmas = 1,					//크리스마스
		eThanksgiving,					//추석
		eNewYear,						//설날
		eDano,                          //단오 Celebration_of_spring_and_farming
	}

	// Farm Event
	public enum FarmEvent
	{
		eTimeDecrease,                  //시간 감소 - 단비
		eNoFertilizer,                  //비료 불가 - 홍수, 태풍, 우박, 장마, 가뭄
		eFreeFertilizer,                //비료 공짜 - 소가 쉬다감, 비료회사 판촉행사
	}

	// Bank Event
	public enum BankEvent
	{
		eDebtIncrease,					//빚 증가 - 은행장 서류 찾아냄
		eDebtDecrease,					//빚 탕감 - 빚 일부분??(얼마큼인지 안명시) - 은행장 서류 잃어버림
		eInterestExemption,				//이자 면제 - 은행장 로또 당첨
		eNoLoan,						//대출 불가 - 은행휴가
		eInterestChanged,				//이자율 변동 - 경기활성화, 경기 불황
	}

	// Shop Event
	public enum ShopEvent
	{
		eCropPriceUp,					//작물 가격 업
		eWorkerSale,					//일꾼 세일 
		eWorkerPerectageUp,				//일꾼 확률 업
		eFertilizerSale,				//비료 세일
	}

	// Worker Event
	public enum WorkerEvent
	{
		eFreeUpgrade,					//일꾼 무료 성장
		eFreeExp,						//일꾼 경험치 증가
	}

	// Crop Event
	public enum CropEvent
	{
		eExpensive,						//작물 비싸짐
		eCropMastered,					//작물 마스터
	}
	///////////////////////////////////////////////////////////////////////////
    public enum Field_Position
    {
        eField0 = 0,
        eField1 = 1,
        eField2 = 2,
        eField3,
        eField4,
        eField5,
    }

	//For Customization
	public enum WorkerAppearanceDetail
	{
		eWorkerName,
		eBasedBody,
		eHair,
		eShirt,
		ePants,
		eHairColor,
		eShoes
	}
}
