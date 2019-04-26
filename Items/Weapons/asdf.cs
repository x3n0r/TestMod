using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TestMod.Items.Weapons
{
	public class asdf : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("asdf");
			Tooltip.SetDefault("This is a modded sword.");
		}
		public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("ProjectileOrbit");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.NextFloat() < 0.01f)
            {
                createProjectile(player,damage, knockBack);
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            createProjectile(player,damage,knockBack);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }

        private void createProjectile(Player player,int damage, float knockBack)
        {
            damage = damage / 5;
            Projectile.NewProjectile(player.position, player.velocity, mod.ProjectileType("ProjectileOrbit"), damage, knockBack, player.whoAmI, -1f, -1000f);
        }
    }
}
