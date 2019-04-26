using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TestMod.Projectiles
{
    class ProjectileOrbit : ModProjectile
    {

        public override void SetDefaults()
        {
            drawOffsetX = -2;
            drawOriginOffsetX = 0;
            drawOriginOffsetY = 0;
            projectile.arrow = false;
            projectile.damage = 5;
            projectile.width = 8; //hitbox is 8 pixels wide
            projectile.height = 8; //hiutbox is 8 pixels high
            projectile.aiStyle = -1; 
            projectile.friendly = true; //player projectile
            projectile.ranged = true; //ranged projectile
            projectile.penetrate = 3;
            projectile.magic = true;
            projectile.timeLeft = 600; //lasts for 300 frames/ticks. Terraria runs at 60FPS, so it lasts 5 seconds.
            aiType = ProjectileID.WoodenArrowFriendly;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType("ProjectileOrbit"), damage, knockback, projectile.owner, -1f, -1000f);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            return false;
        }
        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
        public override void AI()
        {
            //Making player variable "p" set as the projectile's owner
            Player p = Main.player[projectile.owner];

            //Factors for calculations
            double deg = (double)projectile.ai[1] * 2f; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 64; //Distance away from the player

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
            projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.width;

            
            projectile.rotation = (float)Math.Atan2(p.position.Y - projectile.position.Y, p.position.X - projectile.position.X);
            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            projectile.ai[1] += 1f;



        }
    }

}
