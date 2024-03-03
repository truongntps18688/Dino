using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMN : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void explosionDestroy()
    {
        Destroy(gameObject);
    }
    public void deadEnemy()
    {
        Transform parentTransform = transform.parent;
        // Destroy cha
        if (parentTransform != null)
        {
            Destroy(parentTransform.gameObject);
        }
        GameObject expls = Instantiate(ScriptableObjectMN.Instance.EnemyData.explosionData.explosion, transform.position, Quaternion.identity);
        Destroy(expls, 0.25f);
    }

    public void changeAnimatorNormal()
    {
        if(animator != null)
            animator.SetBool("Hurt", false);
    }
}
