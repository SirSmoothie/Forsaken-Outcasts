using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : NetworkBehaviour
{
    public Camera cameraObject;
    private void Start()
    {
        if(!IsOwner) return;

        cameraObject.enabled = true;
    }
}
