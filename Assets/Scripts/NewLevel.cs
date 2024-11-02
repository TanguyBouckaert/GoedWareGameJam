using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewLevel : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement pm;

    [SerializeField]
    public List<GameObject> levelPositions = new List<GameObject>();

    public List<Transform> GetPositionsInRoom(int room)
    {
        List<Transform> transforms = new List<Transform>();
        transforms.Add(levelPositions[room].transform.GetChild(1));
        transforms.Add(levelPositions[room].transform.GetChild(2));
        return transforms;
    }

    public void CancelEnemy()
    {
        pm.DeleteEnemy();
    }
}
