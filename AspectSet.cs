using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Reputations
{
    public class AspectSet
    {
        public int ID;

        List<float> values = new List<float>() { 0, 0, 0, 0, 0, 0, 0 };
        public List<float> Values
        {
            get { return values;  }
            set { values = value; }
        }

        public AspectSet()
        {
            MakeNewID();
        }

        public AspectSet( float a, float b, float c, float d, float e, float f, float g )
        {
            MakeNewID();
            values = new List<float>() { a, b, c, d, e, f, g };
        }

        public AspectSet(float[] newValues)
        {
            if (newValues.Length == 7)
            {
                MakeNewID();
                values = new List<float>(newValues);
            }
            else
            {
                Debug.LogError("The wrong number of values were given");
            }
        }

        public AspectSet(AspectSet aspectSet)
        {
            MakeNewID();
            Values = aspectSet.Values;
        }

        private void MakeNewID()
        {
            ID = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        }

        public static AspectSet Zero = new AspectSet( 0, 0, 0, 0, 0, 0, 0 );

        public float Distance(AspectSet rS )
        {
            float distance = 0f;

            for (int i = 0; i < 7; i++)
            {
                distance += Mathf.Abs(values[i] - rS.values[i]);
            }

            return distance/7;
        }

        public bool SimilarTo(AspectSet rS, float successRadius)
        {
            return Distance(rS) <= successRadius;
        }
        
        public static AspectSet operator +( AspectSet rep1, AspectSet rep2 )
        {
            return new AspectSet(
                rep1.Values[0] + rep2.Values[0],
                rep1.Values[1] + rep2.Values[1],
                rep1.Values[2] + rep2.Values[2],
                rep1.Values[3] + rep2.Values[3],
                rep1.Values[4] + rep2.Values[4],
                rep1.Values[5] + rep2.Values[5],
                rep1.Values[6] + rep2.Values[6]
            );
        }
        public static AspectSet operator -(AspectSet rep1, AspectSet rep2)
        {
            return new AspectSet(
                rep1.Values[0] - rep2.Values[0],
                rep1.Values[1] - rep2.Values[1],
                rep1.Values[2] - rep2.Values[2],
                rep1.Values[3] - rep2.Values[3],
                rep1.Values[4] - rep2.Values[4],
                rep1.Values[5] - rep2.Values[5],
                rep1.Values[6] - rep2.Values[6]
            );
        }
        public static AspectSet operator *(AspectSet rep1, AspectSet rep2)
        {
            return new AspectSet(
                rep1.Values[0] * rep2.Values[0],
                rep1.Values[1] * rep2.Values[1],
                rep1.Values[2] * rep2.Values[2],
                rep1.Values[3] * rep2.Values[3],
                rep1.Values[4] * rep2.Values[4],
                rep1.Values[5] * rep2.Values[5],
                rep1.Values[6] * rep2.Values[6]
            );
        }

        public static AspectSet operator *( float a, AspectSet rep )
        {
            AspectSet answer = rep;
            for (int i = 0; i < 7; i++)
            {
                answer.Values[i] *= a;
            }
            return answer;
        }

        public static bool operator ==(AspectSet a, AspectSet b)
        {
            return a.values.Equals(b.values);
        }

        public static bool operator !=(AspectSet a, AspectSet b)
        {
            return !(a.values.Equals(b.values));
        }

        public override bool Equals(System.Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                List<float> p = ((AspectSet)obj).Values;
                return Values.Equals(p);
            }
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public float Total()
        {
            float answer = 0;
            for (int i = 0; i < 7; i++)
            {
                answer += values[i];
            }
            return answer;
        }

        public AspectSet Abs()
        {
            AspectSet answer = this;
            for (int i = 0; i < 7; i++)
            {
                answer.values[i] = Mathf.Abs( values[i] );
            }
            return answer;
        }
    }

}
