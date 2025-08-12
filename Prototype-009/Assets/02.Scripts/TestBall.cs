using System.Runtime.InteropServices;
using UnityEngine;
using Utility.Unity.Common;

public class TestBall : MonoBehaviour
{
    [SerializeField] private float power;
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(power * Randoms.RandomLinnerVector3(new Vector3(-1f,-1f,0),new Vector3(1f,1f,0)));
    }
}
