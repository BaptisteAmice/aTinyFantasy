using Godot;
using System;
using aTinyFantasy.InventorySystem.Item;

namespace aTinyFantasy.InventorySystem.Inventory {
    [GlobalClass]
    public partial class SlotData : Resource
    {
        const int MAX_STACK_SIZE = 99;

        [Export]
        public ItemData Item { get; set; }

        private int _quantity = 1;
        [Export(PropertyHint.Range, "1,99")]
        public int Quantity { get => _quantity; set => SetQuantity(value); }

        
        public void SetQuantity(int newQuantity)
        {
            _quantity = newQuantity;
            if (_quantity > 1 && !Item.IsStackable)
            {
                GD.PrintErr("Trying to set quantity of non-stackable item to more than 1 - setting to 1", Item.Name);
                _quantity = 1;
            }
        }

        public bool CanFullyMergeWith(SlotData otherSlotData)
        {
            return Item == otherSlotData.Item && Item.IsStackable && Quantity + otherSlotData.Quantity <= MAX_STACK_SIZE;
        }

        public bool CanMergeWith(SlotData otherSlotData)
        {
            return Item == otherSlotData.Item && Item.IsStackable && Quantity < MAX_STACK_SIZE;
        }

        public void FullyMergeWith(SlotData otherSlotData)
        {
            if (CanFullyMergeWith(otherSlotData))
            {
                Quantity += otherSlotData.Quantity;
            }
        }

        public SlotData CreateSingleSlotData()
        {
            SlotData newSlotData = Duplicate() as SlotData;
            newSlotData.Quantity = 1;
            Quantity -= 1;
            return newSlotData;
        }
    }
}
