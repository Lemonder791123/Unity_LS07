using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed;
    private Camera camera;

    // Use this for initialization
    void Start()
    {
        // 形變組件transform,與該腳本直接關聯上的組件就是transform
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // 得到鼠標當前位置
        float mouseX = Input.GetAxis("Mouse X") * speed;
        float mouseY = Input.GetAxis("Mouse Y") * speed;
        // 設置照相機和Player的旋轉角度，X,Y值需要更具情況變化位置
        camera.transform.localRotation = camera.transform.localRotation * Quaternion.Euler(-mouseY, 0, 0);
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, mouseX, 0);
        //需要將攝像頭放在另個GameObject里
        //當上下旋轉的時候，只旋轉攝像頭（相當於人的頭部）
        //當左右旋轉的時候，旋轉整體（相當於全身，包括頭部）
        //這樣使得整個視角都處於正對面，而不會出現傾斜等現象
    }
}