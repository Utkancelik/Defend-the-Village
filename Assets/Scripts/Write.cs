using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Write : MonoBehaviour
{
    [SerializeField] private float delay = 1.0f;

    [SerializeField][Multiline] private string text;

    [SerializeField] private Animator animator;

    private TextMeshProUGUI textMeshPRO;

    private void Start()
    {
        textMeshPRO = GetComponent<TextMeshProUGUI>();

        StartCoroutine(TypeIt(text));
    }

    private IEnumerator TypeIt(string text)
    {
        foreach (char character in text)
        {
            textMeshPRO.text += character.ToString();

            yield return new WaitForSeconds(delay);
        }


        

        animator.SetTrigger("Whiten");

        yield return new WaitForSeconds(1f);

        FindObjectOfType<GameManager>().isGameRunning = true;
        FindObjectOfType<GameManager>().isGameOver = false;
        FindObjectOfType<GameManager>().isGamePaused = false;

        textMeshPRO.gameObject.SetActive(false);

    }


}
