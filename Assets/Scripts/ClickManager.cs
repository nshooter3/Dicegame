using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public Enemy targetedEnemy;
    //TODO: Dice holder.

    // Update is called once per frame
    void Update()
    {
        targetedEnemy = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                targetedEnemy = hit.collider.gameObject.GetComponent<Enemy>();
                //Debug.Log("Click on enemy " + hit.collider.gameObject.name);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Dice"))
            {
                //Do dice stuff who cares
                //Debug.Log("Click on dice " + hit.collider.gameObject.name);
            }
        }
    }

    public bool CheckForSelectedEnemy(out Enemy enemy)
    {
        if (targetedEnemy != null)
        {
            enemy = targetedEnemy;
            return true;
        }
        enemy = null;
        return false;
    }
}
