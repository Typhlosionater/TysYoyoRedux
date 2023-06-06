using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.DataStructures;
using Terraria.Localization;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Linq;

namespace TysYoyoRedux.Projectiles
{
	public class TysYoyoReduxGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		public int HitEffectCooldown = 0;

		public int EoCEnragedTimer = 0;

		public bool ValorWispOrbitActive;

		public bool GradientPulseOrbitActive;

		public bool Code1ElectrostaticAuraActive;

		public bool Code2PlasmaticAuraActive;

		public Vector2 ValkyrieTargetedEnemy = new Vector2(0, 0);

		//On Hit NPC effects
		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			//Vanilla Yoyo Effects + Debuffs that trigger on NPC hits
			if (projectile.aiStyle == 99 && ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
			{
				//Artery Bloody life steal effect
				if (projectile.type == ProjectileID.CrimsonYoyo)
				{
					if (Main.rand.Next(2) == 0 && target.lifeMax > 5 && !Main.player[projectile.owner].moonLeech)
					{
						int healingAmount = damage / 10;
						if (healingAmount > 0 && !(Main.player[projectile.owner].lifeSteal <= 0f))
						{
							Main.player[Main.myPlayer].lifeSteal -= healingAmount;
							Projectile.NewProjectile(projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileID.VampireHeal, 0, 0, projectile.owner, projectile.owner, healingAmount);
						}
					}
				}

				//Amazon Poisons on Impact
				if (projectile.type == ProjectileID.JungleYoyo)
				{
					if (Main.rand.Next(5) == 0)
					{
						target.AddBuff(BuffID.Poisoned, 7 * 60);
					}
				}

				//Cascade produces a fiery explosion + sparks on impact
				if (projectile.type == ProjectileID.Cascade && HitEffectCooldown == 0)
				{
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.CascadeFieryExplosionProjectile>(), projectile.damage, projectile.knockBack, projectile.owner);

					int ProjectileAmount = 2 + Main.rand.Next(2);
					for (int d = 0; d < ProjectileAmount; d++)
					{
						Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
						ProjectileVelocity *= Main.rand.NextFloat(0.8f, 1f);
						int FiredProjectile = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, 504, projectile.damage / 4, projectile.knockBack / 8, projectile.owner);
						Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
					}

					HitEffectCooldown = 18;
					projectile.netUpdate = true;
				}

				//Hel-Fire Produces flame torrents every 3 hits
				if (projectile.type == ProjectileID.HelFire && HitEffectCooldown == 0)
				{
					SoundEngine.PlaySound(SoundID.Item34, projectile.Center);

					Vector2 ProjectileVelocity = new Vector2(5, 5);
					float numberProjectiles = 12;
					float rotation = MathHelper.ToRadians(330 / 2);
					float randomization = Main.rand.Next(45);
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 IndividualProjectileVelocity = ProjectileVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
						IndividualProjectileVelocity = new Vector2(IndividualProjectileVelocity.X, IndividualProjectileVelocity.Y).RotatedBy(randomization);
						IndividualProjectileVelocity = new Vector2(IndividualProjectileVelocity.X, IndividualProjectileVelocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
						IndividualProjectileVelocity *= Main.rand.NextFloat(0.6f, 1f);

						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, IndividualProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.HelFireFlameTorrentProjectile>(), projectile.damage, 0, projectile.owner);
					}

