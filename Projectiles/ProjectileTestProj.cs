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
    class ProjectileTestProj : ModProjectile
    {

        private static Random generator = null;

        public override void SetDefaults()
        {
            projectile.arrow = true;
            projectile.width = 2; //sprite is 2 pixels wide
            projectile.height = 20; //sprite is 20 pixels tall
            projectile.aiStyle = 0; //projectile moves in a straight line
            projectile.friendly = true; //player projectile
            projectile.ranged = true; //ranged projectile
            projectile.timeLeft = 600; //lasts for 600 frames/ticks. Terraria runs at 60FPS, so it lasts 10 seconds.
            aiType = ProjectileID.WoodenArrowHostile; //This clones the exact AI of the vanilla projectile Bullet.
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false); //The debuff inflicted is the modded debuff Ethereal Flames. 180 is the duration in frames: Terraria runs at 60 FPS, so that's 3 seconds (180/60=3). To change the modded debuff, change EtherealFlames to whatever the buff is called; to add a vanilla debuff, change mod.BuffType("EtherealFlames") to a number based on the terraria buff IDs. Some useful ones are 20 for poison, 24 for On Fire!, 39 for Cursed Flames, 69 for Ichor, and 70 for Venom.
            splitProjectile();
        }

        private void splitProjectile()
        {
            for (int i = 0; i < 5; i++)
            {
                if (Main.rand.NextFloat() < 0.05f)  // .1323f == 13.23% change
                { /* 5% of the time Success*/
                    float scaleFactor4 = projectile.velocity.Length();
                    Vector2 velocity = projectile.velocity;
                    velocity.Normalize();
                    Vector2 vector12 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector12.Normalize();
                    vector12 += velocity * 2f * (i / 2);
                    vector12.Normalize();
                    vector12 *= scaleFactor4;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector12.X, vector12.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0f, -1000f);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
        public override void AI()
        {   /*
            int num3 = projectile.frameCounter;
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            projectile.frameCounter = num3 + 1;
            if (projectile.frameCounter > 0)
            {
                num3 = projectile.frame;
                projectile.frame = num3 + 1;
                projectile.frameCounter = 0;
                if (projectile.frame > 2)
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = -1;
                projectile.rotation = (float)Math.Atan2(-(double)projectile.velocity.Y, -(double)projectile.velocity.X);
            }
            else
            {
                projectile.spriteDirection = 1;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
            }
            if (projectile.ai[0] >= 0f && projectile.ai[0] < 200f)
            {
                int num543 = (int)projectile.ai[0];
                NPC nPC2 = Main.npc[num543];
                if (nPC2.CanBeChasedBy(projectile, false))
                {
                    float num544 = 20f;
                    Vector2 vector40 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num545 = Main.npc[num543].position.X - vector40.X;
                    float num546 = Main.npc[num543].position.Y - vector40.Y;
                    float num547 = (float)Math.Sqrt((double)(num545 * num545 + num546 * num546));
                    num547 = num544 / num547;
                    num545 *= num547;
                    num546 *= num547;
                    projectile.velocity.X = (projectile.velocity.X * 14f + num545) / 15f;
                    projectile.velocity.Y = (projectile.velocity.Y * 14f + num546) / 15f;
                }
                else
                {
                    float num548 = 1000f;
                    for (int num549 = 0; num549 < 200; num549++)
                    {
                        if (Main.npc[num549].CanBeChasedBy(projectile, false))
                        {
                            float num550 = Main.npc[num549].position.X + (float)(Main.npc[num549].width / 2);
                            float num551 = Main.npc[num549].position.Y + (float)(Main.npc[num549].height / 2);
                            float num552 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num550) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num551);
                            if (num552 < num548 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num549].position, Main.npc[num549].width, Main.npc[num549].height))
                            {
                                num548 = num552;
                                projectile.ai[0] = (float)num549;
                            }
                        }
                    }
                }
                return;
            }
            projectile.Kill();
            return;*/
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }

            float num132 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float num133 = projectile.localAI[0];
            if (num133 == 0f)
            {
                projectile.localAI[0] = num132;
                num133 = num132;
            }
            float num134 = projectile.position.X;
            float num135 = projectile.position.Y;
            float num136 = 300f;
            bool flag3 = false;
            int num137 = 0;
            if (projectile.ai[1] == 0f)
            {
                for (int num138 = 0; num138 < 200; num138++)
                {
                    if (Main.npc[num138].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num138 + 1)))
                    {
                        float num139 = Main.npc[num138].position.X + (float)(Main.npc[num138].width / 2);
                        float num140 = Main.npc[num138].position.Y + (float)(Main.npc[num138].height / 2);
                        float num141 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num139) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num140);
                        if (num141 < num136 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num138].position, Main.npc[num138].width, Main.npc[num138].height))
                        {
                            num136 = num141;
                            num134 = num139;
                            num135 = num140;
                            flag3 = true;
                            num137 = num138;
                        }
                    }
                }
                if (flag3)
                {
                    projectile.ai[1] = (float)(num137 + 1);
                }
                flag3 = false;
            }
            if (projectile.ai[1] > 0f)
            {
                int num142 = (int)(projectile.ai[1] - 1f);
                if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
                {
                    float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                    float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                    if (Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num143) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num144) < 1000f)
                    {
                        flag3 = true;
                        num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                        num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                    }
                }
                else
                {
                    projectile.ai[1] = 0f;
                }
            }
            if (!projectile.friendly)
            {
                flag3 = false;
            }
            if (flag3)
            {
                float num145 = num133;
                Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num146 = num134 - vector10.X;
                float num147 = num135 - vector10.Y;
                float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
                num148 = num145 / num148;
                num146 *= num148;
                num147 *= num148;
                int num149 = 8;
                projectile.velocity.X = (projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
            }
        }
    }

}
