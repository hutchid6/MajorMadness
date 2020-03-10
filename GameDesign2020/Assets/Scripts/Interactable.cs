using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
/// attached to the non-player characters, and stores the name of the
/// Yarn node that should be run when you talk to them.
namespace Yarn.Unity.Example
{
    public class Interactable : MonoBehaviour
    {

        public string characterName = "";

        [FormerlySerializedAs("startNode")]
        public string talkToNode = "";

        [Header("Optional")]
        public YarnProgram scriptToLoad;

        // Use this for initialization
        void Start()
        {
            if (scriptToLoad != null)
            {
                DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
                dialogueRunner.Add(scriptToLoad);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
