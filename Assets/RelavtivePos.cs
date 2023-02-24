using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelavtivePos : MonoBehaviour
{
    // Start is called before the first frame update
    void Example()
    {
        transform.localPosition = new Vector3(0, 0, 0);

        print(transform.localPosition.y);
    }

}
