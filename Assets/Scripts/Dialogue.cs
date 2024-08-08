using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] string[] lines;
    [SerializeField] float textSpeed;
    [SerializeField] List<Sprite> images;
    [SerializeField] List<string> names;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI NPCname;
    int index;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // When pressing left click load all the text and finish all dialog when text is finished
        if(Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
                NextLine();
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        image.sprite = images[i];
        NPCname.text = names[i];
        Debug.Log(names[i]);
        i++;
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            ClearText();
        }
    }

    public void ClearText()
    {
        textComponent.text = string.Empty;
        index = 0;
        gameObject.SetActive(false);
        //Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        //Time.timeScale = 1f;
    }
}
