using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
public class CinemachineVirtualCamera : CinemachineVirtualCameraBase, ICinemachineCamera
{
    public override CameraState State => throw new System.NotImplementedException();

    public override Transform LookAt { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override Transform Follow { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}

public class Vcam : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

}
