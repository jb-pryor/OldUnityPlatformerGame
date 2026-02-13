using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int sceneBuildIndex;
    [SerializeField] Animator transitionAnim;

    //level move zoned enter, if collider is a player
    //move game to another scene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger entered");

        //
        if (collision.CompareTag( "Player"))
        {
            //player entered, so transition to next level
            print("switching scene to "+sceneBuildIndex);
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneBuildIndex/*, LoadSceneMode.Single*/);
        //transitionAnim.SetTrigger("Start");
    }
}
