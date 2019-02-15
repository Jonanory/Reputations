using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reputations
{
    public enum CategoryImportance
    {
        NOT_AT_ALL = 0,
        A_LITTLE = 1,
        SOMEWHAT = 10,
        VERY = 50,
        MANDITORY = 250
    }

    public struct SingleReputation
    {
        public Attributes attributes;
        public float timeElement;

        public SingleReputation( Attributes _attributes, float _timeElement = 0f )
        {
            attributes = _attributes;
            timeElement = _timeElement;
        }
    }
    public class Reputation
    {
        [SerializeField]
        [Range(0f, 1.0f)]
        float timeBias = 0.5f;

        [SerializeField]
        [Range(0f, 1.0f)]
        float goodBias = 0.5f;

        [SerializeField]
        [Range(0f, 1.0f)]
        float evilBias = 0.5f;

        public Attributes preferences = new Attributes();

        Dictionary<IReputable, Attributes> knownAttributes = new Dictionary<IReputable, Attributes>();
        public Dictionary<IReputable, Attributes> KnownAttributes
        {
            get { return knownAttributes; }
        }

        Dictionary<IReputable, SingleReputation> reputables = new Dictionary<IReputable, SingleReputation>();
        public Dictionary<IReputable, SingleReputation> Reputables
        {
            get { return reputables; }
        }

        public Reputation() { }
        public Reputation( float _goodBias, float _evilBias, float _timeBias)
        {
            goodBias = _goodBias;
            evilBias = _evilBias;
            timeBias = _timeBias;
        }

        public SingleReputation GetReputable(IReputable reputable)
        {
            if (!reputables.ContainsKey(reputable))
            {
                reputables.Add(reputable, new SingleReputation(new Attributes()));
            }
            return reputables[reputable];
        }

        public void SetReputable(IReputable reputable, Attributes _attributes, float? _timeElement = null )
        {
            if (_timeElement != null)
            {
                reputables[reputable] = new SingleReputation(_attributes, (float)_timeElement);
            }
            else if (reputables.ContainsKey(reputable))
            {
                reputables[reputable] = new SingleReputation(_attributes, reputables[reputable].timeElement);
            }
            else
            {
                reputables[reputable] = new SingleReputation(_attributes, 0.5f);
            }
        }

        /// <summary>
        ///  Change a reacter's opinion of a reputable based on a given set of attributes (generally will be attributes of an action the reactee took).
        /// </summary>
        /// <param name="reputable"></param>
        /// <param name="attributes"></param>
        public void ReactTo( IReputable reputable, Attributes attributes )
        {
            SingleReputation sR = GetReputable(reputable);

            float currentReputation = Opinion(reputable);
            float previousReputationBias = GetPreviousReputationBias( currentReputation, sR.timeElement );

            Attributes newAttributes = NewAttributesValue(sR.attributes, attributes, previousReputationBias );
            float newTimeElement = UpdateTimeElement(sR.timeElement);

            SetReputable( reputable, newAttributes, newTimeElement);
        }

        private Attributes NewAttributesValue(Attributes _currentAttributes, Attributes _actionAttributes, float _bias = 0.5f )
        {
            return (Attributes)(_bias * _currentAttributes + (1 - _bias) * _actionAttributes);
        }

        /// <summary>
        ///  Generate the weight that the reacter will place on the current opinion of a reputable as opposed to the opinion of the input attributes
        /// </summary>
        /// <param name="reputation"></param>
        /// <param name="timeElement"></param>
        /// <returns></returns>
        private float GetPreviousReputationBias(float reputation, float timeElement = 1f)
        {
            float value = (goodBias - evilBias) * reputation + evilBias;
            return value * timeElement;
        }

        private float UpdateTimeElement( float _oldTimeElement )
        {
            float s = 2 * timeBias * timeBias;
            return (s * _oldTimeElement + 1 - _oldTimeElement) / (1 - _oldTimeElement + s);
        }

        /// <summary>
        ///     If a reputable has some level of reputation help, then the reputation of that reputable will be given a boost
        /// </summary>
        /// <param name="reputation">The current reputation</param>
        /// <param name="reputationHelp">Ranges from -1 to 1</param>
        /// <returns>Modified reputation</returns>
        public static float ApplyReputationHelp(float reputation, float reputationHelp = 0)
        {
            if (reputationHelp == 0) return reputation;
            float newReputation = reputation;
            if (reputationHelp < 0)
            {
                newReputation = reputation * reputationHelp + reputation;
            }
            else if (reputationHelp > 0)
            {
                newReputation = (1 - reputation) * reputationHelp + reputation;
            }
            return Mathf.Clamp(newReputation, 0f, 1f);
        }

        /// <summary>
        ///  Get the reacter's opinion of a reputatble.
        /// </summary>
        /// <param name="reputable"></param>
        /// <param name="reputationHelp">A modifier of the output opinion. Ranges from -1 to 1.</param>
        /// <returns></returns>
        public float Opinion(IReputable reputable, float reputationHelp = 0f)
        {
            float reputation = Opinion( knownAttributes[reputable] );
            return ApplyReputationHelp( reputation, reputationHelp );
        }

        /// <summary>
        ///  Get the reacter's opinion of a set of attributes. Based on how close attributes are to the reacter's preference in attributes
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public float Opinion(Attributes attributes)
        {
            return Mathf.Pow(1 - preferences.Distance(attributes), 5f / 3f);
        }
    }


    [System.Serializable]
    public class ReputationCategoryBias
    {
        public CategoryImportance categoryImportance = CategoryImportance.SOMEWHAT;

        [Range(0.0f, 1.0f)]
        public float categoryPreference = 0.5f;

        public ReputationCategoryBias(CategoryImportance _importance = CategoryImportance.SOMEWHAT, float _preference = 0.5f)
        {
            categoryImportance = _importance;
            categoryPreference = _preference;
        }
    }
}
