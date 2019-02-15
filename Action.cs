using System.Collections.Generic;
using UnityEngine;
using System.Collections;
// using LitJson;


namespace Reputations
{
    [System.Serializable]
    public class ActionList
    {
        public List<Action> actions = new List<Action>();
    }

    [System.Serializable]
    public class Action
    {
        public string name = "";
        public Attributes attributes;
        public Attributes targetAttributes = null;

        public Action( string _name, Attributes _attributes)
        {
            name = _name;
            attributes = _attributes;
        }

        public Action(string _name, float[] attributeValues, float[] targetAttributeValues = null )
        {
            name = _name;
            attributes = new Attributes( attributeValues );
            if( targetAttributeValues != null )
            {
                targetAttributes = new Attributes( targetAttributeValues );
            }
        }
    }

}
