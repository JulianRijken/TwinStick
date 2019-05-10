using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    private enum GunType{Semi,Burst,Auto}
    private enum GunState{ReadyToFire,Reloading,Empty,LoadingChamber }

    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GunType gunType = GunType.Auto;
    [SerializeField] private float timeBitweenShots = 0.1f;
    [SerializeField] private float reloadTime = 1f;

    private GunState gunState = GunState.ReadyToFire;

    private bool shootDelayOver = true;

    public void ShootAuto()
    {
        if(gunType.Equals(GunType.Auto))
        Shoot();
    }

    public void Shoot()
    {
        if(gunState.Equals(GunState.ReadyToFire))
        {
            Debug.Log("SHOT!");

            StartCoroutine(ReloadChamber());
        }
    }

    private void Update()
    {
        Debug.Log(gunState.ToString());
    }

    IEnumerator ReloadChamber()
    {
        gunState = GunState.LoadingChamber;
        yield return new WaitForSeconds(timeBitweenShots);
        gunState = GunState.ReadyToFire;
    }


    public void Reload()
    { 
        if(!gunState.Equals(GunState.Reloading))
        {
           StartCoroutine(ReloadMag());
        }
    }

    IEnumerator ReloadMag()
    {
        gunState = GunState.Reloading;
        yield return new WaitForSeconds(reloadTime);
        gunState = GunState.ReadyToFire;
    }




    /// <summary>
    /// Draws The Gun Shooting Diraction And Point
    /// </summary>
    private void OnDrawGizmos()
    {
        if (shootPoint != null)
        {
            Gizmos.color = Color.red;

            Vector3 direction = shootPoint.forward;

            Gizmos.DrawRay(shootPoint.position, direction);
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 20f, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 20f, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(shootPoint.position + direction, right * 0.25f);
            Gizmos.DrawRay(shootPoint.position + direction, left * 0.25f);
        }
    }



}
