using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GUI
{
    public class SlotUI : MonoBehaviour
    {
        public void OnClicked()
        {
            EventManager.SlotSelected.Invoke(new SlotSelectedEventArgs(this));
        }
    }
}
