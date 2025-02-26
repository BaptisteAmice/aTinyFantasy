using Godot;
using System;

namespace aTinyFantasy.InventorySystem.Inventory {
    [GlobalClass]
    public partial class InventoryData : Resource
    {
        [Export]
        public SlotData[] Slots { get; set; } = null;

        [Signal]
        public delegate void InventoryInteractEventHandler(InventoryData inventory, int index, MouseButton button);

        public void OnSlotClicked(int index, MouseButton button)
        {
            //emit a signal
            EmitSignal(SignalName.InventoryInteract, this, index, (int)button);
        }

        public SlotData GrabSlotData(int index)
        {
            if (Slots[index] != null)
            {
                SlotData slotData = Slots[index];
                Slots[index] = null;
                return slotData;
            }
            return null;
        }
    }
}
