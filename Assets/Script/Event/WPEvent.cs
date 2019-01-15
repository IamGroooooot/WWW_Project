using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스에선 이벤트의 발생을 정의합니다. 이벤트가 언제 발생하는지 조건과 그 실행문을 저장합니다.
/// Init() 과 Execution() 함수는 반드시 override되어야합니다.
/// </summary>
public class WPEvent : MonoBehaviour {

    List<Condition> conditions = new List<Condition>();

    protected class Condition
    {
        WPEnum.VariableType variableType;
        WPEnum.CompareType compareType;
        object value;
        System.Type type;
        

        public Condition(WPEnum.VariableType _variableType, WPEnum.CompareType _compareType, object _value)
        {
            variableType = _variableType;
            compareType = _compareType;
            value = _value;
        }

        public void SetType(System.Type _type)
        {
            type = _type;
        }

        public bool CheckCondition()
        {
            if(type == typeof(int))
            {
                int intValue = WPGameVariableManager.instance.LoadIntVariable(variableType);
                int compareValue = (int)value;
                switch (compareType)
                {
                    case WPEnum.CompareType.eEqual:
                        if (intValue == compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eLess:
                        if (intValue <= compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eMore:
                        if (intValue >= compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eUnder:
                        if (intValue < compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eOver:
                        if (intValue > compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eNot:
                        if (intValue != compareValue) return true;
                        else return false;
                }
                return false; // 아무것도 아닐 경우 false 리턴
            }
            else if(type == typeof(float))
            {
                float floatValue = WPGameVariableManager.instance.LoadFloatVariable(variableType);
                float compareValue = (float)value;
                switch (compareType)
                {
                    case WPEnum.CompareType.eEqual:
                        if (floatValue == compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eLess:
                        if (floatValue <= compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eMore:
                        if (floatValue >= compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eUnder:
                        if (floatValue < compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eOver:
                        if (floatValue > compareValue) return true;
                        else return false;
                    case WPEnum.CompareType.eNot:
                        if (floatValue != compareValue) return true;
                        else return false;
                }
                return false; // 아무것도 아닐 경우 false 리턴
            }
            else if(type == typeof(string))
            {
                string stringValue = WPGameVariableManager.instance.LoadStringVariable(variableType);
                string compareValue = (string)value;
                switch (compareType)
                {
                    case WPEnum.CompareType.eEqual:
                        if (stringValue.Equals(compareValue)) return true;
                        else return false;
                    case WPEnum.CompareType.eNot:
                        if (stringValue.Equals(compareValue) == false) return true;
                        else return false;
                }
                return false; // 아무것도 아닐 경우 false 리턴
            }
            else
            {
                return false;
            }
        }
    }

    protected void SetCondition<T>(WPEnum.VariableType varaibleType, WPEnum.CompareType compareType, object value)
    {
        if(typeof(T) == typeof(string))
        {
            if(compareType != WPEnum.CompareType.eEqual || compareType != WPEnum.CompareType.eNot)
            {
                return; // string일 경우 같거나 같지 않음만을 판별할 수 있습니다.
            }
        }
        Condition newCondition = new Condition(varaibleType, compareType, value);
        newCondition.SetType(typeof(T));
        conditions.Add(newCondition);
    }

    private void Start()
    {
        Init();
        StartCoroutine(CheckCondition());
    }

    /// <summary>
    /// SetCondition<Type>()를 이용해 조건문들을 작성하십시오.
    /// </summary>
    protected virtual void Init()
    {

    }

    /// <summary>
    /// 여기서 실행되어야 할 것을 작성하십시오. base.Execution()은 파괴 동작을 수행합니다. Execution()의 가장 하위에 위치시키십시오.
    /// </summary>
    protected virtual void Execution()
    {
        Destroy(gameObject);
    }

    private IEnumerator CheckCondition()
    {
        for(; ; )
        {
            int conditionCount = 0;
            for(int index = 0; index < conditions.Count; ++index)
            {
                if (conditions[index].CheckCondition() == true) conditionCount++;
            }
            if (conditionCount >= conditions.Count) break; // 매 프레임마다 모든 조건들을 검사하여 만족하는지 검사합니다.
            yield return null; // 더 좋은 방법 없나유ㅠㅠㅠㅠㅠㅠㅠ
        }
        Execution();
    }


}
