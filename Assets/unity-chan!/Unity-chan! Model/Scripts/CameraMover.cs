using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraMover : MonoBehaviour
{
    // WASD：前後左右の移動
    // QE：上昇・降下
    // 左ドラッグ：前後左右の移動

    //マウス感度
    [SerializeField, Range(30.0f, 150.0f)]
    private float _mouseSensitive = 90.0f;

    //カメラ操作の有効無効
    private bool _cameraMoveActive = true;
    //カメラのtransform
    private Transform _camTransform;
    //マウスの始点
    private Vector3 _startMousePos;
    //カメラ回転の始点情報
    private Vector3 _presentCamRotation;
    //初期状態 Rotation
    private Quaternion _initialCamRotation;

    private float eulerX = 0.0f;

    void Start()
    {
        _camTransform = this.gameObject.transform;

        //初期回転の保存
        _initialCamRotation = this.gameObject.transform.rotation;
    }

    void Update()
    {
        if (_cameraMoveActive)
        {
            CameraRotationMouseControl(); //カメラの回転 マウス
        }
    }

    //カメラの回転 マウス
    private void CameraRotationMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePos = Input.mousePosition;
            _presentCamRotation.x = _camTransform.transform.eulerAngles.x;
            _presentCamRotation.y = _camTransform.transform.eulerAngles.y;
        }

        if (Input.GetMouseButton(0))
        {
            //(移動開始座標 - マウスの現在座標) / 解像度 で正規化
            float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;

            //回転開始角度 ＋ マウスの変化量 * マウス感度
            float eulerX = _presentCamRotation.x + y * _mouseSensitive;
            //float eulerY = _presentCamRotation.y + x * _mouseSensitive;
            float eulerY = _presentCamRotation.y;

            _camTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }

        if (Input.GetKey(KeyCode.R))
        {
            _presentCamRotation.x = _camTransform.transform.eulerAngles.x;
            _presentCamRotation.y = _camTransform.transform.eulerAngles.y;

            //上に0.1ずつ向く
            eulerX -= 0.15f;
            float eulerY = _presentCamRotation.y;

            _camTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }

        if (Input.GetKey(KeyCode.F))
        {
            _presentCamRotation.x = _camTransform.transform.eulerAngles.x;
            _presentCamRotation.y = _camTransform.transform.eulerAngles.y;

            //下に0.1ずつ向く
            eulerX += 0.15f;
            float eulerY = _presentCamRotation.y;

            _camTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }
    }
}