					HitEffectCooldown = 28;
					projectile.netUpdate = true;
				}

				//Yelets Envenoms on impact
				if (projectile.type == ProjectileID.Yelets)
				{
					if (Main.rand.Next(3) == 0)
					{
						target.AddBuff(BuffID.Venom, 8 * 60);
					}
				}

				//Red's Throw Inflicts a Random Debuff on impact
				if (projectile.type == ProjectileID.RedsYoyo)
				{
					int DebuffSelection = Main.rand.Next(16);
					switch (DebuffSelection)
					{
						case 0:
							target.AddBuff(BuffID.Confused, 5 * 60);
							break;
						case 1:
							target.AddBuff(BuffID.CursedInferno, 5 * 60);
							break;
						case 2:
							target.AddBuff(BuffID.Ichor, 5 * 60);
							break;
						case 3:
							target.AddBuff(BuffID.BetsysCurse, 5 * 60);
							break;
						case 4:
							target.AddBuff(BuffID.Midas, 5 * 60);
							break;
						case 5:
							target.AddBuff(BuffID.Poisoned, 5 * 60);
							break;
						case 6:
							target.AddBuff(BuffID.Venom, 5 * 60);
							break;
						case 7:
							target.AddBuff(BuffID.OnFire3, 5 * 60);
							break;
						case 8:
							target.AddBuff(BuffID.Frostburn2, 5 * 60);
							break;
						case 9:
							target.AddBuff(BuffID.ShadowFlame, 5 * 60);
							break;
						case 10:
							target.AddBuff(BuffID.Daybreak, 5 * 60);
							break;
						case 11:
							target.AddBuff(BuffID.Oiled, 5 * 60);
							break;
						case 12:
							target.AddBuff(BuffID.Lovestruck, 5 * 60);
							break;
						case 13:
							target.AddBuff(BuffID.Stinky, 5 * 60);
							break;
						case 14:
							target.AddBuff(BuffID.Slimed, 5 * 60);
							break;
						case 15:
							target.AddBuff(BuffID.GelBalloonBuff, 5 * 60);
							break;
					}
				}

				//Kraken Abyssal Tentacles Effect
				if (projectile.type == ProjectileID.Kraken && HitEffectCooldown == 0)
				{
					SoundEngine.PlaySound(SoundID.Item103, projectile.position);
					int RandomRot = Main.rand.Next(0, 360);
					for (int d = 0; d < 3; d++)
					{
						RandomRot += 120;
						if (RandomRot >= 360)
						{
							RandomRot -= 360;
						}
						Vector2 value18 = new Vector2(3, 3).RotatedBy(MathHelper.ToRadians(RandomRot + Main.rand.Next(-55, 55)));
						value18.Normalize();
						Vector2 value19 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						value19.Normalize();
						value18 = value18 * 4f + value19;
						value18.Normalize();
						value18 *= new Vector2(5, 5);
						float num325 = (float)Main.rand.Next(10, 80) * 0.001f;
						if (Main.rand.Next(2) == 0)
						{
							num325 *= -1f;
						}
						float num326 = (float)Main.rand.Next(10, 80) * 0.001f;
						if (Main.rand.Next(2) == 0)
						{
							num326 *= -1f;
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, value18, ModContent.ProjectileType<VanillaYoyoEffects.KrakenAbyssalTentacleProjectile>(), projectile.damage, projectile.knockBack, projectile.owner, num326, num325);
					}

					HitEffectCooldown = 13;
					projectile.netUpdate = true;
				}

				//Eye of Cthulhu enrages when striking enemies
				if (projectile.type == ProjectileID.TheEyeOfCthulhu)
				{
					EoCEnragedTimer = 30;
				}
			}
		}

		//On Hit Player effects
		public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
		{
			//Vanilla Yoyo Effects + Debuffs that trigger on Player hits
			if (projectile.aiStyle == 99 && ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
			{
				//Artery Bloody life steal effect
				if (projectile.type == ProjectileID.CrimsonYoyo)
				{
					if (Main.rand.Next(2) == 0 && !Main.player[projectile.owner].moonLeech)
					{
						int healingAmount = damage / 10;
						if (healingAmount > 0 && !(Main.player[projectile.owner].lifeSteal <= 0f))
						{
							Main.player[Main.myPlayer].lifeSteal -= healingAmount;
							Projectile.NewProjectile(projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileID.VampireHeal, 0, 0, projectile.owner, projectile.owner, healingAmount);
						}
					}
				}

				//Amazon Poisons on Impact
				if (projectile.type == ProjectileID.JungleYoyo)
				{
					if (Main.rand.Next(5) == 0)
					{
						target.AddBuff(BuffID.Poisoned, 7 * 60);
					}
				}

				//Cascade produces a fiery explosion + sparks on impact
				if (projectile.type == ProjectileID.Cascade && HitEffectCooldown == 0)
				{
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.CascadeFieryExplosionProjectile>(), projectile.damage, projectile.knockBack, projectile.owner);

					int ProjectileAmount = 2 + Main.rand.Next(2);
					for (int d = 0; d < ProjectileAmount; d++)
					{
						Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
						ProjectileVelocity *= Main.rand.NextFloat(0.8f, 1f);
						int FiredProjectile = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, 504, projectile.damage / 4, projectile.knockBack / 8, projectile.owner);
						Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
					}

					HitEffectCooldown = 18;
					projectile.netUpdate = true;
				}

				//Hel-Fire Produces flame torrents every 3 hits
				if (projectile.type == ProjectileID.HelFire && HitEffectCooldown == 0)
				{
					SoundEngine.PlaySound(SoundID.Item34, projectile.Center);

					Vector2 ProjectileVelocity = new Vector2(5, 5);
					float numberProjectiles = 12;
					float rotation = MathHelper.ToRadians(330 / 2);
					float randomization = Main.rand.Next(45);
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 IndividualProjectileVelocity = ProjectileVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
						IndividualProjectileVelocity = new Vector2(IndividualProjectileVelocity.X, IndividualProjectileVelocity.Y).RotatedBy(randomization);
						IndividualProjectileVelocity = new Vector2(IndividualProjectileVelocity.X, IndividualProjectileVelocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
						IndividualProjectileVelocity *= Main.rand.NextFloat(0.6f, 1f);

						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, IndividualProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.HelFireFlameTorrentProjectile>(), projectile.damage, 0, projectile.owner);
					}

					HitEffectCooldown = 28;
					projectile.netUpdate = true;
				}

				//Yelets Envenoms on impact
				if (projectile.type == ProjectileID.Yelets)
				{
					if (Main.rand.Next(3) == 0)
					{
						target.AddBuff(BuffID.Venom, 8 * 60);
					}
				}

				//Red's Throw Inflicts a Random Debuff on impact
				if (projectile.type == ProjectileID.RedsYoyo)
				{
					int DebuffSelection = Main.rand.Next(11);
					switch (DebuffSelection)
					{
						case 0:
							target.AddBuff(BuffID.Confused, 5 * 60);
							break;
						case 1:
							target.AddBuff(BuffID.CursedInferno, 5 * 60);
							break;
						case 2:
							target.AddBuff(BuffID.Ichor, 5 * 60);
							break;
						case 3:
							target.AddBuff(BuffID.Poisoned, 5 * 60);
							break;
						case 4:
							target.AddBuff(BuffID.Venom, 5 * 60);
							break;
						case 5:
							target.AddBuff(BuffID.OnFire3, 5 * 60);
							break;
						case 6:
							target.AddBuff(BuffID.Frostburn2, 5 * 60);
							break;
						case 7:
							target.AddBuff(BuffID.Lovestruck, 5 * 60);
							break;
						case 8:
							target.AddBuff(BuffID.Stinky, 5 * 60);
							break;
						case 9:
							target.AddBuff(BuffID.Slimed, 5 * 60);
							break;
						case 10:
							target.AddBuff(BuffID.GelBalloonBuff, 5 * 60);
							break;
					}
				}

				//Kraken Abyssal Tentacles Effect
				if (projectile.type == ProjectileID.Kraken && HitEffectCooldown == 0)
				{
					SoundEngine.PlaySound(SoundID.Item103, projectile.position);
					int RandomRot = Main.rand.Next(0, 360);
					for (int d = 0; d < 3; d++)
					{
						RandomRot += 120;
						if (RandomRot >= 360)
						{
							RandomRot -= 360;
						}
						Vector2 value18 = new Vector2(3, 3).RotatedBy(MathHelper.ToRadians(RandomRot + Main.rand.Next(-55, 55)));
						value18.Normalize();
						Vector2 value19 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						value19.Normalize();
						value18 = value18 * 4f + value19;
						value18.Normalize();
						value18 *= new Vector2(5, 5);
						float num325 = (float)Main.rand.Next(10, 80) * 0.001f;
						if (Main.rand.Next(2) == 0)
						{
							num325 *= -1f;
						}
						float num326 = (float)Main.rand.Next(10, 80) * 0.001f;
						if (Main.rand.Next(2) == 0)
						{
							num326 *= -1f;
						}
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, value18, ModContent.ProjectileType<VanillaYoyoEffects.KrakenAbyssalTentacleProjectile>(), projectile.damage, projectile.knockBack, projectile.owner, num326, num325);
					}

					HitEffectCooldown = 13;
					projectile.netUpdate = true;
				}

				//Eye of Cthulhu enrages when striking enemies
				if (projectile.type == ProjectileID.TheEyeOfCthulhu)
				{
					EoCEnragedTimer = 30;
				}
			}
		}

		//Constant effects
		public override void AI(Projectile projectile)
		{
			//Vanilla Yoyo Effects that trigger passively
			if (projectile.aiStyle == 99 && ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
			{
                //On hit effect cooldown
                if (HitEffectCooldown > 0)
				{
					HitEffectCooldown--;
					projectile.netUpdate = true;
				}

				//Rally Afterimages
				if (projectile.type == ProjectileID.Rally)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 2)
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.RallyAfterimageProjectile>(), projectile.damage / 2, 0, projectile.owner);
					}
				}

				//Malaise Bile Drops
				if (projectile.type == ProjectileID.CorruptYoyo)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 10 + Main.rand.Next(16))
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						Projectile.NewProjectile(projectile.GetSource_FromThis(), (projectile.position.X + 2 + Main.rand.Next(8)), (projectile.position.Y + 2 + Main.rand.Next(8)), 0f, 0f, ModContent.ProjectileType<VanillaYoyoEffects.MalaiseBileDropletProjectile>(), projectile.damage / 3, 0.5f, projectile.owner);
					}
				}

				//Amazon randomly fires stringers
				if (projectile.type == ProjectileID.JungleYoyo)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 15 + Main.rand.Next(6))
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						Vector2 ProjectileVelocity = new Vector2(8, 8).RotatedByRandom(MathHelper.ToRadians(360));
						ProjectileVelocity *= Main.rand.NextFloat(0.6f, 1f);
						int FiredProjectile = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ProjectileID.HornetStinger, projectile.damage / 2, projectile.knockBack / 4, projectile.owner);
						Main.projectile[FiredProjectile].timeLeft = 15;
						Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
					}
				}

				//Code 1 Electrostatic Aura
				if (projectile.type == ProjectileID.Code1)
				{
					if (!this.Code1ElectrostaticAuraActive)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.Code1ElectrostaticAuraProjectile>(), projectile.damage / 3, 0f, projectile.owner, 0f, projectile.whoAmI);
						this.Code1ElectrostaticAuraActive = true;
					}
				}

				//Valor is orbited by 3 ghostly wisp projectiles
				if (projectile.type == ProjectileID.Valor)
				{
					if (!this.ValorWispOrbitActive)
					{
						for (int d = 0; d < 3; d++)
						{
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.ValorGhostlyWispProjectile>(), projectile.damage, projectile.knockBack / 4, projectile.owner, d, projectile.whoAmI);
						}
						this.ValorWispOrbitActive = true;
					}
				}

				//Gradient is orbited by 5 chromatic pulse projectiles
				if (projectile.type == ProjectileID.Gradient)
				{
					if (!this.GradientPulseOrbitActive)
					{
						for (int d = 0; d < 5; d++)
						{
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.GradientChromaticPulseProjectile>(), projectile.damage, projectile.knockBack / 4, projectile.owner, d, projectile.whoAmI);
						}
						this.GradientPulseOrbitActive = true;
					}
				}

				//Chik rapidly & randomly produces crystal shards
				if (projectile.type == ProjectileID.Chik)
				{
					if (Main.rand.Next(8) == 0)
                    {
						float ShardXVelocity = (0f - projectile.velocity.X) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
						float ShardYVelocity = (0f - projectile.velocity.Y) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
						int FiredProjectile = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, ShardXVelocity / 2, ShardYVelocity / 2, ProjectileID.CrystalShard, projectile.damage / 5, 0f, projectile.owner);
						Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
					}
				}

				//Amarok Snowflakes
				if (projectile.type == ProjectileID.Amarok)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 30)
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						SoundEngine.PlaySound(SoundID.Item27, projectile.Center);
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.AmarokSnowflakeProjectile>(), projectile.damage, 0, projectile.owner);
					}
				}

				//Code 2 Plasmatic Aura
				if (projectile.type == ProjectileID.Code2)
				{
					if (!this.Code2PlasmaticAuraActive)
					{
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<VanillaYoyoEffects.Code2PlasmaticAuraProjectile>(), projectile.damage / 2, 0f, projectile.owner, 0f, projectile.whoAmI);
						this.Code2PlasmaticAuraActive = true;
					}
				}

				//Yelets Randomly Fires Razor Leaves
				if (projectile.type == ProjectileID.Yelets)
				{
					projectile.frameCounter++;
					if (projectile.frameCounter >= 10 + Main.rand.Next(11))
					{
						projectile.frameCounter = 0;
						projectile.netUpdate = true;

						SoundEngine.PlaySound(SoundID.Item7, projectile.Center);
						Vector2 ProjectileVelocity = new Vector2(10, 10).RotatedByRandom(MathHelper.ToRadians(360));
						ProjectileVelocity *= Main.rand.NextFloat(0.75f, 1f);
						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.YeletsRazorLeafProjectile>(), projectile.damage / 2, projectile.knockBack / 3, projectile.owner);
					}
				}

				//Valkyrie Feather Barrage
				if (projectile.type == ProjectileID.ValkyrieYoyo)
				{
					projectile.frameCounter++;
					if ((projectile.frameCounter >= 45) && (ValkyrieTargetedEnemy == new Vector2(0, 0)))
                    {
						//Select Attack Target
						float AttackRange = 600f;
						for (int NpcCheck = 0; NpcCheck < 200; NpcCheck++)
						{
							NPC nPC = Main.npc[NpcCheck];
							if (Main.npc[NpcCheck].CanBeChasedBy(this, ignoreDontTakeDamage: true) && Vector2.Distance(projectile.Center, nPC.Center) <= AttackRange && Collision.CanHit(projectile.Center, 1, 1, Main.npc[NpcCheck].Center, 1, 1))
							{
								if (Vector2.Distance(projectile.Center, nPC.Center) < Vector2.Distance(projectile.Center, ValkyrieTargetedEnemy))
								{
									ValkyrieTargetedEnemy = nPC.Center;
								}
							}
						}

						projectile.frameCounter = 45;
					}
					if (((projectile.frameCounter == 45) || (projectile.frameCounter == 60) || (projectile.frameCounter == 75)) && (ValkyrieTargetedEnemy != new Vector2(0, 0)))
					{
						//Actually Fire
						Vector2 ProjectileVelocity = ValkyrieTargetedEnemy - projectile.Center;
						ProjectileVelocity.Normalize();
						ProjectileVelocity *= 6;
						ProjectileVelocity = ProjectileVelocity.RotatedByRandom(MathHelper.ToRadians(8 + Main.rand.Next(9)));

						Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.ValkyrieYoyoFeatherProjectile>(), projectile.damage / 2, projectile.knockBack / 5, projectile.owner);
					}
					if ((projectile.frameCounter >= 75) && (ValkyrieTargetedEnemy != new Vector2(0, 0)))
					{
						ValkyrieTargetedEnemy = new Vector2(0, 0);

						projectile.frameCounter = 0;
						projectile.netUpdate = true;
					}
				}

				//Eye of Cthulhu Produces mini servants of cthulhu
				if (projectile.type == ProjectileID.TheEyeOfCthulhu)
				{
					projectile.frameCounter++;

					//When you hit enemies it speeds up the rate at which the yoyo produces eyes temporarily
					if (EoCEnragedTimer > 0)
                    {
						EoCEnragedTimer--;

						if (projectile.frameCounter >= 20 + Main.rand.Next(11))
						{
							projectile.frameCounter = 0;
							projectile.netUpdate = true;

							Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
							ProjectileVelocity *= Main.rand.NextFloat(0.8f, 1f);
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.EoCMiniServantProjectile>(), projectile.damage / 5, projectile.knockBack / 8, projectile.owner);
						}
					}
                    else
                    {
						if (projectile.frameCounter >= 35 + Main.rand.Next(11))
						{
							projectile.frameCounter = 0;
							projectile.netUpdate = true;

							Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
							ProjectileVelocity *= Main.rand.NextFloat(0.8f, 1f);
							Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, ProjectileVelocity, ModContent.ProjectileType<VanillaYoyoEffects.EoCMiniServantProjectile>(), projectile.damage / 5, projectile.knockBack / 8, projectile.owner);
						}
					}
				}
			}
		}

		//Damage modification effects
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			//Format:C double critical strike damage effect
			if (projectile.type == ProjectileID.FormatC && ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
            {
				if (crit)
				{
					damage = (int)(damage * 2f);
				}
			}
		}

		//Counterweight Changes
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type >= 556 && projectile.type <= 561 && ModContent.GetInstance<TysYoyoReduxConfigServer>().BuffsToCounterweights == true)
            {
				projectile.usesLocalNPCImmunity = true;
				projectile.localNPCHitCooldown = 10;
				projectile.extraUpdates = 1;
			}
        }

		//AI overrides
        public override void Load()
		{
			On.Terraria.Projectile.AI_099_1 += ProjectileAI_099_01;
			On.Terraria.Projectile.AI_099_2 += ProjectileAI_099_02;
		}

		//Overrides vanilla Counterweight AI
		private void ProjectileAI_099_01(On.Terraria.Projectile.orig_AI_099_1 orig, Terraria.Projectile self)
		{
			//Setup for Counterweight Timelimit
			if (ModContent.GetInstance<TysYoyoReduxConfigServer>().BuffsToCounterweights == true)
            {
				self.frameCounter++;
			}

			self.timeLeft = 6;
			bool flag = true;
			float num = 250f;
			float num2 = 0.1f;
			float num3 = 15f;
			float num4 = 12f;
			num *= 0.5f;
			num3 *= 0.8f;
			num4 *= 1.5f;
			if (self.owner == Main.myPlayer)
			{
				bool flag2 = false;
				for (int i = 0; i < 1000; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == self.owner && Main.projectile[i].aiStyle == 99 && (Main.projectile[i].type < 556 || Main.projectile[i].type > 561))
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					self.ai[0] = -1f;
					self.netUpdate = true;
				}
			}
			if (Main.player[self.owner].yoyoString)
			{
				num += num * 0.25f + 10f;
			}
			self.rotation += 0.5f;
			if (Main.player[self.owner].dead)
			{
				self.Kill();
				return;
			}
			if (!flag)
			{
				Main.player[self.owner].heldProj = self.whoAmI;
				Main.player[self.owner].SetDummyItemTime(2);
				if (self.position.X + (float)(self.width / 2) > Main.player[self.owner].position.X + (float)(Main.player[self.owner].width / 2))
				{
					Main.player[self.owner].ChangeDir(1);
					self.direction = 1;
				}
				else
				{
					Main.player[self.owner].ChangeDir(-1);
					self.direction = -1;
				}
			}
			if (self.ai[0] == 0f || self.ai[0] == 1f)
			{
				if (self.ai[0] == 1f)
				{
					num *= 0.75f;
				}
				num4 *= 0.5f;
				bool flag3 = false;
				Vector2 vector = Main.player[self.owner].Center - self.Center;
				if ((double)vector.Length() > (double)num * 0.9)
				{
					flag3 = true;
				}
				if (vector.Length() > num)
				{
					float num5 = vector.Length() - num;
					Vector2 vector2 = default(Vector2);
					vector2.X = vector.Y;
					vector2.Y = vector.X;
					vector.Normalize();
					vector *= num;
					self.position = Main.player[self.owner].Center - vector;
					self.position.X -= self.width / 2;
					self.position.Y -= self.height / 2;
					float num6 = self.velocity.Length();
					self.velocity.Normalize();
					if (num5 > num6 - 1f)
					{
						num5 = num6 - 1f;
					}
					self.velocity *= num6 - num5;
					num6 = self.velocity.Length();
					Vector2 vector3 = new Vector2(self.Center.X, self.Center.Y);
					Vector2 vector4 = new Vector2(Main.player[self.owner].Center.X, Main.player[self.owner].Center.Y);
					if (vector3.Y < vector4.Y)
					{
						vector2.Y = Math.Abs(vector2.Y);
					}
					else if (vector3.Y > vector4.Y)
					{
						vector2.Y = 0f - Math.Abs(vector2.Y);
					}
					if (vector3.X < vector4.X)
					{
						vector2.X = Math.Abs(vector2.X);
					}
					else if (vector3.X > vector4.X)
					{
						vector2.X = 0f - Math.Abs(vector2.X);
					}
					vector2.Normalize();
					vector2 *= self.velocity.Length();
					new Vector2(vector2.X, vector2.Y);
					if (Math.Abs(self.velocity.X) > Math.Abs(self.velocity.Y))
					{
						Vector2 vector5 = self.velocity;
						vector5.Y += vector2.Y;
						vector5.Normalize();
						vector5 *= self.velocity.Length();
						if ((double)Math.Abs(vector2.X) < 0.1 || (double)Math.Abs(vector2.Y) < 0.1)
						{
							self.velocity = vector5;
						}
						else
						{
							self.velocity = (vector5 + self.velocity * 2f) / 3f;
						}
					}
					else
					{
						Vector2 vector6 = self.velocity;
						vector6.X += vector2.X;
						vector6.Normalize();
						vector6 *= self.velocity.Length();
						if ((double)Math.Abs(vector2.X) < 0.2 || (double)Math.Abs(vector2.Y) < 0.2)
						{
							self.velocity = vector6;
						}
						else
						{
							self.velocity = (vector6 + self.velocity * 2f) / 3f;
						}
					}
				}
				if (Main.myPlayer == self.owner)
				{
					//Slightly Edited if statement should mean that the counterweight will always return after 4-6 seconds
					if (Main.player[self.owner].channel && self.frameCounter < (int)(60 * 2 * Main.rand.NextFloat(4f, 6f)))
					{
						Vector2 vector7 = new Vector2(Main.mouseX - Main.lastMouseX, Main.mouseY - Main.lastMouseY);
						if (self.velocity.X != 0f || self.velocity.Y != 0f)
						{
							if (flag)
							{
								vector7 *= -1f;
							}
							if (flag3)
							{
								if (self.Center.X < Main.player[self.owner].Center.X && vector7.X < 0f)
								{
									vector7.X = 0f;
								}
								if (self.Center.X > Main.player[self.owner].Center.X && vector7.X > 0f)
								{
									vector7.X = 0f;
								}
								if (self.Center.Y < Main.player[self.owner].Center.Y && vector7.Y < 0f)
								{
									vector7.Y = 0f;
								}
								if (self.Center.Y > Main.player[self.owner].Center.Y && vector7.Y > 0f)
								{
									vector7.Y = 0f;
								}
							}
							self.velocity += vector7 * num2;
							self.netUpdate = true;
						}
					}
					else
					{
						self.ai[0] = 10f;
						self.netUpdate = true;
					}
				}
				if (flag || self.type == 562 || self.type == 547 || self.type == 555 || self.type == 564 || self.type == 552 || self.type == 563 || self.type == 549 || self.type == 550 || self.type == 554 || self.type == 553 || self.type == 603)
				{
					float num7 = 800f;
					Vector2 vector8 = default(Vector2);
					bool flag4 = false;
					if (self.type == 549)
					{
						num7 = 200f;
					}
					if (self.type == 554)
					{
						num7 = 400f;
					}
					if (self.type == 553)
					{
						num7 = 250f;
					}
					if (self.type == 603)
					{
						num7 = 320f;
					}
					for (int j = 0; j < 200; j++)
					{
						if (Main.npc[j].CanBeChasedBy(this))
						{
							float num8 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
							float num9 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
							float num10 = Math.Abs(self.position.X + (float)(self.width / 2) - num8) + Math.Abs(self.position.Y + (float)(self.height / 2) - num9);
							if (num10 < num7 && (self.type != 563 || !(num10 < 200f)) && Collision.CanHit(self.position, self.width, self.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height) && (double)(Main.npc[j].Center - Main.player[self.owner].Center).Length() < (double)num * 0.9)
							{
								num7 = num10;
								vector8.X = num8;
								vector8.Y = num9;
								flag4 = true;
							}
						}
					}
					if (flag4)
					{
						vector8 -= self.Center;
						vector8.Normalize();
						if (self.type == 563)
						{
							vector8 *= 4f;
							self.velocity = (self.velocity * 14f + vector8) / 15f;
						}
						else if (self.type == 553)
						{
							vector8 *= 5f;
							self.velocity = (self.velocity * 12f + vector8) / 13f;
						}
						else if (self.type == 603)
						{
							vector8 *= 16f;
							self.velocity = (self.velocity * 9f + vector8) / 10f;
						}
						else if (self.type == 554)
						{
							vector8 *= 8f;
							self.velocity = (self.velocity * 6f + vector8) / 7f;
						}
						else
						{
							vector8 *= 6f;
							self.velocity = (self.velocity * 7f + vector8) / 8f;
						}
					}
				}
				if (self.velocity.Length() > num3)
				{
					self.velocity.Normalize();
					self.velocity *= num3;
				}
				if (self.velocity.Length() < num4)
				{
					self.velocity.Normalize();
					self.velocity *= num4;
				}
				return;
			}
			self.tileCollide = false;
			Vector2 vector9 = Main.player[self.owner].Center - self.Center;
			float num11 = vector9.Length();
			if (num11 < 40f || vector9.HasNaNs() || num11 > 2000f)
			{
				self.Kill();
				return;
			}
			float num12 = num3 * 1.5f;
			if (self.type == 546)
			{
				num12 *= 1.5f;
			}
			if (self.type == 554)
			{
				num12 *= 1.25f;
			}
			if (self.type == 555)
			{
				num12 *= 1.35f;
			}
			if (self.type == 562)
			{
				num12 *= 1.25f;
			}
			float num13 = 12f;
			vector9.Normalize();
			vector9 *= num12;
			self.velocity = (self.velocity * (num13 - 1f) + vector9) / num13;
		}

		//Overrides vanilla yoyo AI
		private void ProjectileAI_099_02(On.Terraria.Projectile.orig_AI_099_2 orig, Terraria.Projectile self)
		{
			//Overwrites vanilla yoyo AI so I can add accessory effects
			bool flag = false;
			for (int i = 0; i < self.whoAmI; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == self.owner && Main.projectile[i].type == self.type)
				{
					flag = true;
				}
			}
			if (self.owner == Main.myPlayer)
			{
				self.localAI[0] += 1f;
				if (flag)
				{
					self.localAI[0] += (float)Main.rand.Next(10, 31) * 0.1f;
				}
				float num = self.localAI[0] / 60f;
				num /= (1f + Main.player[self.owner].GetAttackSpeed(DamageClass.Melee)) / 2f;
				float num2 = ProjectileID.Sets.YoyosLifeTimeMultiplier[self.type];
				//If the player has the yoyo bearings accessory equiped, their yoyo lifetime is increased by 50%. [MOD EFFECT]
				if (Main.player[self.owner].GetModPlayer<TysYoyoReduxPlayer>().YoyoBearings && num2 != -1)
				{
					num2 *= 1.5f;
				}
				if (num2 != -1f && num > num2)
				{
					self.ai[0] = -1f;
				}
			}
			if (self.type == 603 && self.owner == Main.myPlayer)
			{
				self.localAI[1] += 1f;
				if (self.localAI[1] >= 6f)
				{
					float num3 = 400f;
					Vector2 vector = self.velocity;
					Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					vector2.Normalize();
					vector2 *= (float)Main.rand.Next(10, 41) * 0.1f;
					if (Main.rand.Next(3) == 0)
					{
						vector2 *= 2f;
					}
					vector *= 0.25f;
					vector += vector2;
					for (int j = 0; j < 200; j++)
					{
						if (Main.npc[j].CanBeChasedBy(this))
						{
							float num4 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
							float num5 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
							float num6 = Math.Abs(self.position.X + (float)(self.width / 2) - num4) + Math.Abs(self.position.Y + (float)(self.height / 2) - num5);
							if (num6 < num3 && Collision.CanHit(self.position, self.width, self.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
							{
								num3 = num6;
								vector.X = num4;
								vector.Y = num5;
								vector -= self.Center;
								vector.Normalize();
								vector *= 8f;
							}
						}
					}
					vector *= 0.8f;
					Projectile.NewProjectile(self.GetSource_FromThis(), self.Center.X - vector.X, self.Center.Y - vector.Y, vector.X, vector.Y, 604, self.damage, self.knockBack, self.owner);
					self.localAI[1] = 0f;
				}
			}
			bool flag2 = false;
			if (self.type >= 556 && self.type <= 561)
			{
				flag2 = true;
			}
			if (Main.player[self.owner].dead)
			{
				self.Kill();
				return;
			}
			if (!flag2 && !flag)
			{
				Main.player[self.owner].heldProj = self.whoAmI;
				Main.player[self.owner].SetDummyItemTime(2);
				if (self.position.X + (float)(self.width / 2) > Main.player[self.owner].position.X + (float)(Main.player[self.owner].width / 2))
				{
					Main.player[self.owner].ChangeDir(1);
					self.direction = 1;
				}
				else
				{
					Main.player[self.owner].ChangeDir(-1);
					self.direction = -1;
				}
			}
			if (self.velocity.HasNaNs())
			{
				self.Kill();
			}
			self.timeLeft = 6;
			float num7 = 10f;
			float num8 = 10f;
			float num9 = 3f;
			float num10 = 200f;
			num10 = ProjectileID.Sets.YoyosMaximumRange[self.type];
			num8 = ProjectileID.Sets.YoyosTopSpeed[self.type];
			if (self.type == 545)
			{
				if (Main.rand.Next(6) == 0)
				{
					int num11 = Dust.NewDust(self.position, self.width, self.height, 6);
					Main.dust[num11].noGravity = true;
				}
			}
			else if (self.type == 553 && Main.rand.Next(2) == 0)
			{
				int num12 = Dust.NewDust(self.position, self.width, self.height, 6);
				Main.dust[num12].noGravity = true;
				Main.dust[num12].scale = 1.6f;
			}
			if (Main.player[self.owner].yoyoString)
			{
				num10 = num10 * 1.25f + 30f;
			}
			num10 /= (1f + Main.player[self.owner].GetAttackSpeed(DamageClass.Melee) * 3f) / 4f;
			num8 /= (1f + Main.player[self.owner].GetAttackSpeed(DamageClass.Melee) * 3f) / 4f;
			num7 = 14f - num8 / 2f;
			if (num7 < 1f)
			{
				num7 = 1f;
			}
			num9 = 5f + num8 / 2f;
			if (flag)
			{
				num9 += 20f;
			}
			if (self.ai[0] >= 0f)
			{
				if (self.velocity.Length() > num8)
				{
					self.velocity *= 0.98f;
				}
				bool flag3 = false;
				bool flag4 = false;
				Vector2 vector3 = Main.player[self.owner].Center - self.Center;
				if (vector3.Length() > num10)
				{
					flag3 = true;
					if ((double)vector3.Length() > (double)num10 * 1.3)
					{
						flag4 = true;
					}
				}
				if (self.owner == Main.myPlayer)
				{
					if (!Main.player[self.owner].channel || Main.player[self.owner].stoned || Main.player[self.owner].frozen)
					{
						self.ai[0] = -1f;
						self.ai[1] = 0f;
						self.netUpdate = true;
					}
					else
					{
						Vector2 vector4 = Main.ReverseGravitySupport(Main.MouseScreen) + Main.screenPosition;
						float x = vector4.X;
						float y = vector4.Y;
						Vector2 vector5 = new Vector2(x, y) - Main.player[self.owner].Center;
						if (vector5.Length() > num10)
						{
							vector5.Normalize();
							vector5 *= num10;
							vector5 = Main.player[self.owner].Center + vector5;
							x = vector5.X;
							y = vector5.Y;
						}
						if (self.ai[0] != x || self.ai[1] != y)
						{
							Vector2 vector6 = new Vector2(x, y) - Main.player[self.owner].Center;
							if (vector6.Length() > num10 - 1f)
							{
								vector6.Normalize();
								vector6 *= num10 - 1f;
								Vector2 vector7 = Main.player[self.owner].Center + vector6;
								x = vector7.X;
								y = vector7.Y;
							}
							self.ai[0] = x;
							self.ai[1] = y;
							self.netUpdate = true;
						}
					}
				}
				if (flag4 && self.owner == Main.myPlayer)
				{
					self.ai[0] = -1f;
					self.netUpdate = true;
				}
				if (self.ai[0] >= 0f)
				{
					if (flag3)
					{
						num7 /= 2f;
						num8 *= 2f;
						if (self.Center.X > Main.player[self.owner].Center.X && self.velocity.X > 0f)
						{
							self.velocity.X *= 0.5f;
						}
						if (self.Center.Y > Main.player[self.owner].Center.Y && self.velocity.Y > 0f)
						{
							self.velocity.Y *= 0.5f;
						}
						if (self.Center.X < Main.player[self.owner].Center.X && self.velocity.X < 0f)
						{
							self.velocity.X *= 0.5f;
						}
						if (self.Center.Y < Main.player[self.owner].Center.Y && self.velocity.Y < 0f)
						{
							self.velocity.Y *= 0.5f;
						}
					}
					Vector2 vector8 = new Vector2(self.ai[0], self.ai[1]) - self.Center;
					if (flag3)
					{
						num7 = 1f;
					}
					self.velocity.Length();
					float num13 = vector8.Length();
					if (num13 > num9)
					{
						vector8.Normalize();
						float num14 = Math.Min(num13 / 2f, num8);
						if (flag3)
						{
							num14 = Math.Min(num14, num8 / 2f);
						}
						vector8 *= num14;
						self.velocity = (self.velocity * (num7 - 1f) + vector8) / num7;
					}
					else if (flag)
					{
						if ((double)self.velocity.Length() < (double)num8 * 0.6)
						{
							vector8 = self.velocity;
							vector8.Normalize();
							vector8 *= num8 * 0.6f;
							self.velocity = (self.velocity * (num7 - 1f) + vector8) / num7;
						}
					}
					else
					{
						self.velocity *= 0.8f;
					}
					if (flag && !flag3 && (double)self.velocity.Length() < (double)num8 * 0.6)
					{
						self.velocity.Normalize();
						self.velocity *= num8 * 0.6f;
					}
				}
			}
			else
			{
				num7 = (int)((double)num7 * 0.8);
				num8 *= 1.5f;
				self.tileCollide = false;
				Vector2 vector9 = Main.player[self.owner].Center - self.Center;
				float num15 = vector9.Length();
				if (num15 < num8 + 10f || num15 == 0f || num15 > 2000f)
				{
					self.Kill();
				}
				else
				{
					vector9.Normalize();
					vector9 *= num8;
					self.velocity = (self.velocity * (num7 - 1f) + vector9) / num7;
				}
			}
			self.rotation += 0.45f;
		}
	}
}