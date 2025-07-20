using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public static DoorController Instance;

    public Animator leftDoorAnim;
    public Animator rightDoorAnim;

    void Awake()
    {
        Instance = this;
    }

    public void OpenDoors()
    {
        leftDoorAnim.SetTrigger("Open");
        rightDoorAnim.SetTrigger("Open");
    }

    public void CloseDoors()
    {
        leftDoorAnim.SetTrigger("Close");
        rightDoorAnim.SetTrigger("Close");
    }
}
