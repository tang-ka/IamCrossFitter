using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    int i = 0;

    public void Start()
    {
        StartAsync();
    }

    async Task StartAsync()
    {
        print("aaaaaaaaaaaaaaa");
        await Test1();
        print("bbbbbbbbbbbbbbb");
    }

    public async UniTask Test1()
    {
        await UniTask.WaitUntil(() =>
        {
            print(i++);
            if (i == 10)
                return true;

            return false;
        });

        Test2();

        print("cccccccccccccc");
    }

    public async void Test2()
    {
        print("hi");
        await UniTask.WaitUntil(() =>
        {
            print(i++);
            if (i == 20)
                return true;

            return false;
        });

        print("ddddddddddddddd");
    }

    public void Test3()
    { 

    }

    public void Test4()
    {

    }
}
