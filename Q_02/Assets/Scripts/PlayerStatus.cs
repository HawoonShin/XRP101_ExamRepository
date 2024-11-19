using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float MoveSpeed { get; set; }
    // 밑에 방식으로 하면 계속 스택이 쌓이는데 굳이 밑에처럼 할 이유가 있나..?
    //{
    //    get => MoveSpeed;
    //    private set => MoveSpeed = value;
    //}

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        MoveSpeed = 5f;
    }
}
