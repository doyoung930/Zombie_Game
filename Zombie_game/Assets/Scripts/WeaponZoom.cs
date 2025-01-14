using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera; // CinemachineVirtualCamera로 변경
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;

    public bool zoomedInToggle = false;
    
    
    
    private void Start()
    {
        // 초기 FOV 설정
        virtualCamera.m_Lens.FieldOfView = zoomedOutFOV; // 시작할 때 일반 FOV 설정
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 우클릭으로 줌
        {

            // FOV 값 변경
            if (!zoomedInToggle)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void OnDisable()
    {
        ZoomOut();
    }
    
    public void ZoomOut()
    {
        zoomedInToggle = false;
        virtualCamera.m_Lens.FieldOfView = zoomedOutFOV; // 줌아웃 FOV 설정
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        virtualCamera.m_Lens.FieldOfView = zoomedInFOV; // 줌인 FOV 설정
    }
}