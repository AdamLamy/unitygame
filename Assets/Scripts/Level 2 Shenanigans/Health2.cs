using UnityEngine;
using UnityEngine.SceneManagement;

public class Health2 : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            //iframes
        }
        else
        {
            if(!dead)
            {
                anim.SetTrigger("die");
                //TimerController.instance.EndTimer();
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerCombat>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                dead = true;
                SceneManager.LoadScene(10);
            }
            
        }
    }
}
