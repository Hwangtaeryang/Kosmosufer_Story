using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropColl : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            RaceManager.Instance.dropColl = true;
            RaceManager.Instance.RaceOver();
        }
    }
}
