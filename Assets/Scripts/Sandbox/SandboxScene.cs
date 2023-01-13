#pragma warning disable CS1591

using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SandboxScene : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;
    // MyMyClass mc;
    //ReactiveProperty<int> rp = new ReactiveProperty<int>();


    //public async void Start()
    //{
    //    rp.Value = 10;

    //    buttonA.onClick.AddListener(() =>
    //    {
    //        rp.Value = 99;
    //    });

    //    Debug.Log("Begin:" + rp.Value);
    //    var v= await rp;
    //    Debug.Log("End:" + v);
    //}

}

public class MyMyClass
{
    public int MyProperty { get; set; }

    ~MyMyClass()
    {
        Debug.Log("GCed");
    }
}

#pragma warning restore CS1591
