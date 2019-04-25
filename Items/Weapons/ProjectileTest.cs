using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TestMod.Items.Weapons
{
    public class ProjectileTest : ModItem
    {
        public override void SetStaticDefaults() //used to set the name (defaults to the ID but with spaces before every capital letter except the first letter of the ID), tooltip and more.
        {
            Tooltip.SetDefault(""); //tooltip
        }
        public override void SetDefaults()
        {
            item.damage = 5; //deals 5 damage
            item.ranged = true; //deals magic damage
            item.noMelee = true; //the item sprite itself does no damage
            item.width = 8; //26 px wide
            item.height = 8; //26 px tall
            item.maxStack = 999; //This defines the items max stack
            item.consumable = true;  //Tells the game that this should be used up once fired
            item.knockBack = 1.5f;  //Added with the weapon's knockback
            item.value = Item.buyPrice(0, 0, 20, 0); // How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 20silvers)
            item.rare = 4;   //The color the title of your Weapon when hovering over it ingame  
            item.shoot = mod.ProjectileType("ProjectileTestProj");  //This defines what type of projectile this weapon will shoot 
            item.shootSpeed = 2f; //Added to the weapon's shoot speed
            item.ammo = 40;    //This defines what type of ammo is. 40 is arrow
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
