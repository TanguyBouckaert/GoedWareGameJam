using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPosDraw : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {

            Gizmos.DrawIcon(transform.position, "target.png", true, new Color(255, 0, 0, 50));
    }
}
