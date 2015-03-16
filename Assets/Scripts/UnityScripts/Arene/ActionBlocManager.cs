using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ActionBlocManager : MonoBehaviour {


    Bot bot;

    public BlockAction[] allActions;

    public GameObject prefabButton;

    public int side = 0;

	void Start () {
        bot = GetComponentInParent<MoveSlide>().botControlled;

        allActions = bot.GetComponentsInChildren<BlockAction>();

        int i = 0;

        foreach (BlockAction action in allActions)
        {
            if(action.block.blockType == Block.BlockType.ACTIVE)
            { 
                // Create Button
                GameObject newButton = Instantiate(prefabButton) as GameObject;
                newButton.GetComponent<BoutonAction>().blocLinked = action;
                newButton.GetComponentInChildren<Text>().text = action.block.id;
                newButton.transform.SetParent(transform);
                newButton.GetComponent<RectTransform>().localPosition = new Vector3(0,140 + i * -90,0);
                newButton.GetComponent<RectTransform>().localScale = new Vector3(0.6f,0.6f,0.6f);

                i++;
            }
        }
	}
	

	void Update () {
	
	}
}
