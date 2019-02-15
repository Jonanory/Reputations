using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Reputations
{
    public enum AttributeName
    {
        FUN,
        SELFLESS,
        FAIR,
        MORAL,
        SHONEST,
        NOVEL,
        LOGICAL
    }

    [Serializable]
    public class Attributes : AspectSet
    {
        public Attributes() : base() { }
        public Attributes(float a, float b, float c, float d, float e, float f, float g) : base(a, b, c, d, e, f, g) { }
        public Attributes(float[] newValues) : base(newValues) { }
        public Attributes( AspectSet repSet ) : base(repSet) { }

        public Dictionary< AttributeName, float > ToDictionary()
        {
            Dictionary<AttributeName, float>  dict = new Dictionary<AttributeName, float>();

            foreach (AttributeName attr in Enum.GetValues(typeof(AttributeName)))
            {
                dict.Add(attr, Values[(int)attr]);
            }
            return dict;
        }

        public float GetAttribute(AttributeName _attribute)
        {
            return Values[(int)_attribute];
        }

        public void SetAttribute(AttributeName _attribute, float newValue)
        {
            Values[(int)_attribute] = newValue;
        }
    }

}
