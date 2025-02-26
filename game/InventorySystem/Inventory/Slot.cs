using Godot;
using System;
using aTinyFantasy.InventorySystem.Item;

namespace aTinyFantasy.InventorySystem.Inventory {
    public partial class Slot : PanelContainer
    {

        TextureRect icon; 
        Label quantityLabel;

        public override void _Ready()
        {
            icon = GetNode<TextureRect>("MarginContainer/TextureRect");
            quantityLabel = GetNode<Label>("QuantityLabel");
        }

        public void SetSlotData(SlotData slotData)
        {
            ItemData itemData = slotData.Item;
            icon.Texture = (Texture2D)itemData.Icon;
            var tooltipText = "\n" + itemData.Name + "\n" + itemData.Description;

            if (slotData.Quantity > 1)
            {
                quantityLabel.Text = "x" + slotData.Quantity;
                quantityLabel.Show();
            }
        }

        public static implicit operator Slot(PackedScene v)
        {
            throw new NotImplementedException();
        }
    }
}
