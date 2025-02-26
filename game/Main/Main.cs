using Godot;
using System;
using aTinyFantasy.Player;
using aTinyFantasy.InventorySystem.Inventory;

public partial class Main : Node2D
{
    Player player;
    InventoryInterface inventoryInterface;

    public override void _Ready()
    {
        player = GetNode<Player>("Player");

        //subscribe to the player's inventory data change event
        player.ToggleInventory += ToggleInventory;

        inventoryInterface = GetNode<InventoryInterface>("UI/InventoryInterface");

        InitializeInventory();
    }

    public void ToggleInventory()
    {
        inventoryInterface.Visible = !inventoryInterface.Visible;
        /*if (inventoryInterface.Visible)
        {
            Input.SetMouseMode(Input.MouseModeEnum.Visible);
        } else
        {
            Input.SetMouseMode(Input.MouseModeEnum.Captured);
        }*/
    }

    private void InitializeInventory()
    {
        if (player == null || inventoryInterface == null || player.InventoryData == null)
        {
            GD.PrintErr("Player or InventoryInterface is null in InitializeInventory!");
            return;
        }

        inventoryInterface.SetPlayerInventoryData(player.InventoryData);
    }



}
