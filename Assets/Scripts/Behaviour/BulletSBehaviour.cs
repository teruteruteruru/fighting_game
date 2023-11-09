using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSBehaviour : BulletBehaviour
{
    #region define

    #endregion

    #region serialize field
    #endregion

    #region field

    #endregion

    #region property

    #endregion

    #region Unity function
    private void Update()
    {
        Destroy(gameObject, 3f);
    }
    #endregion

    #region public function
    #endregion

    #region private function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ìñÇΩÇ¡ÇΩÇÁé©êgÇè¡Ç∑
        Destroy(gameObject,0.5f);
        
    }


    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.name == "BulletArea")
        {
            // ìñÇΩÇ¡ÇΩÇÁé©êgÇè¡Ç∑
            Destroy(gameObject);
        }
    }
    #endregion
}

