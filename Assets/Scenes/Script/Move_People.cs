using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_People : MonoBehaviour
{

    public float startTime;

    public float minZ, maxZ;

    [Range(1, 100)]
    public float moveSpeed;

    public int sign = -1;





    void Update()
    {
        transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime * sign);

    }

}
