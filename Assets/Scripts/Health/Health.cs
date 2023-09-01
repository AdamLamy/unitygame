using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public string levelName;

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

                

                Scene scene = SceneManager.GetActiveScene();
                levelName = scene.name;
                //Debug.Log("Scene: " + levelName);
                if (levelName == "Level 1")
                {
                    SceneManager.LoadScene(6);
                }
                else if (levelName == "Level2" && sceneCheck.previousscene == false)
                {
                    SceneManager.LoadScene(10);
                }
                else
                {
                    sceneCheck.previousscene = false;
                    SceneManager.LoadScene(12);
                }

            }
            
        }
    }
}
