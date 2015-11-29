using UnityEngine;

namespace Assets.Scripts.Attachables
{
    public enum AttachableType { None, Weapon, Engine, Support }

    public class Attachable : MonoBehaviour
    {
        [Header("Attachable")]
        public string AttachableID;
        public string AttachableName;
        public AttachableType Type;
        public int Level;
        public int EnergyRequirement;
        public Sprite PresentationSprite;
    }
}
