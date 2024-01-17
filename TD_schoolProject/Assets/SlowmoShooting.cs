using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEditor;

public class SlowmoShooting : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float fireRate = 4f;
    [SerializeField] private float freezeDuration = 1f;
    
    private float timeSinceLastFire;
    
    private void Update()
    { 
            timeSinceLastFire += Time.deltaTime;
            if (timeSinceLastFire >= 1f / fireRate)
            {
                Freeze();
                timeSinceLastFire = 0f;
            } 
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Freeze()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero);

        if (hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                
                EnemyMovement enemyMovement = hit.transform.GetComponent<EnemyMovement>();
                enemyMovement.UpdateSpeed(0.5f); 
                
                StartCoroutine(ResetEnemySpeed(enemyMovement));
            }
        }
    }
    
    private IEnumerator ResetEnemySpeed(EnemyMovement enemyMovement)
    {
        yield return new WaitForSeconds(freezeDuration);
        enemyMovement.ResetSpeed();
    }
    
    private void OnDrawGizmosSelected() 
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, Vector3.forward, range);
    }
}
