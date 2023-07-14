using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ItemEffect 스크립트는 추상클래스이고, ScriptableObject를 상속받음
public abstract class ItemEffect : ScriptableObject
{
    public abstract bool ExecuteRole(); // 추상메서드
}
