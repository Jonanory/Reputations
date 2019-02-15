using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Reputations
{
    public class ReputationManager : MonoBehaviour
    {
        public static ReputationManager master;

        private static Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public static Dictionary<string, Action> Actions
        {
            get { return actions; }
        }
        
        private static List<IReputable> reputables = new List<IReputable>();
        public static List<IReputable> Reputables
        {
            get { return reputables; }
        }

        void Awake()
        {
            if (master == null)
            {
                master = this;
            }
            else if (master != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            //Load All Actions from a file
            actions = new Dictionary<string, Action>();
            var path = Path.Combine( Directory.GetCurrentDirectory(), "\\Actions.json" );
            string jsonString = File.ReadAllText( path );
            ActionList actionList = JsonUtility.FromJson<ActionList>(jsonString);

            foreach (Action _action in actionList.actions)
            {
                actions.Add(_action.name, _action);
            }


            // Find all the reputable objects in the scene
            var foundReputables = FindObjectsOfType<MonoBehaviour>().OfType<IReputable>();
            foreach (IReputable reputable in foundReputables)
            {
                reputables.Add( reputable );
            }
        }
    }

}
